using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
