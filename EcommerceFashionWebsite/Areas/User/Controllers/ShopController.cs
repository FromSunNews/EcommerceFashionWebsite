using EcommerceFashionWebsite.Areas.Admin.ViewModels;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
	[Area("User")]
	public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ShopController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
           

            string categoryValue = HttpContext.Request.Query["CategoryId"];
            string typeValue = HttpContext.Request.Query["Type"];

            // Kiểm tra xem giá trị của 'Category' có tồn tại không
            // Chuyển đổi giá trị từ chuỗi sang số (nếu cần thiết)
            if (!string.IsNullOrEmpty(categoryValue) && int.TryParse(categoryValue, out var categoryId))
                {
                    var products = _db.ProductModel
                   .Include(p => p.Images)
                   .Include(p => p.Categories)
                   .ThenInclude(pc => pc.Category)
                   .Where(p => p.Categories.Any(pc => (int)pc.Category.Id == categoryId))
                   .ToList();

                    var productViewModels = products.Select(product => new ProductViewModel
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,

                        ImageUrls = new List<string>
                    {
                        _db.ImageModel
                         .Where(img => img.ProductId == product.Id)
                         .Select(img => img.ImageUrl)
                         .FirstOrDefault()
                    },
                        PriceApply = (int)product.PriceApply,
                        PriceOrigin = (int)product.PriceOrigin,
                        NumberInStock = product.NumberInStock,
                        StarRating = product.StarRating,

                        // Các thuộc tính khác của ProductViewModel bạn muốn hiển thị
                    }).ToList();
                
                    return View(productViewModels);
                }
            else if (!string.IsNullOrEmpty(typeValue) && Enum.TryParse<ProductType>(typeValue, out var typeId))
            {
                var products = _db.ProductModel
               .Include(p => p.Images)
               .Include(p => p.Categories)
               .ThenInclude(pc => pc.Category)
               .Where(p => p.Categories.Any(pc => pc.Category.Type == typeId))
               .ToList();

                var productViewModels = products.Select(product => new ProductViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,

                    ImageUrls = new List<string>
                    {
                        _db.ImageModel
                         .Where(img => img.ProductId == product.Id)
                         .Select(img => img.ImageUrl)
                         .FirstOrDefault()
                    },
                    PriceApply = (int)product.PriceApply,
                    PriceOrigin = (int)product.PriceOrigin,
                    NumberInStock = product.NumberInStock,
                    StarRating = product.StarRating,

                    // Các thuộc tính khác của ProductViewModel bạn muốn hiển thị
                }).ToList();

                return View(productViewModels);
            }
            else
            {
                var products = _db.ProductModel
           .Include(p => p.Images)
           .Include(p => p.Categories)
           .ThenInclude(pc => pc.Category)
           .ToList();

                var productViewModels = products.Select(product => new ProductViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,

                    ImageUrls = new List<string>
                {
                    _db.ImageModel
                     .Where(img => img.ProductId == product.Id)
                     .Select(img => img.ImageUrl)
                     .FirstOrDefault()
                },
                    PriceApply = (int)product.PriceApply,
                    PriceOrigin = (int)product.PriceOrigin,
                    NumberInStock = product.NumberInStock,
                    StarRating = product.StarRating,

                    // Các thuộc tính khác của ProductViewModel bạn muốn hiển thị
                }).ToList();

                return View(productViewModels);
            }
        }
    }
}
