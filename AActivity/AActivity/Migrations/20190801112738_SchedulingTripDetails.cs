using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class SchedulingTripDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchedulingTripDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EducationalBodyId = table.Column<int>(nullable: false),
                    SchedulingTripHeadId = table.Column<int>(nullable: false),
                    TripTypeId = table.Column<int>(nullable: false),
                    TripDate = table.Column<DateTime>(nullable: false),
                    Notce = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingTripDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulingTripDetails_EducationalBodies_EducationalBodyId",
                        column: x => x.EducationalBodyId,
                        principalTable: "EducationalBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SchedulingTripDetails_SchedulingTripHead_SchedulingTripHeadId",
                        column: x => x.SchedulingTripHeadId,
                        principalTable: "SchedulingTripHead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchedulingTripDetails_TripTypes_TripTypeId",
                        column: x => x.TripTypeId,
                        principalTable: "TripTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingTripDetails_EducationalBodyId",
                table: "SchedulingTripDetails",
                column: "EducationalBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingTripDetails_SchedulingTripHeadId",
                table: "SchedulingTripDetails",
                column: "SchedulingTripHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingTripDetails_TripTypeId",
                table: "SchedulingTripDetails",
                column: "TripTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulingTripDetails");
        }
    }
}
