using eShopFlower.ViewModels.Catalog.Categories;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Languages;

namespace eShopFlower.AdminApp.Services
{
	public class CategoryApiClient : BaseApiClient, ICategoryApiClient
	{
		public CategoryApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
			: base(httpClientFactory, configuration, contextAccessor)
		{
		}

		public async Task<List<CategoryViewModel>> GetAll(string languageId)
		{
			return await GetListAsync<CategoryViewModel>("/api/categories?languageId=" + languageId);
		}
	}
}