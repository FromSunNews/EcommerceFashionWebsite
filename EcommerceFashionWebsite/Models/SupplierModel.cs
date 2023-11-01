using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceFashionWebsite.Models
{
    public class SupplierModel
    {
        [Key]
        public int Id { get; set; }
        public string? UrlImage { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Addresss { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Country { get; set; }
        public string? HomePage { get; set; }

    }
}
