using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class MergingApplicationsProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationfourhistory");

            migrationBuilder.DropTable(
                name: "applicationonehistory");

            migrationBuilder.AddColumn<int>(
                name: "applicationhistoryid",
                table: "applicationone",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "changestatedescription",
                table: "applicationone",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "commitstate",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "applicationhistoryid",
                table: "applicationfour",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "applicationhistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    userfullname = table.Column<string>(type: "text", nullable: true),
                    modificationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    applicationhistorytype = table.Column<int>(type: "integer", nullable: false),
                    applicationid = table.Column<int>(type: "integer", nullable: false),
                    applicationtype = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationhistory", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_applicationhistoryid",
                table: "applicationone",
                column: "applicationhistoryid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationfour_applicationhistoryid",
                table: "applicationfour",
                column: "applicationhistoryid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationfour_applicationhistory_applicationhistoryid",
                table: "applicationfour",
                column: "applicationhistoryid",
                principalTable: "applicationhistory",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_applicationhistory_applicationhistoryid",
                table: "applicationone",
                column: "applicationhistoryid",
                principalTable: "applicationhistory",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationfour_applicationhistory_applicationhistoryid",
                table: "applicationfour");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_applicationhistory_applicationhistoryid",
                table: "applicationone");

            migrationBuilder.DropTable(
                name: "applicationhistory");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_applicationhistoryid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationfour_applicationhistoryid",
                table: "applicationfour");

            migrationBuilder.DropColumn(
                name: "applicationhistoryid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "changestatedescription",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "commitstate",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "applicationhistoryid",
                table: "applicationfour");

            migrationBuilder.CreateTable(
                name: "applicationfourhistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationfourhistorytype = table.Column<int>(type: "integer", nullable: false),
                    applicationfourid = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    modificationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    userfullname = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "applicationonehistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationoneid = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    modificationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    userfullname = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_applicationfourhistory_applicationfourid",
                table: "applicationfourhistory",
                column: "applicationfourid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationonehistory_applicationoneid",
                table: "applicationonehistory",
                column: "applicationoneid",
                unique: true);
        }
    }
}
