using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class appsettingUpdateUmrah : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QtyOmrahDaysTrip",
                table: "AppSettings",
                newName: "QtyOmrahMedinaDaysTrip");

            migrationBuilder.AddColumn<int>(
                name: "QtyOmrahMakkahDaysTrip",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyOmrahMakkahDaysTrip",
                table: "AppSettings");

            migrationBuilder.RenameColumn(
                name: "QtyOmrahMedinaDaysTrip",
                table: "AppSettings",
                newName: "QtyOmrahDaysTrip");
        }
    }
}
