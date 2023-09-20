using AspNetCoreHero.ToastNotification.Abstractions;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        public LoginController(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserModel userModel)
        {
                bool isValidUserName = _db.UserModel.ToList().Exists(p => p.Username.Equals(userModel.Username));
                bool isValidPassword = _db.UserModel.ToList().Exists(p => p.Password.Equals(userModel.Password));
                if (isValidUserName && isValidPassword)
                {
                    _notyf.Success("Login successfully!");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // thong bao loi 
                    _notyf.Error("Username or password is wrong!");
                }
            return View(userModel);
        }
    }
}
