using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class LetterSignutreForAdvance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LetterSignutreForAdvances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LetterAdvancedDelegationId = table.Column<int>(nullable: false),
                    WhoHasSignutre = table.Column<int>(nullable: false),
                    SignatureId = table.Column<int>(nullable: false),
                    IsHeOwnerOfSignature = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterSignutreForAdvances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterSignutreForAdvances_LetterAdvancedDelegations_LetterAdvancedDelegationId",
                        column: x => x.LetterAdvancedDelegationId,
                        principalTable: "LetterAdvancedDelegations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LetterSignutreForAdvances_Signatures_SignatureId",
                        column: x => x.SignatureId,
                        principalTable: "Signatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LetterSignutreForAdvances_LetterAdvancedDelegationId",
                table: "LetterSignutreForAdvances",
                column: "LetterAdvancedDelegationId");

            migrationBuilder.CreateIndex(
                name: "IX_LetterSignutreForAdvances_SignatureId",
                table: "LetterSignutreForAdvances",
                column: "SignatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterSignutreForAdvances");
        }
    }
}
