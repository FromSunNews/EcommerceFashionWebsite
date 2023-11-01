using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
        [Area("User")]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InvoiceController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int InvoiceId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            InvoiceModel invoiceModel = _db.InvoiceModel.Where(i => i.Id == InvoiceId && i.ApplicationUserId == claim.Value).Select(i => new InvoiceModel()
            {
                Id = i.Id,
                ApplicationUser = _db.UserModel.Where(u => u.Id == i.ApplicationUserId).SingleOrDefault(),
                InvoiceSliceModels = _db.InvoiceSliceModel.Where(item =>  item.InvoiceId == InvoiceId).Select(item => new InvoiceSliceModel()
                {
                    Quantity = item.Quantity,
                    ProductModel = _db.ProductModel.Where(p => p.Id == item.ProductId).SingleOrDefault()
                }).ToList(),
                Total = i.Total,
                OrderDate = i.OrderDate,
                OrderStatus = i.OrderStatus,
                PhoneNumber = i.PhoneNumber,
                Name = i.Name,
                Address = i.Address,
                DeliveryMethod = i.DeliveryMethod,
                Discount = i.Discount
            }).SingleOrDefault();


            return View(invoiceModel);
        }
    }
}
