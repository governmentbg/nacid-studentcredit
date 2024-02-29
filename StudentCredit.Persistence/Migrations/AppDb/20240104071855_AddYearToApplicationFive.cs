using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddYearToApplicationFive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "yearid",
                table: "applicationfive",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationfive_yearid",
                table: "applicationfive",
                column: "yearid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfive_year_yearid",
                table: "applicationfive",
                column: "yearid",
                principalTable: "year",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfive_year_yearid",
                table: "applicationfive");

            migrationBuilder.DropIndex(
                name: "IX_applicationfive_yearid",
                table: "applicationfive");

            migrationBuilder.DropColumn(
                name: "yearid",
                table: "applicationfive");
        }
    }
}
