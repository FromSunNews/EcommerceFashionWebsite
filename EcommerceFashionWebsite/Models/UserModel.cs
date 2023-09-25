using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EcommerceFashionWebsite.Models
{
    public class UserModel: IdentityUser
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
