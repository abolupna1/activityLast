using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class AppSettingsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AmountExternalCreditToTrip",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AmountInternalCreditToTrip",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AmountOmrahCreditToTrip",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "QtyExternalDaysTrip",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyInternalDaysTrip",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyOmrahDaysTrip",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountExternalCreditToTrip",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "AmountInternalCreditToTrip",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "AmountOmrahCreditToTrip",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "QtyExternalDaysTrip",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "QtyInternalDaysTrip",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "QtyOmrahDaysTrip",
                table: "AppSettings");
        }
    }
}
