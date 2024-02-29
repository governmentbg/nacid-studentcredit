using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddColumnForPaidByApplicationFour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "paidbyapplicationfour",
                table: "applicationone",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paidbyapplicationfour",
                table: "applicationone");
        }
    }
}
