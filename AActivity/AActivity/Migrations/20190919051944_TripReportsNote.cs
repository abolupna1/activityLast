using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class TripReportsNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripReportsNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripReportId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    DateNotes = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripReportsNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripReportsNotes_TripReports_TripReportId",
                        column: x => x.TripReportId,
                        principalTable: "TripReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripReportsNotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripReportsNotes_TripReportId",
                table: "TripReportsNotes",
                column: "TripReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TripReportsNotes_UserId",
                table: "TripReportsNotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripReportsNotes");
        }
    }
}
