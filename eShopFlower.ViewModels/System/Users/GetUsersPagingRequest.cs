using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.Common;

namespace eShopFlower.ViewModels.System.Users
{
	public class GetUsersPagingRequest : PagingRequestBase
	{
		public string? Keyword { get; set; }
	}
}