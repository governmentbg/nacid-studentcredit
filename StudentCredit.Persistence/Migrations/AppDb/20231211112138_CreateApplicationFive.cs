using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CreateApplicationFive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationfive",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationfivetype = table.Column<int>(type: "integer", nullable: false),
                    blankdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    amountrequested = table.Column<decimal>(type: "numeric", nullable: true),
                    creditcount = table.Column<int>(type: "integer", nullable: true),
                    from = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    to = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    commitstate = table.Column<int>(type: "integer", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    changestatedescription = table.Column<string>(type: "text", nullable: true),
                    rootid = table.Column<int>(type: "integer", nullable: false),
                    applicationhistoryid = table.Column<int>(type: "integer", nullable: true),
                    bankid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationfive", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationfive_applicationhistory_applicationhistoryid",
                        column: x => x.applicationhistoryid,
                        principalTable: "applicationhistory",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicationfive_bank_bankid",
                        column: x => x.bankid,
                        principalTable: "bank",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicationfiveattachedfile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationfiveid = table.Column<int>(type: "integer", nullable: false),
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: true),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    mimetype = table.Column<string>(type: "text", nullable: true),
                    dbid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationfiveattachedfile", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationfiveattachedfile_applicationfive_applicationfive~",
                        column: x => x.applicationfiveid,
                        principalTable: "applicationfive",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationfive_applicationhistoryid",
                table: "applicationfive",
                column: "applicationhistoryid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationfive_bankid",
                table: "applicationfive",
                column: "bankid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationfiveattachedfile_applicationfiveid",
                table: "applicationfiveattachedfile",
                column: "applicationfiveid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationfiveattachedfile");

            migrationBuilder.DropTable(
                name: "applicationfive");
        }
    }
}
