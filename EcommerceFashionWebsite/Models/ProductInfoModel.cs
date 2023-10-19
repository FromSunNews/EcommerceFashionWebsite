using System.ComponentModel.DataAnnotations;

namespace EcommerceFashionWebsite.Models
{
    public class ProductInfoModel
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? IdealFor { get; set; }
        public string? StyleCode { get; set; }
        public string? ColorCode { get; set; }
        public string? Material { get; set; }
        public bool Waterproof { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
    }
}
