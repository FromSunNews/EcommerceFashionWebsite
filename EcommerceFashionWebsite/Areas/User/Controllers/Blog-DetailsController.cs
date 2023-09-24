using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
	[Area("User")]
	public class Blog_DetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
