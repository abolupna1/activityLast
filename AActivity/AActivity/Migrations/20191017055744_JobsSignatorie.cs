using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class JobsSignatorie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignatureRole",
                table: "Signatures");

            migrationBuilder.AddColumn<int>(
                name: "JobsSignatorieId",
                table: "Signatures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobsSignatories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobsSignatories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_JobsSignatorieId",
                table: "Signatures",
                column: "JobsSignatorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Signatures_JobsSignatories_JobsSignatorieId",
                table: "Signatures",
                column: "JobsSignatorieId",
                principalTable: "JobsSignatories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signatures_JobsSignatories_JobsSignatorieId",
                table: "Signatures");

            migrationBuilder.DropTable(
                name: "JobsSignatories");

            migrationBuilder.DropIndex(
                name: "IX_Signatures_JobsSignatorieId",
                table: "Signatures");

            migrationBuilder.DropColumn(
                name: "JobsSignatorieId",
                table: "Signatures");

            migrationBuilder.AddColumn<string>(
                name: "SignatureRole",
                table: "Signatures",
                nullable: false,
                defaultValue: "");
        }
    }
}
