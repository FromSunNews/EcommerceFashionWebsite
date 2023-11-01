using EcommerceFashionWebsite.Models;
using System.ComponentModel.DataAnnotations;

namespace EcommerceFashionWebsite.Areas.Admin.ViewModels
{
    public class CreateSupplierViewModel
    {
        [Key]
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Addresss { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Country { get; set; }
        public string? HomePage { get; set; }
    }
}
