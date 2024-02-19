using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eShopFlower.AdminApp.Services;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace eShopFlower.AdminApp.Controllers
{
	public class UserController : BaseController
	{
		private readonly IUserApiClient _userApiClient;
		private readonly IConfiguration _configuration;
		private readonly IRoleApiClient _roleApiClient;

		public UserController(IUserApiClient userApiClient, IConfiguration configuration, IRoleApiClient roleApiClient)
		{
			_userApiClient = userApiClient;
			_configuration = configuration;
			_roleApiClient = roleApiClient;
		}

		public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
		{
			var request = new GetUsersPagingRequest()
			{
				Keyword = keyword,
				PageSize = pageSize,
				PageIndex = pageIndex
			};

			var data = await _userApiClient.GetUsersPagings(request);
			ViewBag.Keyword = keyword;
			// var user = User.Identity.Name;
			if (TempData["result"] != null)
			{
				ViewBag.SuccessMsg = TempData["result"];
			}
			return View(data.ResultObject);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			HttpContext.Session.Remove("Token");

			return RedirectToAction("Index", "Login");
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return View();
			var result = await _userApiClient.RegisterUser(request);
			if (result.IsSuccessd)
			{
				TempData["result"] = "Thêm mới người dùng thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", result.Message);
			return View(request);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var result = await _userApiClient.GetById(id);
			if (result.IsSuccessd)
			{
				var user = result.ResultObject;
				var updateRequest = new UserUpdateRequest()
				{
					Dob = user.Dob,
					Email = user.Email,
					FirtName = user.FirstName,
					LastName = user.LastName,
					PhoneNumber = user.PhoneNunber,
					Id = id
				};
				return View(updateRequest);
			}

			return RedirectToAction("Error", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UserUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return View();
			var result = await _userApiClient.UpdateUser(request.Id, request);
			if (result.IsSuccessd)
			{
				TempData["result"] = "Cập nhập người dùng thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", result.Message);
			return View(request);
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var result = await _userApiClient.GetById(id);
			return View(result.ResultObject);
		}

		[HttpGet]
		public IActionResult Delete(Guid id)
		{
			return View(new UserDeleteRequest()
			{
				Id = id
			});
		}

		[HttpPost]
		public async Task<IActionResult> Delete(UserDeleteRequest request)
		{
			if (!ModelState.IsValid)
				return View();
			var result = await _userApiClient.Delete(request.Id);
			if (result.IsSuccessd)
			{
				TempData["result"] = "Xóa người dùng thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", result.Message);
			return View(request);
		}

		[HttpGet]
		public async Task<IActionResult> RoleAssign(Guid id)
		{
			var roleAssignRequest = await GetRoleAssignRequest(id);
			return View(roleAssignRequest);
		}

		[HttpPost]
		public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
		{
			if (!ModelState.IsValid)
				return View();
			var result = await _userApiClient.RoleAssign(request.Id, request);
			if (result.IsSuccessd)
			{
				TempData["result"] = "Cập nhập quyền thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", result.Message);
			var roleAssignRequest = await GetRoleAssignRequest(request.Id);
			return View(roleAssignRequest);
		}

		private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
		{
			var userObj = await _userApiClient.GetById(id);
			var roleObj = await _roleApiClient.GetAll();
			var roleAssignRequest = new RoleAssignRequest();
			foreach (var item in roleObj.ResultObject)
			{
				roleAssignRequest.Roles.Add(new SelectItem()
				{
					Id = item.Id.ToString(),
					Name = item.Name,
					Selected = userObj.ResultObject.Roles.Contains(item.Name)
				});
			}
			return roleAssignRequest;
		}
	}
}