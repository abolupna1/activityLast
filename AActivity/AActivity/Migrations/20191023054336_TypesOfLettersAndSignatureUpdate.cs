using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class TypesOfLettersAndSignatureUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndAtDate",
                table: "TypesOfLettersAndSignatures");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TypesOfLettersAndSignatures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndAtDate",
                table: "TypesOfLettersAndSignatures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "TypesOfLettersAndSignatures",
                nullable: false,
                defaultValue: false);
        }
    }
}
