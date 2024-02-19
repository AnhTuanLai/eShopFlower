using eShopFlower.Application.Catalog.Products;
using eShopFlower.Application.System.Languages;
using eShopFlower.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlower.BackendApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LanguageController : ControllerBase
	{
		private readonly ILanguageService _languageService;

		public LanguageController(ILanguageService languageService)
		{
			_languageService = languageService;
		}

		[HttpGet()]
		public async Task<IActionResult> GetALl()
		{
			var language = await _languageService.GetAll();
			return Ok(language);
		}
	}
}