using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class addedbankcontracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "changed",
                table: "bank");

            migrationBuilder.DropColumn(
                name: "concluded",
                table: "bank");

            migrationBuilder.DropColumn(
                name: "email",
                table: "bank");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "bank");

            migrationBuilder.DropColumn(
                name: "terminated",
                table: "bank");

            migrationBuilder.CreateTable(
                name: "bankcontract",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    bankid = table.Column<int>(type: "integer", nullable: false),
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: true),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    mimetype = table.Column<string>(type: "text", nullable: true),
                    dbid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankcontract", x => x.id);
                    table.ForeignKey(
                        name: "FK_bankcontract_bank_bankid",
                        column: x => x.bankid,
                        principalTable: "bank",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bankcontract_bankid",
                table: "bankcontract",
                column: "bankid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankcontract");

            migrationBuilder.AddColumn<string>(
                name: "changed",
                table: "bank",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "concluded",
                table: "bank",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "bank",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "bank",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "terminated",
                table: "bank",
                type: "text",
                nullable: true);
        }
    }
}
