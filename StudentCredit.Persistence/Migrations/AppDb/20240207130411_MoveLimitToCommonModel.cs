using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class MoveLimitToCommonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "limit",
                table: "sheetyear",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "limit",
                table: "monthdata",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "limit",
                table: "sheetyear");

            migrationBuilder.DropColumn(
                name: "limit",
                table: "monthdata");
        }
    }
}
