using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class ChangeNationalityPropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_country_secondnationalityid",
                table: "applicationone");

            migrationBuilder.RenameColumn(
                name: "secondnationalityid",
                table: "applicationone",
                newName: "othernationalityid");

            migrationBuilder.RenameIndex(
                name: "IX_applicationone_secondnationalityid",
                table: "applicationone",
                newName: "IX_applicationone_othernationalityid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_country_othernationalityid",
                table: "applicationone",
                column: "othernationalityid",
                principalTable: "country",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_country_othernationalityid",
                table: "applicationone");

            migrationBuilder.RenameColumn(
                name: "othernationalityid",
                table: "applicationone",
                newName: "secondnationalityid");

            migrationBuilder.RenameIndex(
                name: "IX_applicationone_othernationalityid",
                table: "applicationone",
                newName: "IX_applicationone_secondnationalityid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_country_secondnationalityid",
                table: "applicationone",
                column: "secondnationalityid",
                principalTable: "country",
                principalColumn: "id");
        }
    }
}
