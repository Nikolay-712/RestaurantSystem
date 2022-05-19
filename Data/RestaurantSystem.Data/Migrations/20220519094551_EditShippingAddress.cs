using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantSystem.Data.Migrations
{
    public partial class EditShippingAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Orders_Id",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Orders",
                newName: "ShippingAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddress",
                table: "Orders",
                newName: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Orders_Id",
                table: "Addresses",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
