using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceFashionWebsite.Models
{
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductModel")]
        public int ProductId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
