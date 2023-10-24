using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceFashionWebsite.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductCategoryModel_CategoryId",
                table: "ProductCategoryModel");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryModel_CategoryId",
                table: "ProductCategoryModel",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductCategoryModel_CategoryId",
                table: "ProductCategoryModel");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryModel_CategoryId",
                table: "ProductCategoryModel",
                column: "CategoryId",
                unique: true);
        }
    }
}
