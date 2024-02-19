using eShopFlower.ViewModels.Catalog.Categories;
using eShopFlower.ViewModels.Common;

namespace eShopFlower.AdminApp.Services
{
	public interface ICategoryApiClient
	{
		Task<List<CategoryViewModel>> GetAll(string languageId);
	}
}