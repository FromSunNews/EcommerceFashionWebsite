using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceFashionWebsite.Models
{
    public class InvoiceDetailModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("InvoiceModel")]
        public int InvoiceId { get; set; }

        [ValidateNever]
        public InvoiceModel InvoiceModel { get; set; }


        [ForeignKey("ProductModel")]
        public int ProductId { get; set; }

        [ValidateNever]
        public ProductModel ProductModel { get; set; }
        public int Quantity { get; set; }
    }
}
