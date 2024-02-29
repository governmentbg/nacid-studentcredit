using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class addedapp4histprytype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "applicationfourhistorytype",
                table: "applicationfourhistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "applicationfourhistorytype",
                table: "applicationfourhistory");
        }
    }
}
