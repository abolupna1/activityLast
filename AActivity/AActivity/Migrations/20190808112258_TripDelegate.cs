using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class TripDelegate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripDelegates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeNumber = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: false),
                    JopName = table.Column<string>(nullable: false),
                    TripBookingId = table.Column<int>(nullable: false),
                    IsFromEducationBody = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripDelegates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripDelegates_TripBookings_TripBookingId",
                        column: x => x.TripBookingId,
                        principalTable: "TripBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripDelegates_TripBookingId",
                table: "TripDelegates",
                column: "TripBookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripDelegates");
        }
    }
}
