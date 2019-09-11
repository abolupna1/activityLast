using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class LetterFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LetterFoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LetterId = table.Column<int>(nullable: false),
                    QtyMeals = table.Column<int>(nullable: false),
                    FirstMealDate = table.Column<DateTime>(nullable: false),
                    LastMealDate = table.Column<DateTime>(nullable: false),
                    FirstMealTime = table.Column<string>(nullable: true),
                    LastMealTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterFoods_Letters_LetterId",
                        column: x => x.LetterId,
                        principalTable: "Letters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LetterFoods_LetterId",
                table: "LetterFoods",
                column: "LetterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterFoods");
        }
    }
}
