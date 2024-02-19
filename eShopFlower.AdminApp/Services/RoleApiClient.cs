using System.Net.Http.Headers;
using System.Net.Http;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Roles;
using eShopFlower.ViewModels.System.Users;
using Newtonsoft.Json;

namespace eShopFlower.AdminApp.Services
{
	public class RoleApiClient : IRoleApiClient
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public RoleApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		public async Task<ApiResult<List<RoleViewModel>>> GetAll()
		{
			var session = _contextAccessor.HttpContext.Session.GetString("Token");
			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

			var api_host = _configuration["api_host"];
			var response = await client.GetAsync(api_host + $"api/roles");

			var body = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				List<RoleViewModel> myDeserializeObjList = (List<RoleViewModel>)JsonConvert.DeserializeObject(body, typeof(List<RoleViewModel>));
				return new ApiSuccessResult<List<RoleViewModel>>(myDeserializeObjList);
			}

			return JsonConvert.DeserializeObject<ApiErrorResult<List<RoleViewModel>>>(body);
		}
	}
}