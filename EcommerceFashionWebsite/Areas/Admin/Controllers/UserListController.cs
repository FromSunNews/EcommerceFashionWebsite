using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class UserListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
