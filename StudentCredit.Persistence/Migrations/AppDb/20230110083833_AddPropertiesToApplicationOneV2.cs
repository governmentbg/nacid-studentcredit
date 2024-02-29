using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddPropertiesToApplicationOneV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "educationalqualificationid",
                table: "applicationone",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "educationformtypeid",
                table: "applicationone",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "interestdue",
                table: "applicationone",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "overallsize",
                table: "applicationone",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "principalabsorbed",
                table: "applicationone",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_educationalqualificationid",
                table: "applicationone",
                column: "educationalqualificationid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_educationformtypeid",
                table: "applicationone",
                column: "educationformtypeid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_educationalqualification_educationalqualific~",
                table: "applicationone",
                column: "educationalqualificationid",
                principalTable: "educationalqualification",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_educationformtype_educationformtypeid",
                table: "applicationone",
                column: "educationformtypeid",
                principalTable: "educationformtype",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_educationalqualification_educationalqualific~",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_educationformtype_educationformtypeid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_educationalqualificationid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_educationformtypeid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "educationalqualificationid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "educationformtypeid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "interestdue",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "overallsize",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "principalabsorbed",
                table: "applicationone");
        }
    }
}
