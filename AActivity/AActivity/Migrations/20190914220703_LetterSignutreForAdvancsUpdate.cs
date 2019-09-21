using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class LetterSignutreForAdvancsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LetterId",
                table: "LetterSignutreForAdvances",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LetterSignutreForAdvances_LetterId",
                table: "LetterSignutreForAdvances",
                column: "LetterId");

            migrationBuilder.AddForeignKey(
                name: "FK_LetterSignutreForAdvances_Letters_LetterId",
                table: "LetterSignutreForAdvances",
                column: "LetterId",
                principalTable: "Letters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LetterSignutreForAdvances_Letters_LetterId",
                table: "LetterSignutreForAdvances");

            migrationBuilder.DropIndex(
                name: "IX_LetterSignutreForAdvances_LetterId",
                table: "LetterSignutreForAdvances");

            migrationBuilder.DropColumn(
                name: "LetterId",
                table: "LetterSignutreForAdvances");
        }
    }
}
