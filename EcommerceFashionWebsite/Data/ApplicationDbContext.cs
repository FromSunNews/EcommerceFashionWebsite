using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace EcommerceFashionWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductModel> ProductModel { get; set; }

        // =======================================
        public DbSet<ImageModel> ImageModel { get; set; }

        // ======================================
        public DbSet<CategoryModel> CategoryModel { get; set; }
        public DbSet<ProductCategoryModel> ProductCategoryModel { get; set; }

        // =======================================
        public DbSet<ReviewModel> ReviewModel { get; set; }
        // =========================================
        public DbSet<UserModel> UserModel { get; set; } 
        //==========================================
        public DbSet<CartModel> CartModel { get; set; }


        public DbSet<ProductInfoModel> ProductInfoModel { get; set; }

        public DbSet<InvoiceModel> InvoiceModel { get; set; }
        public DbSet<InvoiceDetailModel> InvoiceDetailModel { get; set; }
        public DbSet<AdditionalServiceModel> AdditionalServiceModel { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}