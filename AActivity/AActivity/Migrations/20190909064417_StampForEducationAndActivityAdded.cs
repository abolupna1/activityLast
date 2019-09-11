using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class StampForEducationAndActivityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stamp",
                table: "EducationalBodies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Stamp",
                table: "AppSettings",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stamp",
                table: "EducationalBodies");

            migrationBuilder.DropColumn(
                name: "Stamp",
                table: "AppSettings");
        }
    }
}
