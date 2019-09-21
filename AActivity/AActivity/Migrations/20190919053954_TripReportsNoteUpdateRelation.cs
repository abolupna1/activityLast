using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class TripReportsNoteUpdateRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WhoIsWriteNote",
                table: "TripReportsNotes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhoIsWriteNote",
                table: "TripReportsNotes");
        }
    }
}
