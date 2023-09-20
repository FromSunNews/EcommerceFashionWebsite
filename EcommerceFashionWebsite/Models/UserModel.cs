using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EcommerceFashionWebsite.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 50 characters")]
        public string Username { get; set; }

        

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Range(12, 120, ErrorMessage = "Age must be between 18 and 120")]
        public int Age { get; set; }

        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender")]
        public Gender Gender { get; set; }

    }
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
