using EcommerceFashionWebsite.Areas.Admin.ViewModels;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using EcommerceFashionWebsite.ViewComponentsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceFashionWebsite.ViewComponents
{
    public class FemaleProductViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public FemaleProductViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var products = _db.ProductModel
                   .Include(p => p.Images)
                   .Include(p => p.Categories)
                   .ThenInclude(pc => pc.Category);

            var allCategory = _db.CategoryModel.Where(c => c.Type == ProductType.FEMALE).ToList();

            List<ProductHomeModel> productHomeModels = new List<ProductHomeModel>();

            foreach (var category in allCategory)
            {
                var productViewModels = products
                    .Where(p => p.Categories.Any(pc => pc.Category.Id == category.Id))
                    .Select(product => new ProductViewModel
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

                productHomeModels.Add(new ProductHomeModel()
                {
                    CategoryName= category.Name,
                    ProductViewModel = productViewModels
                });
            }


            return View(productHomeModels);
        }
    }
}

