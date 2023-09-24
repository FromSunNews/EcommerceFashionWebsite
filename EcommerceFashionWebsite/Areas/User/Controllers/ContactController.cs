using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Controllers
{
	[Area("User")]
	public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
