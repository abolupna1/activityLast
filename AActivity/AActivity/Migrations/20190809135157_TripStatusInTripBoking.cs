using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class TripStatusInTripBoking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripStatus",
                table: "TripBookings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripStatus",
                table: "TripBookings");
        }
    }
}
