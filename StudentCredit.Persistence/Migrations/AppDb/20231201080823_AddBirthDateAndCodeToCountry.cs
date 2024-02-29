using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddBirthDateAndCodeToCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "country",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "birthdate",
                table: "applicationone",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "country");

            migrationBuilder.DropColumn(
                name: "birthdate",
                table: "applicationone");
        }
    }
}
