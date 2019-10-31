using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class uodatetypeletter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Letters_TypeLetter",
                table: "Letters",
                column: "TypeLetter");

            migrationBuilder.AddForeignKey(
                name: "FK_Letters_TypesOfletters_TypeLetter",
                table: "Letters",
                column: "TypeLetter",
                principalTable: "TypesOfletters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Letters_TypesOfletters_TypeLetter",
                table: "Letters");

            migrationBuilder.DropIndex(
                name: "IX_Letters_TypeLetter",
                table: "Letters");
        }
    }
}
