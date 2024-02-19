using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.Data.EF;
using eShopFlower.Data.Entities;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Languages;
using eShopFlower.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eShopFlower.Application.System.Languages
{
	public class LanguageService : ILanguageService
	{
		private readonly IConfiguration _configuration;
		private readonly eShopFlowerDbContext _context;

		public LanguageService(IConfiguration configuration, eShopFlowerDbContext context)
		{
			_configuration = configuration;
			_context = context;
		}

		public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
		{
			var languages = await _context.Languages.Select(x => new LanguageViewModel()
			{
				Id = x.Id,
				Name = x.Name,
				IsDefault = x.IsDefault
			}).ToListAsync();

			return new ApiSuccessResult<List<LanguageViewModel>>(languages);
		}
	}
}