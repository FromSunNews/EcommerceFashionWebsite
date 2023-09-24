using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceFashionWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> UserModel { get; set; } 
        public DbSet<CategoryModel> CategoryModel { get; set; }
        public DbSet<ProductModel> ProductModel { get; set; }
    }
}