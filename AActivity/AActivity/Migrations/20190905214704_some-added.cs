using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class someadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "TripDelegates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmployeMobile",
                table: "TripDelegates",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "AmountVisitCreditToTrip",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "SignutreDelegates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SignatureId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    StartAtDate = table.Column<DateTime>(nullable: false),
                    EndAtDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignutreDelegates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignutreDelegates_Signatures_SignatureId",
                        column: x => x.SignatureId,
                        principalTable: "Signatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignutreDelegates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignutreDelegates_SignatureId",
                table: "SignutreDelegates",
                column: "SignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_SignutreDelegates_UserId",
                table: "SignutreDelegates",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignutreDelegates");

            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "TripDelegates");

            migrationBuilder.DropColumn(
                name: "EmployeMobile",
                table: "TripDelegates");

            migrationBuilder.DropColumn(
                name: "AmountVisitCreditToTrip",
                table: "AppSettings");
        }
    }
}
