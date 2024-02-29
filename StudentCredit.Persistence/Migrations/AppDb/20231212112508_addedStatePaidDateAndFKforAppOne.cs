using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class addedStatePaidDateAndFKforAppOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "paidbyapplicationfourdate",
                table: "applicationone",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationfour_applicationoneid",
                table: "applicationfour",
                column: "applicationoneid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfour_applicationone_applicationoneid",
                table: "applicationfour",
                column: "applicationoneid",
                principalTable: "applicationone",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfour_applicationone_applicationoneid",
                table: "applicationfour");

            migrationBuilder.DropIndex(
                name: "IX_applicationfour_applicationoneid",
                table: "applicationfour");

            migrationBuilder.DropColumn(
                name: "paidbyapplicationfourdate",
                table: "applicationone");
        }
    }
}
