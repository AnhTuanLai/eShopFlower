using eShopFlower.ViewModels.Catalog.Products;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Users;

namespace eShopFlower.AdminApp.Services
{
	public interface IProductApiClient
	{
		Task<PagedResult<ProductViewModel>> GetPagings(GetManageProductPadingRequest request);

		Task<bool> CreateProduct(ProductCreateRequest request);

		Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

		Task<ProductViewModel> GetById(int id, string languageId);
	}
}