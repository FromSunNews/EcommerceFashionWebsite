using EcommerceFashionWebsite.Models;

namespace EcommerceFashionWebsite.ViewComponentsModel
{
    public class CategoryViewComponentModel
    {
        public ICollection<CategoryModel> CategoriesWomen { get; set; }
        public ICollection<CategoryModel> CategoriesMen { get; set; }
        public ICollection<CategoryModel> CategoriesUnisex { get; set; }
        public ICollection<CategoryModel> CategoriesKid { get; set; }
    }
}
