using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AppFourAndMoreNomenclatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "earlypaymentdate",
                table: "applicationone",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "hypotesistype",
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
                    table.PrimaryKey("PK_hypotesistype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applicationfour",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    hypotesistypeid = table.Column<int>(type: "integer", nullable: false),
                    bankname = table.Column<string>(type: "text", nullable: true),
                    bulstat = table.Column<string>(type: "text", nullable: true),
                    bankaccount = table.Column<string>(type: "text", nullable: true),
                    studentfullname = table.Column<string>(type: "text", nullable: true),
                    uin = table.Column<string>(type: "text", nullable: true),
                    institution = table.Column<string>(type: "text", nullable: true),
                    speciality = table.Column<string>(type: "text", nullable: true),
                    course = table.Column<int>(type: "integer", nullable: true),
                    facultynumber = table.Column<string>(type: "text", nullable: true),
                    creditnumber = table.Column<string>(type: "text", nullable: true),
                    credittype = table.Column<int>(type: "integer", nullable: false),
                    contractdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    schoolremaining = table.Column<int>(type: "integer", nullable: true),
                    paymentperiod = table.Column<int>(type: "integer", nullable: true),
                    interest = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    outstandingdebtamount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationfour", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationfour_hypotesistype_hypotesistypeid",
                        column: x => x.hypotesistypeid,
                        principalTable: "hypotesistype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationfour_hypotesistypeid",
                table: "applicationfour",
                column: "hypotesistypeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationfour");

            migrationBuilder.DropTable(
                name: "hypotesistype");

            migrationBuilder.DropColumn(
                name: "earlypaymentdate",
                table: "applicationone");
        }
    }
}
