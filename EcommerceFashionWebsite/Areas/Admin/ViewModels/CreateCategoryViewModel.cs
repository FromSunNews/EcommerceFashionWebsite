using EcommerceFashionWebsite.Models;
using System.ComponentModel.DataAnnotations;

namespace EcommerceFashionWebsite.Areas.Admin.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }

        public ProductType Type { get; set; }
        public string? Desc { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }
    }
}
