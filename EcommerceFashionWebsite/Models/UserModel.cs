using Microsoft.AspNetCore.Identity;

namespace EcommerceFashionWebsite.Models
{
    public class UserModel: IdentityUser
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
