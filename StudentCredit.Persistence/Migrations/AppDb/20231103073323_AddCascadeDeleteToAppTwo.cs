using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteToAppTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recordentry_applicationtwo_applicationtwoid",
                table: "recordentry");

            migrationBuilder.AddForeignKey(
                name: "FK_recordentry_applicationtwo_applicationtwoid",
                table: "recordentry",
                column: "applicationtwoid",
                principalTable: "applicationtwo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recordentry_applicationtwo_applicationtwoid",
                table: "recordentry");

            migrationBuilder.AddForeignKey(
                name: "FK_recordentry_applicationtwo_applicationtwoid",
                table: "recordentry",
                column: "applicationtwoid",
                principalTable: "applicationtwo",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
