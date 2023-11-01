using EcommerceFashionWebsite.Areas.Admin.ViewModels;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Interfaces;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPhotoService _photoService;

        public SupplierController(ApplicationDbContext db, IPhotoService photoService)
        {
            _db = db;
            _photoService = photoService;
        }

        public IActionResult Index()
        {
            // Hiển thị danh sách nhà cc
            var supplier = _db.SupplierModel;
            ViewBag.Suppliers = supplier;
            return View();
        }

        public IActionResult Add()
        {
            // Hiển thị dnah sách thể loại
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateSupplierViewModel supplierVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(supplierVM.Image);


                var supplierModel = new SupplierModel
                {
                    CompanyName = supplierVM.CompanyName,
                    ContactName = supplierVM.ContactName,
                    Addresss = supplierVM.Addresss,
                    Email = supplierVM.Email,
                    Phone = supplierVM.Phone,
                    Country = supplierVM.Country,
                    HomePage = supplierVM.HomePage,
                    UrlImage = result.Url.ToString()
                };
                _db.SupplierModel.Add(supplierModel);
                _db.SaveChanges();
                // Hiển thị dnah sách thể loại
                return RedirectToAction("Index", "Supplier", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var supplierModel = _db.SupplierModel.Find(id);

            var supplierVM = new CreateSupplierViewModel
            {
                Id = supplierModel.Id,
                CompanyName = supplierModel.CompanyName,
                ContactName = supplierModel.ContactName,
                Addresss = supplierModel.Addresss,
                Email = supplierModel.Email,
                Phone = supplierModel.Phone,
                Country = supplierModel.Country,
                HomePage = supplierModel.HomePage,
                ImageURL = supplierModel.UrlImage
            };
            return View(supplierVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(CreateSupplierViewModel supplierVM)
        {
            if (ModelState.IsValid)
            {
                string urlImage = supplierVM.ImageURL;
                if (supplierVM.Image != null && supplierVM.Image.Length > 0)
                {
                    var result = await _photoService.AddPhotoAsync(supplierVM.Image);
                    urlImage = result.Url.ToString();
                }

                var supplierModel = new SupplierModel
                {
                    Id=supplierVM.Id,
                    CompanyName = supplierVM.CompanyName,
                    ContactName = supplierVM.ContactName,
                    Addresss = supplierVM.Addresss,
                    Email = supplierVM.Email,
                    Phone = supplierVM.Phone,
                    Country = supplierVM.Country,
                    HomePage = supplierVM.HomePage,
                    UrlImage = urlImage
                };
                _db.SupplierModel.Update(supplierModel);

                _db.SaveChanges();
                // Hiển thị dnah sách thể loại
                return RedirectToAction("Index", "Supplier", new { area = "Admin" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var supplierModel = _db.SupplierModel.Find(id);

            if (supplierModel != null)
            {
                _db.SupplierModel.Remove(supplierModel);
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
