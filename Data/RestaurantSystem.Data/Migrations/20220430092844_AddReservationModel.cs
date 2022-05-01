#nullable disable

namespace RestaurantSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddReservationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeopleCount = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RestaurantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RestaurantId",
                table: "Reservations",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
