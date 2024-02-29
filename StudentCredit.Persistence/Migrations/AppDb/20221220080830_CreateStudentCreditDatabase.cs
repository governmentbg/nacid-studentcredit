using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CreateStudentCreditDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adjourntype",
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
                    table.PrimaryKey("PK_adjourntype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applicationonetype",
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
                    table.PrimaryKey("PK_applicationonetype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applicationone",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    applicationonetypeid = table.Column<int>(type: "integer", nullable: false),
                    educationtype = table.Column<int>(type: "integer", nullable: false),
                    blankdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    bankname = table.Column<string>(type: "text", nullable: true),
                    bulstat = table.Column<string>(type: "text", nullable: true),
                    contactperson = table.Column<string>(type: "text", nullable: true),
                    studentfullname = table.Column<string>(type: "text", nullable: true),
                    nationality = table.Column<string>(type: "text", nullable: true),
                    uin = table.Column<string>(type: "text", nullable: true),
                    idnumber = table.Column<string>(type: "text", nullable: true),
                    institution = table.Column<string>(type: "text", nullable: true),
                    researcharea = table.Column<string>(type: "text", nullable: true),
                    speciality = table.Column<string>(type: "text", nullable: true),
                    course = table.Column<int>(type: "integer", nullable: false),
                    facultynumber = table.Column<string>(type: "text", nullable: true),
                    creditnumber = table.Column<string>(type: "text", nullable: true),
                    creditsize = table.Column<decimal>(type: "numeric", nullable: false),
                    semestertax = table.Column<decimal>(type: "numeric", nullable: false),
                    interest = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    credittype = table.Column<int>(type: "integer", nullable: false),
                    contractdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cancelcondition = table.Column<string>(type: "text", nullable: true),
                    schoolremaining = table.Column<int>(type: "integer", nullable: true),
                    expirationdateofgraceperiod = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    monthlypayment = table.Column<decimal>(type: "numeric", nullable: false),
                    paymentdescription = table.Column<string>(type: "text", nullable: true),
                    paymentperiod = table.Column<int>(type: "integer", nullable: false),
                    paymentdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    recontractioninfo = table.Column<string>(type: "text", nullable: true),
                    recontractiondate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    adjourntypeid = table.Column<int>(type: "integer", nullable: true),
                    adjourndate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    adjournperiod = table.Column<int>(type: "integer", nullable: false),
                    forcepaymentinfo = table.Column<string>(type: "text", nullable: true),
                    forcepaymentdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationone", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationone_adjourntype_adjourntypeid",
                        column: x => x.adjourntypeid,
                        principalTable: "adjourntype",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicationone_applicationonetype_applicationonetypeid",
                        column: x => x.applicationonetypeid,
                        principalTable: "applicationonetype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_adjourntypeid",
                table: "applicationone",
                column: "adjourntypeid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_applicationonetypeid",
                table: "applicationone",
                column: "applicationonetypeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationone");

            migrationBuilder.DropTable(
                name: "adjourntype");

            migrationBuilder.DropTable(
                name: "applicationonetype");
        }
    }
}
