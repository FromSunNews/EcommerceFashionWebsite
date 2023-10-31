using EcommerceFashionWebsite.Areas.Admin.ViewModels;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using EcommerceFashionWebsite.ViewComponentsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace EcommerceFashionWebsite.ViewComponents
{
    [Authorize]
    public class CartWishListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public CartWishListViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
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
                        //ProductModel = _db.ProductModel.Where(p => p.Id == c.ProductId).SingleOrDefault()
                    }).ToList();

                int totalPrice = 0;

                foreach (var cart in carts)
                {
                    totalPrice += cart.Quantity * (int)cart.ProductModel.PriceApply;
                }
                var cartWishListModel = new CartWishListModel() {
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
                            PriceApply = c.ProductModel.PriceApply
                        }
                    })
                    .ToList(),
                    TotalPrice = totalPrice
                };
                return View(cartWishListModel);
            }
            else
            {
                return View();
            }
        }
    }
}

