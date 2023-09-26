using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
	[Area("User")]
	public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
