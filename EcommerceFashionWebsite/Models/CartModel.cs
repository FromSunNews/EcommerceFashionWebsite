using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceFashionWebsite.Models
{
    public class CartModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductModel")]
        public int ProductId { get; set; }
        [ValidateNever]
        public ProductModel ProductModel { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("UserModel")]
        public string ApplicationUserId { get; set; }

        [ValidateNever]
        public UserModel UserModel { get; set; }

    }
}
