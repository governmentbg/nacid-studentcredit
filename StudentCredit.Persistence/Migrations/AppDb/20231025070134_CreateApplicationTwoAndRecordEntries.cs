using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CreateApplicationTwoAndRecordEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationtwo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bankid = table.Column<int>(type: "integer", nullable: false),
                    creationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    recorddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    totalsum = table.Column<decimal>(type: "numeric", nullable: false),
                    creditcount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwo", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwo_bank_bankid",
                        column: x => x.bankid,
                        principalTable: "bank",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recordentry",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: false),
                    creditnumber = table.Column<string>(type: "text", nullable: true),
                    studentfullname = table.Column<string>(type: "text", nullable: true),
                    uin = table.Column<string>(type: "text", nullable: true),
                    totalsum = table.Column<decimal>(type: "numeric", nullable: true),
                    principalabsorbed = table.Column<decimal>(type: "numeric", nullable: true),
                    interest = table.Column<decimal>(type: "numeric", nullable: true),
                    capitalizedprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    isrepaid = table.Column<bool>(type: "boolean", nullable: true),
                    monthlypayment = table.Column<decimal>(type: "numeric", nullable: true),
                    repaidmonthlyprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    repaidmonthlyinterest = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recordentry", x => x.id);
                    table.ForeignKey(
                        name: "FK_recordentry_applicationtwo_applicationtwoid",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwo_bankid",
                table: "applicationtwo",
                column: "bankid");

            migrationBuilder.CreateIndex(
                name: "IX_recordentry_applicationtwoid",
                table: "recordentry",
                column: "applicationtwoid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recordentry");

            migrationBuilder.DropTable(
                name: "applicationtwo");
        }
    }
}
