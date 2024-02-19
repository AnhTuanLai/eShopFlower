using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Roles;

namespace eShopFlower.AdminApp.Services
{
	public interface IRoleApiClient
	{
		Task<ApiResult<List<RoleViewModel>>> GetAll();
	}
}