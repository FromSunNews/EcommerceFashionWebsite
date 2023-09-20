using AspNetCoreHero.ToastNotification.Abstractions;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        public RegisterController(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                bool isValidUserName = !_db.UserModel.ToList().Exists(p => p.Username.Equals(userModel.Username));
                bool isValidEmail = !_db.UserModel.ToList().Exists(p => p.Email.Equals(userModel.Email));
                if (isValidUserName && isValidEmail)
                {
                    // Thêm thông tin user
                    _db.UserModel.Add(userModel);
                    // Lưu lại
                    _db.SaveChanges();
                    _notyf.Success("Create user successfully!");
                    return RedirectToAction("Index", "Login");
                } else
                {
                    // thong bao loi 
                    _notyf.Error("Username or email is already exist!");
                }
            }

            return View();
        }
    }
}
