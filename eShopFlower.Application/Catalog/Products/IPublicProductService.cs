using eShopFlower.ViewModels.Catalog.Products;
using eShopFlower.ViewModels.Common;

namespace eShopFlower.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetALlByCategoryId(string languageId, GetPublicProductPadingRequest request);

       // Task<List<ProductViewModel>> GetAll(string languageId);
    }
}
