using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class LetterFoodAddsomefiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtyStudents",
                table: "LetterFoods",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LetterFoods",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LetterFoods_UserId",
                table: "LetterFoods",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LetterFoods_AspNetUsers_UserId",
                table: "LetterFoods",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LetterFoods_AspNetUsers_UserId",
                table: "LetterFoods");

            migrationBuilder.DropIndex(
                name: "IX_LetterFoods_UserId",
                table: "LetterFoods");

            migrationBuilder.DropColumn(
                name: "QtyStudents",
                table: "LetterFoods");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LetterFoods");
        }
    }
}
