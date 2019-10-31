using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class FinishWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinishWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripBookingId = table.Column<int>(nullable: false),
                    TripDelegateId = table.Column<int>(nullable: false),
                    JopDegree = table.Column<string>(nullable: true),
                    DelegationNumber = table.Column<int>(nullable: false),
                    DateDelgation = table.Column<DateTime>(nullable: false),
                    DelegationBudy = table.Column<string>(nullable: true),
                    MyProperty = table.Column<int>(nullable: false),
                    TransportBuy = table.Column<bool>(nullable: false),
                    LivingBuy = table.Column<bool>(nullable: false),
                    FoodsBuy = table.Column<bool>(nullable: false),
                    TransportToToWorkBuy = table.Column<bool>(nullable: false),
                    CashAdvance = table.Column<bool>(nullable: false),
                    CashAdvanceAmont = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishWorks_TripBookings_TripBookingId",
                        column: x => x.TripBookingId,
                        principalTable: "TripBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishWorks_TripDelegates_TripDelegateId",
                        column: x => x.TripDelegateId,
                        principalTable: "TripDelegates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishWorks_TripBookingId",
                table: "FinishWorks",
                column: "TripBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishWorks_TripDelegateId",
                table: "FinishWorks",
                column: "TripDelegateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishWorks");
        }
    }
}
