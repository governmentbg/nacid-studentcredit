using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class ChangeFromEnumToNomenclature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "extensionofgraceperiod",
                table: "applicationone",
                newName: "extensionofgraceperiodid");

            migrationBuilder.CreateTable(
                name: "extensionofgraceperiod",
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
                    table.PrimaryKey("PK_extensionofgraceperiod", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_extensionofgraceperiodid",
                table: "applicationone",
                column: "extensionofgraceperiodid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_extensionofgraceperiod_extensionofgraceperio~",
                table: "applicationone",
                column: "extensionofgraceperiodid",
                principalTable: "extensionofgraceperiod",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_extensionofgraceperiod_extensionofgraceperio~",
                table: "applicationone");

            migrationBuilder.DropTable(
                name: "extensionofgraceperiod");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_extensionofgraceperiodid",
                table: "applicationone");

            migrationBuilder.RenameColumn(
                name: "extensionofgraceperiodid",
                table: "applicationone",
                newName: "extensionofgraceperiod");
        }
    }
}
