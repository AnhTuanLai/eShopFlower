using eShopFlower.Application.System.Roles;
using eShopFlower.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlower.BackendApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class RolesController : ControllerBase
	{
		private readonly IRolesService _rolesService;

		public RolesController(IRolesService rolesService)
		{
			_rolesService = rolesService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var roles = await _rolesService.GetAll();
			return Ok(roles);
		}
	}
}