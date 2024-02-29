using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddEnumForRecontractionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reductionofloanamount",
                table: "applicationone");

            migrationBuilder.AddColumn<int>(
                name: "recontractiontype",
                table: "applicationone",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recontractiontype",
                table: "applicationone");

            migrationBuilder.AddColumn<bool>(
                name: "reductionofloanamount",
                table: "applicationone",
                type: "boolean",
                nullable: true);
        }
    }
}
