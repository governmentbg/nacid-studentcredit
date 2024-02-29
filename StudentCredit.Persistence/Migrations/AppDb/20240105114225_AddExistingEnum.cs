using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddExistingEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_country_othernationalityid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_othernationalityid",
                table: "applicationone");

            migrationBuilder.RenameColumn(
                name: "othernationalityid",
                table: "applicationone",
                newName: "specialityenum");

            migrationBuilder.AddColumn<int>(
                name: "researchareaenum",
                table: "applicationone",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "secondnationalityid",
                table: "applicationone",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_secondnationalityid",
                table: "applicationone",
                column: "secondnationalityid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_country_secondnationalityid",
                table: "applicationone",
                column: "secondnationalityid",
                principalTable: "country",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_country_secondnationalityid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_secondnationalityid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "researchareaenum",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "secondnationalityid",
                table: "applicationone");

            migrationBuilder.RenameColumn(
                name: "specialityenum",
                table: "applicationone",
                newName: "othernationalityid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_othernationalityid",
                table: "applicationone",
                column: "othernationalityid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_country_othernationalityid",
                table: "applicationone",
                column: "othernationalityid",
                principalTable: "country",
                principalColumn: "id");
        }
    }
}
