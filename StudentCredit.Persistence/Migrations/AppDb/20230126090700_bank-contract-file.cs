using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class bankcontractfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dbid",
                table: "bankcontract");

            migrationBuilder.DropColumn(
                name: "hash",
                table: "bankcontract");

            migrationBuilder.DropColumn(
                name: "key",
                table: "bankcontract");

            migrationBuilder.DropColumn(
                name: "mimetype",
                table: "bankcontract");

            migrationBuilder.DropColumn(
                name: "name",
                table: "bankcontract");

            migrationBuilder.DropColumn(
                name: "size",
                table: "bankcontract");

            migrationBuilder.CreateTable(
                name: "bankcontractfile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contractid = table.Column<int>(type: "integer", nullable: false),
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: true),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    mimetype = table.Column<string>(type: "text", nullable: true),
                    dbid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankcontractfile", x => x.id);
                    table.ForeignKey(
                        name: "FK_bankcontractfile_bankcontract_contractid",
                        column: x => x.contractid,
                        principalTable: "bankcontract",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bankcontractfile_contractid",
                table: "bankcontractfile",
                column: "contractid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankcontractfile");

            migrationBuilder.AddColumn<int>(
                name: "dbid",
                table: "bankcontract",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "hash",
                table: "bankcontract",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "key",
                table: "bankcontract",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "mimetype",
                table: "bankcontract",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "bankcontract",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "size",
                table: "bankcontract",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
