using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class fixfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfourattachedfile_applicationfour_applicationfour~",
                table: "applicationfourattachedfile");

            migrationBuilder.DropColumn(
                name: "applicaitonfourid",
                table: "applicationfourattachedfile");

            migrationBuilder.AlterColumn<int>(
                name: "applicationfourid",
                table: "applicationfourattachedfile",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfourattachedfile_applicationfour_applicationfour~",
                table: "applicationfourattachedfile",
                column: "applicationfourid",
                principalTable: "applicationfour",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfourattachedfile_applicationfour_applicationfour~",
                table: "applicationfourattachedfile");

            migrationBuilder.AlterColumn<int>(
                name: "applicationfourid",
                table: "applicationfourattachedfile",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "applicaitonfourid",
                table: "applicationfourattachedfile",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfourattachedfile_applicationfour_applicationfour~",
                table: "applicationfourattachedfile",
                column: "applicationfourid",
                principalTable: "applicationfour",
                principalColumn: "id");
        }
    }
}
