using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Languages;
using eShopFlower.ViewModels.System.Roles;

namespace eShopFlower.Application.System.Languages
{
	public interface ILanguageService
	{
		Task<ApiResult<List<LanguageViewModel>>> GetAll();
	}
}