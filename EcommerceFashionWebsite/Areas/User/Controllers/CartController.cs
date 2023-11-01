using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using EcommerceFashionWebsite.ViewComponentsModel;
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
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            var carts = _db.CartModel.Where(c => c.ApplicationUserId == claim.Value)
                .Select(c => new CartModel
                {
                    Id = c.Id,
                    ApplicationUserId = c.ApplicationUserId,
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    ProductModel = c.ProductModel
                }).ToList();

            int totalPrice = 0;

            foreach (var cart in carts)
            {
                totalPrice += cart.Quantity * (int)cart.ProductModel.PriceApply;
            }
            var cartWishListModel = new CartWishListModel()
            {
                NumberWishList = 1,
                CartModels = _db.CartModel.Where(c => c.ApplicationUserId == claim.Value)
                .Select(c => new CartModel
                {
                    Id = c.Id,
                    ApplicationUserId = c.ApplicationUserId,
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    ProductModel = new ProductModel
                    {
                        Id = c.ProductModel.Id,
                        Name = c.ProductModel.Name,
                        Images = new List<ImageModel> {
                            _db.ImageModel.Where(img => img.ProductId == c.ProductModel.Id).FirstOrDefault()
                        },
                        StarRating = c.ProductModel.StarRating,
                        PriceApply = c.ProductModel.PriceApply,
                        PriceOrigin = c.ProductModel.PriceOrigin,
                        Sizes = c.ProductModel.Sizes,
                        Colors = c.ProductModel.Colors
                    }
                })
                .ToList(),
                TotalPrice = totalPrice
            };
            return View(cartWishListModel);
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
