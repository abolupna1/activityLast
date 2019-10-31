using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class DelegateToSignutre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DelegateToSignutres",
                columns: table => new
                {
                    WonerSignutreId = table.Column<int>(nullable: false),
                    DelegatedToSignutreId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    DateDelegate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelegateToSignutres", x => new { x.WonerSignutreId, x.DelegatedToSignutreId });
                    table.ForeignKey(
                        name: "FK_DelegateToSignutres_Signatures_DelegatedToSignutreId",
                        column: x => x.DelegatedToSignutreId,
                        principalTable: "Signatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DelegateToSignutres_Signatures_WonerSignutreId",
                        column: x => x.WonerSignutreId,
                        principalTable: "Signatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DelegateToSignutres_DelegatedToSignutreId",
                table: "DelegateToSignutres",
                column: "DelegatedToSignutreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelegateToSignutres");
        }
    }
}
