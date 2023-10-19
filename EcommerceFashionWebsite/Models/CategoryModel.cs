using System.ComponentModel.DataAnnotations;

namespace EcommerceFashionWebsite.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ProductType Type { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? urlImage { get; set; }
        public string? publicIdImage { get; set; }
        public string? Desc { get; set; }

        public virtual ProductCategoryModel? ProductCategory { get; set; }
    }

    public enum ProductType
    {
        FEMALE,
        MALE,
        UNISEX
    }
}
