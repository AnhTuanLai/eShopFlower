using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.Data.Entities;
using eShopFlower.ViewModels.Catalog.Products;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eShopFlower.Application.System.Users
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IConfiguration _config;

		public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_config = config;
		}

		public async Task<ApiResult<string>> Authenticate(LoginRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.UserName);
			if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

			var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
			if (!result.Succeeded)
			{
				return new ApiErrorResult<string>("Đăng nhập không đúng");
			}

			var role = await _userManager.GetRolesAsync(user);
			var claims = new[]
			{
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.GivenName,user.FirstName),
				new Claim(ClaimTypes.Role,string.Join(";",role)),
				new Claim (ClaimTypes.Name,request.UserName)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_config["Tokens:Issuer"],
				_config["Tokens:Issuer"],
				claims,
				expires: DateTime.Now.AddHours(3),
				signingCredentials: creds);

			return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
		}

		public async Task<ApiResult<bool>> Delete(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
				return new ApiErrorResult<bool>("User không tồn tại");
			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded)
				return new ApiSuccessResult<bool>();
			return new ApiErrorResult<bool>("Xóa không thành công");
		}

		public async Task<ApiResult<UserViewModel>> GetById(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return new ApiErrorResult<UserViewModel>("User không tồn tại");
			}
			var roles = await _userManager.GetRolesAsync(user);
			var userVM = new UserViewModel()
			{
				Email = user.Email,
				Dob = user.Dob,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNunber = user.PhoneNumber,
				Id = user.Id,
				UserName = user.UserName,
				Roles = roles
			};
			return new ApiSuccessResult<UserViewModel>(userVM);
		}

		public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUsersPagingRequest request)
		{
			var query = _userManager.Users;

			if (!string.IsNullOrEmpty(request.Keyword))
			{
				query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
			}

			int totalRow = await query.CountAsync();

			var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
				.Select(x => new UserViewModel()
				{
					Email = x.Email,
					FirstName = x.FirstName,
					LastName = x.LastName,
					UserName = x.UserName,
					PhoneNunber = x.PhoneNumber,
					Id = x.Id
				}).ToListAsync();
			// select and  projection
			var pageResult = new PagedResult<UserViewModel>()
			{
				TotalRecords = totalRow,
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				Items = data
			};
			return new ApiSuccessResult<PagedResult<UserViewModel>>(pageResult);
		}

		public async Task<ApiResult<bool>> Register(RegisterRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.UserName);
			if (user != null)
			{
				return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
			}
			if (await _userManager.FindByEmailAsync(request.Email) != null)
			{
				return new ApiErrorResult<bool>("Email đã tồn tại");
			}
			user = new AppUser
			{
				UserName = request.UserName,
				Email = request.Email,
				Dob = request.Dob,
				FirstName = request.FirtName,
				LastName = request.LastName,
				PhoneNumber = request.PhoneNumber
			};
			var result = await _userManager.CreateAsync(user, request.Password);
			if (result.Succeeded)
			{
				return new ApiSuccessResult<bool>();
			}
			return new ApiErrorResult<bool>("Đăng kí không thành công");
		}

		public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
			}

			var removeRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
			foreach (var roleName in removeRoles)
			{
				if (await _userManager.IsInRoleAsync(user, roleName) == true)
				{
					await _userManager.AddToRoleAsync(user, roleName);
				}
			}
			await _userManager.RemoveFromRolesAsync(user, removeRoles);

			var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
			foreach (var roleName in addedRoles)
			{
				if (await _userManager.IsInRoleAsync(user, roleName) == false)
				{
					await _userManager.AddToRoleAsync(user, roleName);
				}
			}
			return new ApiSuccessResult<bool>();
		}

		public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
		{
			if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
			{
				return new ApiErrorResult<bool>("Email đã tồn tại");
			}

			var user = await _userManager.FindByIdAsync(id.ToString());

			user.Email = request.Email;
			user.Dob = request.Dob;
			user.FirstName = request.FirtName;
			user.LastName = request.LastName;
			user.PhoneNumber = request.PhoneNumber;

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return new ApiSuccessResult<bool>();
			}
			return new ApiErrorResult<bool>("Cập nhập không thành công");
		}
	}
}