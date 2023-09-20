using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Controllers
{
    public class FagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
