using eShopFlower.ViewModels.Common;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using eShopFlower.ViewModels.Catalog.Products;
using eShopFlower.Utilities.Constants;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace eShopFlower.AdminApp.Services
{
	public class ProductApiClient : BaseApiClient, IProductApiClient
	{
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
			: base(httpClientFactory, configuration, contextAccessor)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
		{
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var session = _contextAccessor.HttpContext.Session.GetString("Token");

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

			var json = JsonConvert.SerializeObject(request);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
			var api_host = _configuration["api_host"];
			var response = await client.PutAsync(api_host + $"api/users/{id}/categories", httpContent);
			var result = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
			return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
		}

		public async Task<bool> CreateProduct(ProductCreateRequest request)
		{
			var session = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
			var languageId = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

			var requestContent = new MultipartFormDataContent();

			if (request.ThumbnailImage != null)
			{
				byte[] data;
				using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
				{
					data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
				}
				ByteArrayContent bytes = new ByteArrayContent(data);
				requestContent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
			}

			requestContent.Add(new StringContent(request.Price.ToString()), "price");
			requestContent.Add(new StringContent(request.OriginalPrice.ToString()), "originalPrice");
			requestContent.Add(new StringContent(request.Stock.ToString()), "stock");
			requestContent.Add(new StringContent(request.Name.ToString()), "name");
			requestContent.Add(new StringContent(request.Description.ToString()), "description");
			requestContent.Add(new StringContent(request.SeoDescription.ToString()), "seoDescription");
			requestContent.Add(new StringContent(request.SeoTitle.ToString()), "seoTitle");
			requestContent.Add(new StringContent(request.SeoAlias.ToString()), "seoAlias");
			requestContent.Add(new StringContent(languageId), "languageId");

			var api_host = _configuration["api_host"];
			var response = await client.PostAsync(api_host + $"/api/products", requestContent);

			return response.IsSuccessStatusCode;
		}

		public async Task<ProductViewModel> GetById(int id, string languageId)
		{
			var data = await GetAsync<ProductViewModel>($"api/products/{id}/{languageId}");

			return data;
		}

		public async Task<PagedResult<ProductViewModel>> GetPagings(GetManageProductPadingRequest request)
		{
			var data = await GetAsync<PagedResult<ProductViewModel>>($"api/products/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}" +
				$"&keyword={request.Keyword}&languageId={request.LanguageId}&categoryId={request.CategoryId}");

			return data;
		}
	}
}