using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceFashionWebsite.Migrations
{
    /// <inheritdoc />
    public partial class updated3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "publicIdImage",
                table: "CategoryModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "publicIdImage",
                table: "CategoryModel");
        }
    }
}
