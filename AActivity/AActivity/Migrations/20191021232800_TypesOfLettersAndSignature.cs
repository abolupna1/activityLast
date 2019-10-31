using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class TypesOfLettersAndSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.CreateTable(
                name: "TypesOfLettersAndSignatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SignatureId = table.Column<int>(nullable: false),
                    TypesOfletterId = table.Column<int>(nullable: false),
                    WonerSignatureId = table.Column<int>(nullable: true),
                    IsSignatureOwner = table.Column<bool>(nullable: false),
                    StartAtDate = table.Column<DateTime>(nullable: false),
                    EndAtDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfLettersAndSignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypesOfLettersAndSignatures_Signatures_SignatureId",
                        column: x => x.SignatureId,
                        principalTable: "Signatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypesOfLettersAndSignatures_TypesOfletters_TypesOfletterId",
                        column: x => x.TypesOfletterId,
                        principalTable: "TypesOfletters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypesOfLettersAndSignatures_TypesOfLettersAndSignatures_WonerSignatureId",
                        column: x => x.WonerSignatureId,
                        principalTable: "TypesOfLettersAndSignatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfLettersAndSignatures_SignatureId",
                table: "TypesOfLettersAndSignatures",
                column: "SignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfLettersAndSignatures_TypesOfletterId",
                table: "TypesOfLettersAndSignatures",
                column: "TypesOfletterId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfLettersAndSignatures_WonerSignatureId",
                table: "TypesOfLettersAndSignatures",
                column: "WonerSignatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypesOfLettersAndSignatures");

        


     
        }
    }
}
