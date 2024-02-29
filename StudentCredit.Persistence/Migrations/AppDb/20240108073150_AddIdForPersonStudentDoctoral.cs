using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddIdForPersonStudentDoctoral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "personstudentdoctoralid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "personstudentdoctoralid",
                table: "applicationfour",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "personstudentdoctoralid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "personstudentdoctoralid",
                table: "applicationfour");
        }
    }
}
