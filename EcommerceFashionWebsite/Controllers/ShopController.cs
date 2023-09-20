using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
