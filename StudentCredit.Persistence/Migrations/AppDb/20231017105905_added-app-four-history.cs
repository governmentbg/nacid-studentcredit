using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class addedappfourhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isarchived",
                table: "applicationfour",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "rootid",
                table: "applicationfour",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "applicationfourhistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    userfullname = table.Column<string>(type: "text", nullable: true),
                    modificationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    applicationfourid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationfourhistory", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationfourhistory_applicationfour_applicationfourid",
                        column: x => x.applicationfourid,
                        principalTable: "applicationfour",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationfourhistory_applicationfourid",
                table: "applicationfourhistory",
                column: "applicationfourid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationfourhistory");

            migrationBuilder.DropColumn(
                name: "isarchived",
                table: "applicationfour");

            migrationBuilder.DropColumn(
                name: "rootid",
                table: "applicationfour");
        }
    }
}
