using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
