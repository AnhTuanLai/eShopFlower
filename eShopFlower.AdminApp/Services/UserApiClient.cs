using System.Net.Http.Headers;
using System.Text;
using System.Text.Unicode;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Users;
using Newtonsoft.Json;

namespace eShopFlower.AdminApp.Services
{
	public class UserApiClient : IUserApiClient
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		public async Task<ApiResult<string>> Authenticate(LoginRequest request)
		{
			var json = JsonConvert.SerializeObject(request);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var api_host = _configuration["api_host"];
			var response = await client.PostAsync(api_host + "api/users/authenticate", httpContent);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
			}
			return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
		}

		public async Task<ApiResult<bool>> Delete(Guid id)
		{
			var session = _contextAccessor.HttpContext.Session.GetString("Token");
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
			var api_host = _configuration["api_host"];
			var response = await client.DeleteAsync(api_host + $"api/users/{id}");

			var body = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
			}

			return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
		}

		public async Task<ApiResult<UserViewModel>> GetById(Guid id)
		{
			var session = _contextAccessor.HttpContext.Session.GetString("Token");
			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

			var api_host = _configuration["api_host"];
			var response = await client.GetAsync(api_host + $"api/users/{id}");

			var body = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<ApiSuccessResult<UserViewModel>>(body);
			}

			return JsonConvert.DeserializeObject<ApiErrorResult<UserViewModel>>(body);
		}

		public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPagings(GetUsersPagingRequest request)
		{
			var session = _contextAccessor.HttpContext.Session.GetString("Token");
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

			var api_host = _configuration["api_host"];
			var response = await client.GetAsync(api_host + $"api/users/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");

			var body = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserViewModel>>>(body);
			}

			return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<UserViewModel>>>(body);
		}

		public async Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest)
		{
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			var json = JsonConvert.SerializeObject(registerRequest);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
			var api_host = _configuration["api_host"];
			var response = await client.PostAsync(api_host + $"api/users", httpContent);
			var result = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
			return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
		}

		public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
		{
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var session = _contextAccessor.HttpContext.Session.GetString("Token");

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

			var json = JsonConvert.SerializeObject(request);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
			var api_host = _configuration["api_host"];
			var response = await client.PutAsync(api_host + $"api/users/{id}/roles", httpContent);
			var result = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
			return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
		}

		public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest updateRequest)
		{
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var session = _contextAccessor.HttpContext.Session.GetString("Token");

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

			var json = JsonConvert.SerializeObject(updateRequest);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
			var api_host = _configuration["api_host"];
			var response = await client.PutAsync(api_host + $"api/users/{id}", httpContent);
			var result = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
			return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
		}
	}
}