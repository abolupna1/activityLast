using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class LetterAdvancedDelegation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LetterAdvancedDelegations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LetterId = table.Column<int>(nullable: false),
                    TripDelegateId = table.Column<int>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    AmountAdditional = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterAdvancedDelegations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterAdvancedDelegations_Letters_LetterId",
                        column: x => x.LetterId,
                        principalTable: "Letters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LetterAdvancedDelegations_TripDelegates_TripDelegateId",
                        column: x => x.TripDelegateId,
                        principalTable: "TripDelegates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LetterAdvancedDelegations_LetterId",
                table: "LetterAdvancedDelegations",
                column: "LetterId");

            migrationBuilder.CreateIndex(
                name: "IX_LetterAdvancedDelegations_TripDelegateId",
                table: "LetterAdvancedDelegations",
                column: "TripDelegateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterAdvancedDelegations");
        }
    }
}
