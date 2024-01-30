using eShopFlower.Application.Catalog.Products;
using eShopFlower.ViewModels.Catalog.ProductImages;
using eShopFlower.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlower.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductsService _manageProductsService;

        public ProductsController(IPublicProductService publicProductService, IManageProductsService manageProductsService)
        {
            _publicProductService = publicProductService;
            _manageProductsService = manageProductsService;
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
        [HttpGet("public-paging/{languageId}")]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] GetPublicProductPadingRequest request)
        {
            var products = await _publicProductService.GetALlByCategoryId(languageId, request);
            return Ok(products);
        }

        //http://localhost:port/product/1
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductsService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductsService.Create(request);
            if (productId == 0)
                return BadRequest();
            var product = _manageProductsService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductsService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _manageProductsService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPatch("price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessFul = await _manageProductsService.UpdatePrice(productId, newPrice);
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
            var imageId = await _manageProductsService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();
            var image = _manageProductsService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _manageProductsService.GetImageById(imageId);
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
            var result = await _manageProductsService.UpdateImage(imageId, request);
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
            var result = await _manageProductsService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }
    }
}