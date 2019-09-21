using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class TripReportAndTripReportImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterAdvancedEducation");

            migrationBuilder.CreateTable(
                name: "TripReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripBookingId = table.Column<int>(nullable: false),
                    TextBody = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripReports_TripBookings_TripBookingId",
                        column: x => x.TripBookingId,
                        principalTable: "TripBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripReportImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripReportId = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripReportImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripReportImages_TripReports_TripReportId",
                        column: x => x.TripReportId,
                        principalTable: "TripReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripReportImages_TripReportId",
                table: "TripReportImages",
                column: "TripReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TripReports_TripBookingId",
                table: "TripReports",
                column: "TripBookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripReportImages");

            migrationBuilder.DropTable(
                name: "TripReports");

            migrationBuilder.CreateTable(
                name: "LetterAdvancedEducation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(nullable: false),
                    AmountAdditional = table.Column<float>(nullable: false),
                    LetterId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterAdvancedEducation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterAdvancedEducation_Letters_LetterId",
                        column: x => x.LetterId,
                        principalTable: "Letters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LetterAdvancedEducation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LetterAdvancedEducation_LetterId",
                table: "LetterAdvancedEducation",
                column: "LetterId");

            migrationBuilder.CreateIndex(
                name: "IX_LetterAdvancedEducation_UserId",
                table: "LetterAdvancedEducation",
                column: "UserId");
        }
    }
}
