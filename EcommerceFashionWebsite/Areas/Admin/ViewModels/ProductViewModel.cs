using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Areas.Admin.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public List<string> ImageUrls { get; set; } // Danh sách các URL hình ảnh
        public List<string> CategoryNames { get; set; } // Danh sách tên các danh mục sản phẩm
        public int PriceApply { get; set; }
        public int PriceOrigin { get; set; }
        public Sizes Sizes { get; set; }
        public int NumberInStock { get; set; }
        public string Tags { get; set; }
        public string Introduction { get; set; }
        public string Features { get; set; }
        public string Colors { get; set; }
        public string Desc{ get; set; }

        public ICollection<ProductViewModel>? RelatedProducts { get; set; }
        public int StarRating { get; set; }
        public int Quantity { get; set; } = 1;

    }

}
