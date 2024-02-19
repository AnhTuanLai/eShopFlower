using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Users;

namespace eShopFlower.Application.System.Users
{
	public interface IUserService
	{
		Task<ApiResult<string>> Authenticate(LoginRequest request);

		Task<ApiResult<bool>> Register(RegisterRequest request);

		Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

		Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUsersPagingRequest request);

		Task<ApiResult<UserViewModel>> GetById(Guid id);

		Task<ApiResult<bool>> Delete(Guid id);

		Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
	}
}