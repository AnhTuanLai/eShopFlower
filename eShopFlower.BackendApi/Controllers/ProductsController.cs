using eShopFlower.Application.Catalog.Products;
using eShopFlower.ViewModels.Catalog.ProductImages;
using eShopFlower.ViewModels.Catalog.Products;
using eShopFlower.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlower.BackendApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProductsController : ControllerBase
	{
		private readonly IProductsService _productsService;

		public ProductsController(IProductsService productsService)
		{
			_productsService = productsService;
		}

		//http://localhost:port/product
		/*
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var products = await _publicProductService.GetAll(languageId);
            return Ok(products);
        }*/

		//http://localhost:port/products?pageIndex=1&pageSize10&CategoryId=
		/* [HttpGet("public-paging/{languageId}")]
		 public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] GetPublicProductPadingRequest request)
		 {
			 var products = await _productsService.GetALlByCategoryId(languageId, request);
			 return Ok(products);
		 }*/

		[HttpGet("paging")]
		public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPadingRequest request)
		{
			var products = await _productsService.GetAllPaging(request);
			return Ok(products);
		}

		//http://localhost:port/product/1
		[HttpGet("{productId}/{languageId}")]
		public async Task<IActionResult> GetById(int productId, string languageId)
		{
			var product = await _productsService.GetById(productId, languageId);
			if (product == null)
				return BadRequest("Cannot find product");
			return Ok(product);
		}

		[HttpPut("{id}/categories")]
		public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var result = await _productsService.CategoryAssign(id, request);
			if (!result.IsSuccessd)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPost]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var productId = await _productsService.Create(request);
			if (productId == 0)
				return BadRequest();
			var product = _productsService.GetById(productId, request.LanguageId);
			return CreatedAtAction(nameof(GetById), new { id = productId }, product);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var affectedResult = await _productsService.Update(request);
			if (affectedResult == 0)
				return BadRequest();
			return Ok();
		}

		[HttpDelete("{productId}")]
		public async Task<IActionResult> Delete(int productId)
		{
			var affectedResult = await _productsService.Delete(productId);
			if (affectedResult == 0)
				return BadRequest();
			return Ok();
		}

		[HttpPatch("price/{productId}/{newPrice}")]
		public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
		{
			var isSuccessFul = await _productsService.UpdatePrice(productId, newPrice);
			if (isSuccessFul)
				return Ok();
			return BadRequest();
		}

		//Images
		[HttpPost("{productId}/images")]
		public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var imageId = await _productsService.AddImage(productId, request);
			if (imageId == 0)
				return BadRequest();
			var image = _productsService.GetImageById(imageId);
			return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
		}

		[HttpGet("{productId}/images/{imageId}")]
		public async Task<IActionResult> GetImageById(int productId, int imageId)
		{
			var image = await _productsService.GetImageById(imageId);
			if (image == null)
				return BadRequest("Cannot find product");
			return Ok(image);
		}

		[HttpPut("{productId}/images/{imageId}")]
		public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await _productsService.UpdateImage(imageId, request);
			if (result == 0)
				return BadRequest();

			return Ok();
		}

		[HttpDelete("{productId}/images/{imageId}")]
		public async Task<IActionResult> RemoveImage(int imageId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await _productsService.RemoveImage(imageId);
			if (result == 0)
				return BadRequest();

			return Ok();
		}
	}
}