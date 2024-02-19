using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.Application.Common;
using eShopFlower.Data.EF;
using eShopFlower.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;

namespace eShopFlower.Application.Catalog.Categories
{
	public class CategoryService : ICategoryService
	{
		private readonly eShopFlowerDbContext _context;

		public CategoryService(eShopFlowerDbContext context)
		{
			_context = context;
		}

		public async Task<List<CategoryViewModel>> GetAll(string languageId)
		{
			var query = from c in _context.Categories
						join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
						where ct.LanguageId == languageId
						select new { c, ct };

			return await query.Select(x => new CategoryViewModel()
			{
				Id = x.c.Id,
				Name = x.ct.Name
			}).ToListAsync();
		}
	}
}