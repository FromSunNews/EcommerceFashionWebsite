using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EcommerceFashionWebsite.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsLiked { get; set; } = false;
        public int StarRating { get; set; } = 0;
        [ValidateNever]
        public virtual ICollection<ImageModel>? Images { get; set; }

        [ForeignKey("SupplierModel")]
        public int SupplierId { get; set; }

        [ValidateNever]
        public SupplierModel SupplierModel { get; set; }
        public string? Desc { get; set; }
        public float PriceApply { get; set; } = 0;
        public float PriceOrigin { get; set; } = 0;
        
        public Sizes Sizes { get; set; }
        public int NumberInStock { get; set; } = 0;
        [ValidateNever]
        public ICollection<ProductCategoryModel>? Categories { get; set; }
        public string? Tags { get; set; }
        public string? Introduction { get; set; }
        public string? Features { get; set; }
        [ValidateNever]
        public ProductInfoModel? ProductInfo { get; set; }
        [ValidateNever]
        public ICollection<ReviewModel>? Reviews { get; set; }
        public string Colors { get; set; }
    }

    public enum Sizes
    {
        S,
        M,
        L,
        XL,
        XXL,
        XXXL  
    }

}
