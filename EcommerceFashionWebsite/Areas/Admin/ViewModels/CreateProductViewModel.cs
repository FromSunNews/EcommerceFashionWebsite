using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EcommerceFashionWebsite.Areas.Admin.ViewModels
{
    public class CreateProductViewModel
    {
        public int Id { get; set; }
        [Required]
            public string Name { get; set; }
            

            public ICollection<IFormFile>? Images { get; set; }
            public List<string>? ImageUrls { get; set; }
        public List<string>? SelectedCategories { get; set; }

        [ValidateNever]
        public List<SelectListItem>? CategoriesSelectList { get; set; }

        public int SupplierId { get; set; }

        [ValidateNever]
        public List<SelectListItem>? SupplierSelectedList { get; set; }

        public string? Desc { get; set; }
        [Required]
        public int PriceApply { get; set; } = 0;
        [Required]
        public int PriceOrigin { get; set; } = 0;
        [Required]
        public Sizes Sizes { get; set; }
        [Required]
        public int NumberInStock { get; set; } = 0;
            public string? Tags { get; set; }
            public string? Introduction { get; set; }
            public string? Features { get; set; }
        [Required]
        public string Colors { get; set; } = "#456575";

    }
}
