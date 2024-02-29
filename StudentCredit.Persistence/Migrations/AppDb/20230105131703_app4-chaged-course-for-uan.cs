using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class app4chagedcourseforuan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "course",
                table: "applicationfour");

            migrationBuilder.AddColumn<string>(
                name: "uan",
                table: "applicationfour",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uan",
                table: "applicationfour");

            migrationBuilder.AddColumn<int>(
                name: "course",
                table: "applicationfour",
                type: "integer",
                nullable: true);
        }
    }
}
