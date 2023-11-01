using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserListController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPhotoService _photoService;
        public UserListController(ApplicationDbContext db, IPhotoService photoService)
        {
            _db = db;
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            
            var users = _db.Users.ToList();
            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var userModel = _db.Users.Find(id);

            if (userModel != null)
            {
                _db.Users.Remove(userModel);
                _db.SaveChanges();

                return Json(new { success = true });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
