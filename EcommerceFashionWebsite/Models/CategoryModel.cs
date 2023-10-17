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
    }

    public enum ProductType
    {
        FEMALE,
        MALE,
        UNISEX
    }
}
