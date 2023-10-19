using System.ComponentModel.DataAnnotations;

namespace EcommerceFashionWebsite.Models
{
    public class ProductCategoryModel
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public virtual ProductModel? Product { get; set; }
        public virtual CategoryModel? Category { get; set; }
    }
}
