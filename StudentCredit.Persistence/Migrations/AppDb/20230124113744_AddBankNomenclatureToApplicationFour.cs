using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddBankNomenclatureToApplicationFour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bankname",
                table: "applicationfour");

            migrationBuilder.AddColumn<int>(
                name: "bankid",
                table: "applicationfour",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_applicationfour_bankid",
                table: "applicationfour",
                column: "bankid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfour_bank_bankid",
                table: "applicationfour",
                column: "bankid",
                principalTable: "bank",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfour_bank_bankid",
                table: "applicationfour");

            migrationBuilder.DropIndex(
                name: "IX_applicationfour_bankid",
                table: "applicationfour");

            migrationBuilder.DropColumn(
                name: "bankid",
                table: "applicationfour");

            migrationBuilder.AddColumn<string>(
                name: "bankname",
                table: "applicationfour",
                type: "text",
                nullable: true);
        }
    }
}
