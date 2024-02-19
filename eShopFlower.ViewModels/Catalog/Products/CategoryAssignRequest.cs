using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.Common;

namespace eShopFlower.ViewModels.Catalog.Products
{
	public class CategoryAssignRequest
	{
		public int Id { get; set; }

		public List<SelectItem> Categories { get; set; } = new List<SelectItem>();
	}
}