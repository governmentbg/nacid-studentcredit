using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddCountryNomenclatureUpdateApplicationOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "institution",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "nationality",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "researcharea",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "speciality",
                table: "applicationone");

            migrationBuilder.AddColumn<int>(
                name: "institutionid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "nationalityid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "researchareaid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "specialityid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameen = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_institutionid",
                table: "applicationone",
                column: "institutionid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_nationalityid",
                table: "applicationone",
                column: "nationalityid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_researchareaid",
                table: "applicationone",
                column: "researchareaid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_specialityid",
                table: "applicationone",
                column: "specialityid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_country_nationalityid",
                table: "applicationone",
                column: "nationalityid",
                principalTable: "country",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_institution_institutionid",
                table: "applicationone",
                column: "institutionid",
                principalTable: "institution",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_researcharea_researchareaid",
                table: "applicationone",
                column: "researchareaid",
                principalTable: "researcharea",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_speciality_specialityid",
                table: "applicationone",
                column: "specialityid",
                principalTable: "speciality",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_country_nationalityid",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_institution_institutionid",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_researcharea_researchareaid",
                table: "applicationone");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_speciality_specialityid",
                table: "applicationone");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_institutionid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_nationalityid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_researchareaid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_specialityid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "institutionid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "nationalityid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "researchareaid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "specialityid",
                table: "applicationone");

            migrationBuilder.AddColumn<string>(
                name: "institution",
                table: "applicationone",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nationality",
                table: "applicationone",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "researcharea",
                table: "applicationone",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "speciality",
                table: "applicationone",
                type: "text",
                nullable: true);
        }
    }
}
