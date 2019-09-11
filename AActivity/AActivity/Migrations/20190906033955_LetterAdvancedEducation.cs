using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class LetterAdvancedEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LetterAdvancedEducations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LetterId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    AmountAdditional = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterAdvancedEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterAdvancedEducations_Letters_LetterId",
                        column: x => x.LetterId,
                        principalTable: "Letters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LetterAdvancedEducations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LetterAdvancedEducations_LetterId",
                table: "LetterAdvancedEducations",
                column: "LetterId");

            migrationBuilder.CreateIndex(
                name: "IX_LetterAdvancedEducations_UserId",
                table: "LetterAdvancedEducations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterAdvancedEducations");
        }
    }
}
