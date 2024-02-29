using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddPropertiesToApplicationOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "course",
                table: "applicationone");

            migrationBuilder.AddColumn<int>(
                name: "doctoralyear",
                table: "applicationone",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "studentcourse",
                table: "applicationone",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uan",
                table: "applicationone",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "doctoralyear",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "studentcourse",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "uan",
                table: "applicationone");

            migrationBuilder.AddColumn<int>(
                name: "course",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
