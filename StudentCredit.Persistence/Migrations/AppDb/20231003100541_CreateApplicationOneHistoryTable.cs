using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CreateApplicationOneHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rootid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "applicationonehistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    modificationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    applicationoneid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationonehistory", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationonehistory_applicationone_applicationoneid",
                        column: x => x.applicationoneid,
                        principalTable: "applicationone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationonehistory_applicationoneid",
                table: "applicationonehistory",
                column: "applicationoneid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationonehistory");

            migrationBuilder.DropColumn(
                name: "rootid",
                table: "applicationone");
        }
    }
}
