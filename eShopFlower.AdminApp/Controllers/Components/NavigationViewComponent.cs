using eShopFlower.AdminApp.Models;
using eShopFlower.AdminApp.Services;
using eShopFlower.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlower.AdminApp.Controllers.Components
{
	public class NavigationViewComponent : ViewComponent
	{
		private readonly ILanguageApiClient _languageApiClient;

		public NavigationViewComponent(ILanguageApiClient languageApiClient)
		{
			_languageApiClient = languageApiClient;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var languages = await _languageApiClient.GetAll();
			var navigationVM = new NavigationViewModel()
			{
				CurrentLanguageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId),
				Languages = languages.ResultObject
			};

			return View("Default", navigationVM);
		}
	}
}