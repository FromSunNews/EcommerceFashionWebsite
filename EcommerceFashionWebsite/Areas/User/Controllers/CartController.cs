using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
	[Area("User")]
    
	public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int cartId)
        {
            if (cartId != 0)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
                // check cartId
                var exsistCart = _db.CartModel.Where(c => c.Id == cartId && c.ApplicationUserId == claim.Value).SingleOrDefault();
                if (exsistCart != null)
                {
                    _db.CartModel.Remove(exsistCart);
                    _db.SaveChanges();
                }
                else
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true });
            } else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public IActionResult Increase(int cartId)
        {
            if (cartId != 0)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
                // check cartId
                var exsistCart = _db.CartModel.Where(c => c.Id == cartId && c.ApplicationUserId == claim.Value).SingleOrDefault();
                if (exsistCart != null)
                {
                    exsistCart.Quantity += 1;
                    _db.CartModel.Update(exsistCart);
                    _db.SaveChanges();
                }
                else
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public IActionResult Decrease(int cartId)
        {
            if (cartId != 0)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
                // check cartId
                var exsistCart = _db.CartModel.Where(c => c.Id == cartId && c.ApplicationUserId == claim.Value).SingleOrDefault();
                if (exsistCart != null)
                {
                    exsistCart.Quantity -= 1;
                    _db.CartModel.Update(exsistCart);
                    _db.SaveChanges();
                }
                else
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
