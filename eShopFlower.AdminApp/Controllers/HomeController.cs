using System.Diagnostics;
using eShopFlower.AdminApp.Models;
using eShopFlower.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlower.AdminApp.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpPost]
		public IActionResult Language(NavigationViewModel viewModel)
		{
			HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, viewModel.CurrentLanguageId);
			//return Redirect(viewModel.ReturnUrl);
			return RedirectToAction("Index");
		}
	}
}