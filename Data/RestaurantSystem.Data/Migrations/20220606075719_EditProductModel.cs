namespace RestaurantSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EditProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InDalyMenu",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InDalyMenu",
                table: "Products");
        }
    }
}
