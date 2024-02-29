using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class addedattachedfilesapplicationfour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationfourattachedfile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationfourattachedfiletype = table.Column<int>(type: "integer", nullable: false),
                    applicaitonfourid = table.Column<int>(type: "integer", nullable: false),
                    applicationfourid = table.Column<int>(type: "integer", nullable: true),
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: true),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    mimetype = table.Column<string>(type: "text", nullable: true),
                    dbid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationfourattachedfile", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationfourattachedfile_applicationfour_applicationfour~",
                        column: x => x.applicationfourid,
                        principalTable: "applicationfour",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationfourattachedfile_applicationfourid",
                table: "applicationfourattachedfile",
                column: "applicationfourid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationfourattachedfile");
        }
    }
}
