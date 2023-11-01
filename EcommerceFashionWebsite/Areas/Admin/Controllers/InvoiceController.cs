using EcommerceFashionWebsite.Areas.Admin.ViewModels;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceFashionWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InvoiceController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<InvoiceModel> invoiceModels = _db.InvoiceModel.Select(i => new InvoiceModel()
            {
                Id = i.Id,
                ApplicationUser = _db.UserModel.Where(u => u.Id == i.ApplicationUserId).SingleOrDefault(),
                InvoiceSliceModels = _db.InvoiceSliceModel.Where(item => item.InvoiceId == i.Id).Select(item => new InvoiceSliceModel()
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
            }).ToList();

            return View(invoiceModels);
        }

        public IActionResult Detail(int id)
        {
            InvoiceModel invoiceModel = _db.InvoiceModel.Where(i => i.Id == id).Select(i => new InvoiceModel()
            {
                Id = i.Id,
                ApplicationUser = _db.UserModel.Where(u => u.Id == i.ApplicationUserId).SingleOrDefault(),
                InvoiceSliceModels = _db.InvoiceSliceModel.Where(item => item.InvoiceId == id).Select(item => new InvoiceSliceModel()
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            InvoiceModel invoiceModel = _db.InvoiceModel.Where(i => i.Id == id).Select(i => new InvoiceModel()
            {
                Id = i.Id,
                ApplicationUserId = i.ApplicationUserId,
                ApplicationUser = _db.UserModel.Where(u => u.Id == i.ApplicationUserId).SingleOrDefault(),
                InvoiceSliceModels = _db.InvoiceSliceModel.Where(item => item.InvoiceId == id).Select(item => new InvoiceSliceModel()
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

        [HttpPost]
        public async Task<IActionResult> EditAsync(InvoiceModel invoiceModel)
        {
                InvoiceModel iModel = new InvoiceModel() { 
                    Id= invoiceModel.Id,
                    ApplicationUserId = invoiceModel.ApplicationUserId,
                    PhoneNumber = invoiceModel.PhoneNumber,
                    Email = invoiceModel.Email,
                    Name = invoiceModel.Name,
                    Address = invoiceModel.Address,
                    OrderStatus = invoiceModel.OrderStatus,
                    Total = invoiceModel.Total
                };


                _db.InvoiceModel.Update(iModel);

                _db.SaveChanges();
                // Hiển thị dnah sách thể loại
                return RedirectToAction("Index", "Invoice", new { area = "Admin" });
        }
    }
}
