using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using EcommerceFashionWebsite.ViewComponentsModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
	[Area("User")]
    
	public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            var deliveryMethod = (DeliveryMethodType)TempData["DeliveryMethod"];
            UserModel user = new UserModel() { 
                Email = (string)TempData["Email"],
                Address = (string)TempData["Address"],
                Name = (string)TempData["Name"],
                Phone = (string)TempData["Phone"]
            };

            if (deliveryMethod != null && user != null)
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
                    DeliveryMethod = deliveryMethod,
                    UserModel = user
                };

                return View(cartWishListModel);
            }
            return RedirectToAction("Index", "Cart", new { area = "User" });
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(CartWishListModel cartWishList)
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


            InvoiceModel invoice = new InvoiceModel() {
                ApplicationUserId = claim.Value,
                Total = totalPrice,
                PhoneNumber = cartWishList.UserModel.Phone,
                Address = cartWishList.UserModel.Address,
                Name = cartWishList.UserModel.Name,
                Email = cartWishList.UserModel.Email,
                DeliveryMethod = cartWishList.DeliveryMethod
            };

            _db.InvoiceModel.Add(invoice);
            await _db.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            // Sau khi lưu thay đổi, productModel.Id sẽ chứa ID của đối tượng mới được tạo.
            int newInvoiceId = invoice.Id;

            // tạo các Invoice Slice

            var invoiceSlice = _db.CartModel.Where(c => c.ApplicationUserId == claim.Value)
                    .Select(c => new InvoiceSliceModel
                    {
                        InvoiceId = newInvoiceId,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        ProductModel = c.ProductModel
                    }).ToList();

            _db.InvoiceSliceModel.AddRange(invoiceSlice);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Invoice", new { area = "User", InvoiceId = newInvoiceId });
        }
    }
}
