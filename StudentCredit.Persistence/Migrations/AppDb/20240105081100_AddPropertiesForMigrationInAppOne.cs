using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddPropertiesForMigrationInAppOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "migrationresearchareaname",
                table: "applicationone",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "migrationspecialityname",
                table: "applicationone",
                type: "text",
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
                name: "migrationresearchareaname",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "migrationspecialityname",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "secondnationalityid",
                table: "applicationone");
        }
    }
}
