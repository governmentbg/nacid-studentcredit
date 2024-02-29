using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class MakeColumnNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_country_nationalityid",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_institution_institutionid",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_researcharea_researchareaid",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_speciality_specialityid",
                table: "applicationone");

            migrationBuilder.AlterColumn<int>(
                name: "specialityid",
                table: "applicationone",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "researchareaid",
                table: "applicationone",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "paymentperiod",
                table: "applicationone",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "nationalityid",
                table: "applicationone",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "monthlypayment",
                table: "applicationone",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "institutionid",
                table: "applicationone",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expirationdateofgraceperiod",
                table: "applicationone",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "credittype",
                table: "applicationone",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_country_nationalityid",
                table: "applicationone",
                column: "nationalityid",
                principalTable: "country",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_institution_institutionid",
                table: "applicationone",
                column: "institutionid",
                principalTable: "institution",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_researcharea_researchareaid",
                table: "applicationone",
                column: "researchareaid",
                principalTable: "researcharea",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_speciality_specialityid",
                table: "applicationone",
                column: "specialityid",
                principalTable: "speciality",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_country_nationalityid",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_institution_institutionid",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_researcharea_researchareaid",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_speciality_specialityid",
                table: "applicationone");

            migrationBuilder.AlterColumn<int>(
                name: "specialityid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "researchareaid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "paymentperiod",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "nationalityid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "monthlypayment",
                table: "applicationone",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "institutionid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "expirationdateofgraceperiod",
                table: "applicationone",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "credittype",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_country_nationalityid",
                table: "applicationone",
                column: "nationalityid",
                principalTable: "country",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_institution_institutionid",
                table: "applicationone",
                column: "institutionid",
                principalTable: "institution",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_researcharea_researchareaid",
                table: "applicationone",
                column: "researchareaid",
                principalTable: "researcharea",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_speciality_specialityid",
                table: "applicationone",
                column: "specialityid",
                principalTable: "speciality",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
