using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using EcommerceFashionWebsite.ViewComponentsModel;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.ViewComponents
{
    public class CategoryViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public CategoryViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            CategoryViewComponentModel categoryVC = new CategoryViewComponentModel
            {
                CategoriesWomen = _db.CategoryModel
                        .Where(c => c.Type == ProductType.FEMALE)
                        .ToList(),
                CategoriesMen = _db.CategoryModel
                        .Where(c => c.Type == ProductType.MALE)
                        .ToList(),
                CategoriesUnisex = _db.CategoryModel
                        .Where(c => c.Type == ProductType.UNISEX)
                        .ToList(),
                CategoriesKid = _db.CategoryModel
                        .Where(c => c.Type == ProductType.KID)
                        .ToList(),
            };

            return View(categoryVC);
        }
    }
}
