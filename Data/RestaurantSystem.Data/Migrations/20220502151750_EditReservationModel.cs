namespace RestaurantSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EditReservationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationStatus",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationStatus",
                table: "Reservations");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
