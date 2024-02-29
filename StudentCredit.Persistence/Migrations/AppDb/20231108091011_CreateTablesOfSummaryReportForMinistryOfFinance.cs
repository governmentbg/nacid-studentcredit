using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CreateTablesOfSummaryReportForMinistryOfFinance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "year",
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
                    table.PrimaryKey("PK_year", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sheet",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    yearid = table.Column<int>(type: "integer", nullable: false),
                    bankid = table.Column<int>(type: "integer", nullable: false),
                    limit = table.Column<decimal>(type: "numeric", nullable: true),
                    fulfillmentofthelimit = table.Column<decimal>(type: "numeric", nullable: true),
                    totalsum = table.Column<decimal>(type: "numeric", nullable: true),
                    renegotiatedsum = table.Column<decimal>(type: "numeric", nullable: true),
                    principalabsorbed = table.Column<decimal>(type: "numeric", nullable: true),
                    remaindertodigest = table.Column<decimal>(type: "numeric", nullable: true),
                    interestoverprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    principalpaid = table.Column<decimal>(type: "numeric", nullable: true),
                    interestpaid = table.Column<decimal>(type: "numeric", nullable: true),
                    debtwrittenoff = table.Column<decimal>(type: "numeric", nullable: true),
                    simpledebtprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    simpledebtinterest = table.Column<decimal>(type: "numeric", nullable: true),
                    warrantiesactivatedprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    warrantiesactivatedinterest = table.Column<decimal>(type: "numeric", nullable: true),
                    creditcount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sheet", x => x.id);
                    table.ForeignKey(
                        name: "FK_sheet_bank_bankid",
                        column: x => x.bankid,
                        principalTable: "bank",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sheet_year_yearid",
                        column: x => x.yearid,
                        principalTable: "year",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sheetyear",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    yearid = table.Column<int>(type: "integer", nullable: false),
                    sheetid = table.Column<int>(type: "integer", nullable: false),
                    totalsum = table.Column<decimal>(type: "numeric", nullable: true),
                    renegotiatedsum = table.Column<decimal>(type: "numeric", nullable: true),
                    principalabsorbed = table.Column<decimal>(type: "numeric", nullable: true),
                    remaindertodigest = table.Column<decimal>(type: "numeric", nullable: true),
                    interestoverprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    principalpaid = table.Column<decimal>(type: "numeric", nullable: true),
                    interestpaid = table.Column<decimal>(type: "numeric", nullable: true),
                    debtwrittenoff = table.Column<decimal>(type: "numeric", nullable: true),
                    simpledebtprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    simpledebtinterest = table.Column<decimal>(type: "numeric", nullable: true),
                    warrantiesactivatedprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    warrantiesactivatedinterest = table.Column<decimal>(type: "numeric", nullable: true),
                    creditcount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sheetyear", x => x.id);
                    table.ForeignKey(
                        name: "FK_sheetyear_sheet_sheetid",
                        column: x => x.sheetid,
                        principalTable: "sheet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sheetyear_year_yearid",
                        column: x => x.yearid,
                        principalTable: "year",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "monthdata",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sheetyearid = table.Column<int>(type: "integer", nullable: false),
                    month = table.Column<int>(type: "integer", nullable: false),
                    totalsum = table.Column<decimal>(type: "numeric", nullable: true),
                    renegotiatedsum = table.Column<decimal>(type: "numeric", nullable: true),
                    principalabsorbed = table.Column<decimal>(type: "numeric", nullable: true),
                    remaindertodigest = table.Column<decimal>(type: "numeric", nullable: true),
                    interestoverprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    principalpaid = table.Column<decimal>(type: "numeric", nullable: true),
                    interestpaid = table.Column<decimal>(type: "numeric", nullable: true),
                    debtwrittenoff = table.Column<decimal>(type: "numeric", nullable: true),
                    simpledebtprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    simpledebtinterest = table.Column<decimal>(type: "numeric", nullable: true),
                    warrantiesactivatedprincipal = table.Column<decimal>(type: "numeric", nullable: true),
                    warrantiesactivatedinterest = table.Column<decimal>(type: "numeric", nullable: true),
                    creditcount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monthdata", x => x.id);
                    table.ForeignKey(
                        name: "FK_monthdata_sheetyear_sheetyearid",
                        column: x => x.sheetyearid,
                        principalTable: "sheetyear",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_monthdata_sheetyearid",
                table: "monthdata",
                column: "sheetyearid");

            migrationBuilder.CreateIndex(
                name: "IX_sheet_bankid",
                table: "sheet",
                column: "bankid");

            migrationBuilder.CreateIndex(
                name: "IX_sheet_yearid",
                table: "sheet",
                column: "yearid");

            migrationBuilder.CreateIndex(
                name: "IX_sheetyear_sheetid",
                table: "sheetyear",
                column: "sheetid");

            migrationBuilder.CreateIndex(
                name: "IX_sheetyear_yearid",
                table: "sheetyear",
                column: "yearid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "monthdata");

            migrationBuilder.DropTable(
                name: "sheetyear");

            migrationBuilder.DropTable(
                name: "sheet");

            migrationBuilder.DropTable(
                name: "year");
        }
    }
}
