using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class Letters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Letters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripBookingId = table.Column<int>(nullable: false),
                    NoMersal = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    TypeLetter = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Letters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Letters_TripBookings_TripBookingId",
                        column: x => x.TripBookingId,
                        principalTable: "TripBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Letters_TripBookingId",
                table: "Letters",
                column: "TripBookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Letters");
        }
    }
}
