using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddPropertiesToApplicationOneV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "extensionofgraceperiod",
                table: "applicationone",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "interestdueinoldbank",
                table: "applicationone",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "newexpirationdateofgraceperiod",
                table: "applicationone",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "overallsizeinoldbank",
                table: "applicationone",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "principalabsorbedinoldbank",
                table: "applicationone",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "reductionofloanamount",
                table: "applicationone",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "extensionofgraceperiod",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "interestdueinoldbank",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "newexpirationdateofgraceperiod",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "overallsizeinoldbank",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "principalabsorbedinoldbank",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "reductionofloanamount",
                table: "applicationone");
        }
    }
}
