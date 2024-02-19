using eShopFlower.ViewModels.System.Languages;

namespace eShopFlower.AdminApp.Models
{
	public class NavigationViewModel
	{
		public List<LanguageViewModel> Languages { get; set; }

		public string? CurrentLanguageId { get; set; }
	}
}