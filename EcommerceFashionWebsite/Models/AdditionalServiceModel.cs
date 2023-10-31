using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceFashionWebsite.Models
{
    public class AdditionalServiceModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("InvoiceModel")]
        public int InvoiceId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
