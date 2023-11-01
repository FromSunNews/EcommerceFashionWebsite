using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using EcommerceFashionWebsite.ViewComponentsModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
	[Area("User")]
    
	public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CheckoutController(ApplicationDbContext db)
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

            var useModel = _db.UserModel.Where(u => u.Id == claim.Value).SingleOrDefault();

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
                TotalPrice = totalPrice,
                UserModel = useModel
            };
            return View(cartWishListModel);
        }

        [HttpPost]
        public ActionResult Index(CartWishListModel cartWishList)
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

            var useModel = _db.UserModel.Where(u => u.Id == claim.Value).SingleOrDefault();

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
                TotalPrice = totalPrice,
                DeliveryMethod = cartWishList.DeliveryMethod,
                UserModel = cartWishList.UserModel
            };

            TempData["DeliveryMethod"] = cartWishList.DeliveryMethod;
            TempData["Name"] = cartWishList.UserModel.Name;
            TempData["Address"] = cartWishList.UserModel.Address;
            TempData["Email"] = cartWishList.UserModel.Email;
            TempData["Phone"] = cartWishList.UserModel.Phone;

            return RedirectToAction("Index", "Payment", new { area = "User" }); // Chuyển hướng đến action Index trong PaymentController
        }
    }
}
