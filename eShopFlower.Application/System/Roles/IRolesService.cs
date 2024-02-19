using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.System.Roles;

namespace eShopFlower.Application.System.Roles
{
	public interface IRolesService
	{
		Task<List<RoleViewModel>> GetAll();
	}
}