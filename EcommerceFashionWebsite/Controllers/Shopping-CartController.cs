using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Controllers
{
    public class Shopping_CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
