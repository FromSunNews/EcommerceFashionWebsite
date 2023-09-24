using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
	[Area("User")]
	public class Shopping_CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
