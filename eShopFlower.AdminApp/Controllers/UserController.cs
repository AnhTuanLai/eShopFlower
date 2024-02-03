using eShopFlower.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlower.AdminApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            return View(request);
        }
    }
}