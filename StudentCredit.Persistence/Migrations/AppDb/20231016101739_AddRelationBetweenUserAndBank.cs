using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddRelationBetweenUserAndBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bankid",
                table: "user",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_bankid",
                table: "user",
                column: "bankid");

            migrationBuilder.AddForeignKey(
                name: "FK_user_bank_bankid",
                table: "user",
                column: "bankid",
                principalTable: "bank",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_bank_bankid",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_bankid",
                table: "user");

            migrationBuilder.DropColumn(
                name: "bankid",
                table: "user");
        }
    }
}
