using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddBankNomenclatureToApplicationOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bankid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_bankid",
                table: "applicationone",
                column: "bankid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_bank_bankid",
                table: "applicationone",
                column: "bankid",
                principalTable: "bank",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_bank_bankid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_bankid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "bankid",
                table: "applicationone");
        }
    }
}
