using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceFashionWebsite.Models
{
    public class ProductCategoryModel
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        [ValidateNever]
        public virtual ProductModel? Product { get; set; }
        [ValidateNever]
        public virtual CategoryModel? Category { get; set; }
    }
}
