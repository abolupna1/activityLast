using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class StudentsParticipatingInTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsParticipatingInTrip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentNumber = table.Column<int>(nullable: false),
                    StudentName = table.Column<string>(nullable: false),
                    TripBookingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsParticipatingInTrip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsParticipatingInTrip_TripBookings_TripBookingId",
                        column: x => x.TripBookingId,
                        principalTable: "TripBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsParticipatingInTrip_TripBookingId",
                table: "StudentsParticipatingInTrip",
                column: "TripBookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsParticipatingInTrip");
        }
    }
}
