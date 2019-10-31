using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class appsettingUpdateVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtyDaysVisitEternal",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyDaysVisitInternal",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyDaysVisitEternal",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "QtyDaysVisitInternal",
                table: "AppSettings");
        }
    }
}
