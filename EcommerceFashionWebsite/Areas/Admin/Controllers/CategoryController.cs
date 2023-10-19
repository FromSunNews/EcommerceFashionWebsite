using EcommerceFashionWebsite.Areas.Admin.ViewModels;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Interfaces;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPhotoService _photoService;
        public CategoryController(ApplicationDbContext db, IPhotoService photoService)
        {
            _db = db;
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            // Hiển thị dnah sách thể loại
            var Category = _db.CategoryModel;
            ViewBag.Categories = Category;
            return View();
        }

        public IActionResult Add()
        {
            // Hiển thị dnah sách thể loại
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateCategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(categoryVM.Image);

                var categoryModel = new CategoryModel
                {
                    Name = categoryVM.Name,
                    Type = categoryVM.Type,
                    Desc = categoryVM.Desc,
                    urlImage = result.Url.ToString(),
                    publicIdImage = result.PublicId.ToString()
                };
                _db.CategoryModel.Add(categoryModel);
                _db.SaveChanges();
                // Hiển thị dnah sách thể loại
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id) { 
            if (id == 0)
            {
                return NotFound();
            }

            var categoryModel = _db.CategoryModel.Find(id);

            var categoryVM = new CreateCategoryViewModel
            {
                Name = categoryModel.Name,
                Type = categoryModel.Type,
                Desc = categoryModel.Desc,
                ImageURL = categoryModel.urlImage
            };
            return View(categoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(CreateCategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                string urlImage = categoryVM.ImageURL;
                if (categoryVM.Image != null && categoryVM.Image.Length > 0)
                {
                var result = await _photoService.AddPhotoAsync(categoryVM.Image);
                    urlImage = result.Url.ToString();
                }

                var categoryModel = new CategoryModel
                {
                    Name = categoryVM.Name,
                    Type = categoryVM.Type,
                    Desc = categoryVM.Desc,
                    urlImage = urlImage
                };
                _db.CategoryModel.Update(categoryModel);

                _db.SaveChanges();
                // Hiển thị dnah sách thể loại
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var categoryModel = _db.CategoryModel.Find(id);

            if (categoryModel != null)
            {
                if (categoryModel.publicIdImage != null)
                {
                    await _photoService.DeletePhotoAsync(categoryModel.publicIdImage);
                }
                _db.CategoryModel.Remove(categoryModel);
                _db.SaveChanges();

                return Json(new { success = true });
            } else
            {
                return NotFound();
            }
        }
    }
}
