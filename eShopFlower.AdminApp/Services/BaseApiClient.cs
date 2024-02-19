using eShopFlower.Utilities.Constants;
using eShopFlower.ViewModels.Common;
using eShopFlower.ViewModels.System.Languages;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eShopFlower.AdminApp.Services
{
	public class BaseApiClient
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		protected BaseApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		protected async Task<List<T>> GetListAsync<T>(string url)//, bool requiredLogin = false
		{
			var session = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

			//var api_host = _configuration["api_host"];
			var response = await client.GetAsync(url);

			var body = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var data = (List<T>)JsonConvert.DeserializeObject(body, typeof(List<T>));
				return data;
			}
			throw new Exception(body);
		}

		protected async Task<TResponse> GetAsync<TResponse>(string url)//, bool requiredLogin = false
		{
			var session = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

			var api_host = _configuration["api_host"];
			var response = await client.GetAsync(api_host + url);

			var body = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				TResponse myDeserializeObjectList = (TResponse)JsonConvert.DeserializeObject(body, typeof(List<TResponse>));
				return myDeserializeObjectList;
			}
			return JsonConvert.DeserializeObject<TResponse>(body);
		}
	}
}