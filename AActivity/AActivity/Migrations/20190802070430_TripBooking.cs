using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class TripBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripBookings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchedulingTripDetailId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    TripToDate = table.Column<DateTime>(nullable: false),
                    TripQtyDays = table.Column<int>(nullable: false),
                    TripLocationName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripBookings_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripBookings_SchedulingTripDetails_SchedulingTripDetailId",
                        column: x => x.SchedulingTripDetailId,
                        principalTable: "SchedulingTripDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripBookings_CityId",
                table: "TripBookings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TripBookings_SchedulingTripDetailId",
                table: "TripBookings",
                column: "SchedulingTripDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripBookings");
        }
    }
}
