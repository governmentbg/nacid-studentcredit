using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class bankasnomenclature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "alias",
                table: "bank",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "bank",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "vieworder",
                table: "bank",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alias",
                table: "bank");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "bank");

            migrationBuilder.DropColumn(
                name: "vieworder",
                table: "bank");
        }
    }
}
