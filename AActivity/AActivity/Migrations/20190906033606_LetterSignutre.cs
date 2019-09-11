using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class LetterSignutre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LetterSignutres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LetterId = table.Column<int>(nullable: false),
                    WhoHasSignutre = table.Column<int>(nullable: false),
                    SignatureId = table.Column<int>(nullable: false),
                    IsHeOwnerOfSignature = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterSignutres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterSignutres_Letters_LetterId",
                        column: x => x.LetterId,
                        principalTable: "Letters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LetterSignutres_Signatures_SignatureId",
                        column: x => x.SignatureId,
                        principalTable: "Signatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LetterSignutres_LetterId",
                table: "LetterSignutres",
                column: "LetterId");

            migrationBuilder.CreateIndex(
                name: "IX_LetterSignutres_SignatureId",
                table: "LetterSignutres",
                column: "SignatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterSignutres");
        }
    }
}
