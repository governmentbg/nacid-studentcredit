using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class changedpropname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfour_hypotesistype_hypotesistypeid",
                table: "applicationfour");

            migrationBuilder.DropTable(
                name: "hypotesistype");

            migrationBuilder.RenameColumn(
                name: "hypotesistypeid",
                table: "applicationfour",
                newName: "applicationfourtypeid");

            migrationBuilder.RenameIndex(
                name: "IX_applicationfour_hypotesistypeid",
                table: "applicationfour",
                newName: "IX_applicationfour_applicationfourtypeid");

            migrationBuilder.CreateTable(
                name: "applicationfourtype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationfourtype", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfour_applicationfourtype_applicationfourtypeid",
                table: "applicationfour",
                column: "applicationfourtypeid",
                principalTable: "applicationfourtype",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfour_applicationfourtype_applicationfourtypeid",
                table: "applicationfour");

            migrationBuilder.DropTable(
                name: "applicationfourtype");

            migrationBuilder.RenameColumn(
                name: "applicationfourtypeid",
                table: "applicationfour",
                newName: "hypotesistypeid");

            migrationBuilder.RenameIndex(
                name: "IX_applicationfour_applicationfourtypeid",
                table: "applicationfour",
                newName: "IX_applicationfour_hypotesistypeid");

            migrationBuilder.CreateTable(
                name: "hypotesistype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    alias = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hypotesistype", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfour_hypotesistype_hypotesistypeid",
                table: "applicationfour",
                column: "hypotesistypeid",
                principalTable: "hypotesistype",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
