using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class TripTransportSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripTransportSignatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LetterId = table.Column<int>(nullable: false),
                    StatusSignature = table.Column<int>(nullable: false),
                    SignatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripTransportSignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripTransportSignatures_Letters_LetterId",
                        column: x => x.LetterId,
                        principalTable: "Letters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripTransportSignatures_Signatures_SignatureId",
                        column: x => x.SignatureId,
                        principalTable: "Signatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripTransportSignatures_LetterId",
                table: "TripTransportSignatures",
                column: "LetterId");

            migrationBuilder.CreateIndex(
                name: "IX_TripTransportSignatures_SignatureId",
                table: "TripTransportSignatures",
                column: "SignatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripTransportSignatures");
        }
    }
}
