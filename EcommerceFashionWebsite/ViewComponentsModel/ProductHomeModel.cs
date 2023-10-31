using EcommerceFashionWebsite.Areas.Admin.ViewModels;

namespace EcommerceFashionWebsite.ViewComponentsModel
{
    public class ProductHomeModel
    {
        public string CategoryName { get; set; }
        public ICollection<ProductViewModel> ProductViewModel { get; set; }
    }
}
