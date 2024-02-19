using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Users;

namespace eShopFlower.AdminApp.Services
{
	public interface IUserApiClient
	{
		Task<ApiResult<string>> Authenticate(LoginRequest request);

		Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPagings(GetUsersPagingRequest request);

		Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);

		Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest updateRequest);

		Task<ApiResult<UserViewModel>> GetById(Guid id);

		Task<ApiResult<bool>> Delete(Guid id);

		Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
	}
}