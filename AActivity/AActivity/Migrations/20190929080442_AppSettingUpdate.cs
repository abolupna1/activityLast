using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class AppSettingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtyExtirnalBuses",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyIntirnalBuses",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyUmrahBuses",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyVisitExtirnalBuses",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyVisitIntirnalBuses",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyExtirnalBuses",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "QtyIntirnalBuses",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "QtyUmrahBuses",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "QtyVisitExtirnalBuses",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "QtyVisitIntirnalBuses",
                table: "AppSettings");
        }
    }
}
