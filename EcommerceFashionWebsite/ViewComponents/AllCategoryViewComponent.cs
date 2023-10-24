using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using EcommerceFashionWebsite.ViewComponentsModel;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.ViewComponents
{
    public class AllCategoryViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public AllCategoryViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            ICollection<CategoryModel> category = _db.CategoryModel.ToList();

            return View(category);
        }
    }
}
