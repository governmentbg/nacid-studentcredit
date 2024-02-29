using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class app4specialitynullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfour_speciality_specialityid",
                table: "applicationfour");

            migrationBuilder.AlterColumn<int>(
                name: "specialityid",
                table: "applicationfour",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfour_speciality_specialityid",
                table: "applicationfour",
                column: "specialityid",
                principalTable: "speciality",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfour_speciality_specialityid",
                table: "applicationfour");

            migrationBuilder.AlterColumn<int>(
                name: "specialityid",
                table: "applicationfour",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfour_speciality_specialityid",
                table: "applicationfour",
                column: "specialityid",
                principalTable: "speciality",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
