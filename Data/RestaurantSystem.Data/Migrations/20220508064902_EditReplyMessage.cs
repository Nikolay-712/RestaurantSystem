#nullable disable

namespace RestaurantSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EditReplyMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Replies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Replies");
        }
    }
}
