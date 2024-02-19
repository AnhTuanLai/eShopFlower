using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Languages;

namespace eShopFlower.AdminApp.Services
{
	public interface ILanguageApiClient
	{
		Task<ApiResult<List<LanguageViewModel>>> GetAll();
	}
}