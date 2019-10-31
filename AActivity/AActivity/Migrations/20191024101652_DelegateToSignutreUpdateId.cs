using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class DelegateToSignutreUpdateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DelegateToSignutres",
                table: "DelegateToSignutres");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DelegateToSignutres",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DelegateToSignutres",
                table: "DelegateToSignutres",
                columns: new[] { "Id", "WonerSignutreId", "DelegatedToSignutreId" });

            migrationBuilder.CreateIndex(
                name: "IX_DelegateToSignutres_WonerSignutreId",
                table: "DelegateToSignutres",
                column: "WonerSignutreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DelegateToSignutres",
                table: "DelegateToSignutres");

            migrationBuilder.DropIndex(
                name: "IX_DelegateToSignutres_WonerSignutreId",
                table: "DelegateToSignutres");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DelegateToSignutres");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DelegateToSignutres",
                table: "DelegateToSignutres",
                columns: new[] { "WonerSignutreId", "DelegatedToSignutreId" });
        }
    }
}
