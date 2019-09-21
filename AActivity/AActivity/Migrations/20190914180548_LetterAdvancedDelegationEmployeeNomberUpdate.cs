using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class LetterAdvancedDelegationEmployeeNomberUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeNomber",
                table: "LetterAdvancedDelegations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeNomber",
                table: "LetterAdvancedDelegations");
        }
    }
}
