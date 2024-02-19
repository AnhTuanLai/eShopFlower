using eShopFlower.Application.Catalog.Categories;
using eShopFlower.Application.Catalog.Products;
using eShopFlower.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlower.BackendApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPaging(string languageId)
		{
			var products = await _categoryService.GetAll(languageId);
			return Ok(products);
		}
	}
}