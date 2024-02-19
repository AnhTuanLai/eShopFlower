using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.Common;

namespace eShopFlower.ViewModels.System.Users
{
	public class RoleAssignRequest
	{
		public Guid Id { get; set; }
		public List<SelectItem>? Roles { get; set; } = new List<SelectItem>();
	}
}