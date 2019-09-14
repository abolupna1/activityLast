using Microsoft.EntityFrameworkCore.Migrations;

namespace AActivity.Migrations
{
    public partial class LetterAdvancedDelegationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LetterAdvancedDelegations_TripDelegates_TripDelegateId",
                table: "LetterAdvancedDelegations");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterAdvancedEducations_Letters_LetterId",
                table: "LetterAdvancedEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterAdvancedEducations_AspNetUsers_UserId",
                table: "LetterAdvancedEducations");

            migrationBuilder.DropIndex(
                name: "IX_LetterAdvancedDelegations_TripDelegateId",
                table: "LetterAdvancedDelegations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LetterAdvancedEducations",
                table: "LetterAdvancedEducations");

            migrationBuilder.RenameTable(
                name: "LetterAdvancedEducations",
                newName: "LetterAdvancedEducation");

            migrationBuilder.RenameColumn(
                name: "TripDelegateId",
                table: "LetterAdvancedDelegations",
                newName: "QtyStudents");

            migrationBuilder.RenameIndex(
                name: "IX_LetterAdvancedEducations_UserId",
                table: "LetterAdvancedEducation",
                newName: "IX_LetterAdvancedEducation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LetterAdvancedEducations_LetterId",
                table: "LetterAdvancedEducation",
                newName: "IX_LetterAdvancedEducation_LetterId");

            migrationBuilder.AddColumn<string>(
                name: "CreditToEMployee",
                table: "LetterAdvancedDelegations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeMobile",
                table: "LetterAdvancedDelegations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "LetterAdvancedDelegations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Statatus",
                table: "LetterAdvancedDelegations",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LetterAdvancedEducation",
                table: "LetterAdvancedEducation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LetterAdvancedEducation_Letters_LetterId",
                table: "LetterAdvancedEducation",
                column: "LetterId",
                principalTable: "Letters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterAdvancedEducation_AspNetUsers_UserId",
                table: "LetterAdvancedEducation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LetterAdvancedEducation_Letters_LetterId",
                table: "LetterAdvancedEducation");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterAdvancedEducation_AspNetUsers_UserId",
                table: "LetterAdvancedEducation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LetterAdvancedEducation",
                table: "LetterAdvancedEducation");

            migrationBuilder.DropColumn(
                name: "CreditToEMployee",
                table: "LetterAdvancedDelegations");

            migrationBuilder.DropColumn(
                name: "EmployeeMobile",
                table: "LetterAdvancedDelegations");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "LetterAdvancedDelegations");

            migrationBuilder.DropColumn(
                name: "Statatus",
                table: "LetterAdvancedDelegations");

            migrationBuilder.RenameTable(
                name: "LetterAdvancedEducation",
                newName: "LetterAdvancedEducations");

            migrationBuilder.RenameColumn(
                name: "QtyStudents",
                table: "LetterAdvancedDelegations",
                newName: "TripDelegateId");

            migrationBuilder.RenameIndex(
                name: "IX_LetterAdvancedEducation_UserId",
                table: "LetterAdvancedEducations",
                newName: "IX_LetterAdvancedEducations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LetterAdvancedEducation_LetterId",
                table: "LetterAdvancedEducations",
                newName: "IX_LetterAdvancedEducations_LetterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LetterAdvancedEducations",
                table: "LetterAdvancedEducations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LetterAdvancedDelegations_TripDelegateId",
                table: "LetterAdvancedDelegations",
                column: "TripDelegateId");

            migrationBuilder.AddForeignKey(
                name: "FK_LetterAdvancedDelegations_TripDelegates_TripDelegateId",
                table: "LetterAdvancedDelegations",
                column: "TripDelegateId",
                principalTable: "TripDelegates",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterAdvancedEducations_Letters_LetterId",
                table: "LetterAdvancedEducations",
                column: "LetterId",
                principalTable: "Letters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterAdvancedEducations_AspNetUsers_UserId",
                table: "LetterAdvancedEducations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
