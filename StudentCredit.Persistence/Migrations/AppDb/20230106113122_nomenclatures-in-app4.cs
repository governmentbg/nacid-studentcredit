using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class nomenclaturesinapp4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "institution",
                table: "applicationfour");

            migrationBuilder.DropColumn(
                name: "speciality",
                table: "applicationfour");

            migrationBuilder.AddColumn<int>(
                name: "institutionid",
                table: "applicationfour",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "specialityid",
                table: "applicationfour",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_applicationfour_institutionid",
                table: "applicationfour",
                column: "institutionid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationfour_specialityid",
                table: "applicationfour",
                column: "specialityid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfour_institution_institutionid",
                table: "applicationfour",
                column: "institutionid",
                principalTable: "institution",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfour_speciality_specialityid",
                table: "applicationfour",
                column: "specialityid",
                principalTable: "speciality",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfour_institution_institutionid",
                table: "applicationfour");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationfour_speciality_specialityid",
                table: "applicationfour");

            migrationBuilder.DropIndex(
                name: "IX_applicationfour_institutionid",
                table: "applicationfour");

            migrationBuilder.DropIndex(
                name: "IX_applicationfour_specialityid",
                table: "applicationfour");

            migrationBuilder.DropColumn(
                name: "institutionid",
                table: "applicationfour");

            migrationBuilder.DropColumn(
                name: "specialityid",
                table: "applicationfour");

            migrationBuilder.AddColumn<string>(
                name: "institution",
                table: "applicationfour",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "speciality",
                table: "applicationfour",
                type: "text",
                nullable: true);
        }
    }
}
