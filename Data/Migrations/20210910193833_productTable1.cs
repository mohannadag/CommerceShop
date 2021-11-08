using Microsoft.EntityFrameworkCore.Migrations;

namespace Commerce.Data.Migrations
{
    public partial class productTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Guid_Culture",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryCulture",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId_Culture",
                table: "Products",
                columns: new[] { "CategoryId", "Culture" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId_Culture",
                table: "Products",
                columns: new[] { "CategoryId", "Culture" },
                principalTable: "Categories",
                principalColumns: new[] { "Guid", "Culture" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId_Culture",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId_Culture",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CategoryCulture",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Guid_Culture",
                table: "Products",
                columns: new[] { "Guid", "Culture" },
                principalTable: "Categories",
                principalColumns: new[] { "Guid", "Culture" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
