using Microsoft.EntityFrameworkCore.Migrations;

namespace Commerce.Data.Migrations
{
    public partial class productTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryCulture",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryCulture",
                table: "Products");
        }
    }
}
