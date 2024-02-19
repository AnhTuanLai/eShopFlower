using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.Catalog.Categories;

namespace eShopFlower.Application.Catalog.Categories
{
	public interface ICategoryService
	{
		Task<List<CategoryViewModel>> GetAll(string languageId);
	}
}