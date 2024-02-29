using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddMigrationSpecialityToAppFour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "migrationspecialityname",
                table: "applicationfour",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "specialityenum",
                table: "applicationfour",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "migrationspecialityname",
                table: "applicationfour");

            migrationBuilder.DropColumn(
                name: "specialityenum",
                table: "applicationfour");
        }
    }
}
