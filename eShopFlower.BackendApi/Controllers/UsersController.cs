using eShopFlower.Application.System.Users;
using eShopFlower.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlower.BackendApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userSerice;

		public UsersController(IUserService userService)
		{
			_userSerice = userService;
		}

		[HttpPost("authenticate")]
		[AllowAnonymous]
		public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var resultToken = await _userSerice.Authenticate(request);
			if (string.IsNullOrEmpty(resultToken.ResultObject))
			{
				return BadRequest(resultToken);
			}

			return Ok(resultToken);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var result = await _userSerice.Register(request);
			if (!result.IsSuccessd)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		//put http://localhost/api/users/id
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var result = await _userSerice.Update(id, request);
			if (!result.IsSuccessd)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPut("{id}/roles")]
		public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var result = await _userSerice.RoleAssign(id, request);
			if (!result.IsSuccessd)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		//http://localhost/api/users/paging?pageIndex=1&pageSize=10$keyword=
		[HttpGet("paging")]
		public async Task<IActionResult> GetAllPaging([FromQuery] GetUsersPagingRequest request)
		{
			var products = await _userSerice.GetUsersPaging(request);
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var user = await _userSerice.GetById(id);
			return Ok(user);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _userSerice.Delete(id);
			return Ok(result);
		}
	}
}