using eShopFlower.ViewModels.Catalog.ProductImages;
using eShopFlower.ViewModels.Catalog.Products;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;

namespace eShopFlower.Application.Catalog.Products
{
	public interface IProductsService
	{
		Task<int> Create(ProductCreateRequest request);

		Task<int> Update(ProductUpdateRequest request);

		Task<int> Delete(int productId);

		Task<ProductViewModel> GetById(int productId, string languageId);

		Task<bool> UpdatePrice(int productId, decimal newPrice);

		Task<bool> UpdateStock(int productId, int addedQuantity);

		Task AddViewCount(int productId);

		Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPadingRequest request);

		Task<int> AddImage(int productId, ProductImageCreateRequest request);

		Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

		Task<int> RemoveImage(int imageId);

		Task<ProductImageViewModel> GetImageById(int imageId);

		Task<List<ProductImageViewModel>> GetListImages(int productId);

		Task<PagedResult<ProductViewModel>> GetALlByCategoryId(string languageId, GetPublicProductPadingRequest request);

		Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
	}
}