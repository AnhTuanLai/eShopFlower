using System.Net.Http.Headers;
using System.Net.Http;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Languages;
using eShopFlower.ViewModels.System.Roles;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using eShopFlower.ViewModels.System.Users;
using System.Text;

namespace eShopFlower.AdminApp.Services
{
	public class LanguageApiClient : BaseApiClient, ILanguageApiClient
	{
		public LanguageApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
			: base(httpClientFactory, configuration, contextAccessor)
		{
		}

		public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
		{
			return await GetAsync<ApiResult<List<LanguageViewModel>>>("api/languages");
		}
	}
}