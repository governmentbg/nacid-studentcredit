using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentCredit.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddNomenclatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "educationalqualification",
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
                    table.PrimaryKey("PK_educationalqualification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "educationformtype",
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
                    table.PrimaryKey("PK_educationformtype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "emailtype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject = table.Column<string>(type: "text", nullable: true),
                    body = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailtype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "institution",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    institutiontypeid = table.Column<int>(type: "integer", nullable: true),
                    externalid = table.Column<int>(type: "integer", nullable: false),
                    rootid = table.Column<int>(type: "integer", nullable: false),
                    parentid = table.Column<int>(type: "integer", nullable: true),
                    level = table.Column<int>(type: "integer", nullable: false),
                    institutionownershiptypeid = table.Column<int>(type: "integer", nullable: true),
                    uic = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_institution", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "researcharea",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    codenumber = table.Column<int>(type: "integer", nullable: false),
                    namebg = table.Column<string>(type: "text", nullable: true),
                    nameeng = table.Column<string>(type: "text", nullable: true),
                    parentid = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_researcharea", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
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
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "speciality",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    externalid = table.Column<int>(type: "integer", nullable: false),
                    educationalqualificationid = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_speciality", x => x.id);
                    table.ForeignKey(
                        name: "FK_speciality_educationalqualification_educationalqualificatio~",
                        column: x => x.educationalqualificationid,
                        principalTable: "educationalqualification",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "email",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typeid = table.Column<int>(type: "integer", nullable: false),
                    subject = table.Column<string>(type: "text", nullable: true),
                    body = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email", x => x.id);
                    table.ForeignKey(
                        name: "FK_email_emailtype_typeid",
                        column: x => x.typeid,
                        principalTable: "emailtype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    passwordsalt = table.Column<string>(type: "text", nullable: true),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    middlename = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    roleid = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_roleid",
                        column: x => x.roleid,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "institutionspeciality",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    externalid = table.Column<int>(type: "integer", nullable: false),
                    institutionid = table.Column<int>(type: "integer", nullable: false),
                    specialityid = table.Column<int>(type: "integer", nullable: false),
                    educationalformid = table.Column<int>(type: "integer", nullable: true),
                    duration = table.Column<double>(type: "double precision", nullable: true),
                    researchareaid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_institutionspeciality", x => x.id);
                    table.ForeignKey(
                        name: "FK_institutionspeciality_educationformtype_educationalformid",
                        column: x => x.educationalformid,
                        principalTable: "educationformtype",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_institutionspeciality_institution_institutionid",
                        column: x => x.institutionid,
                        principalTable: "institution",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_institutionspeciality_researcharea_researchareaid",
                        column: x => x.researchareaid,
                        principalTable: "researcharea",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_institutionspeciality_speciality_specialityid",
                        column: x => x.specialityid,
                        principalTable: "speciality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "emailaddressee",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    emailid = table.Column<int>(type: "integer", nullable: false),
                    addresseetype = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    sentdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailaddressee", x => x.id);
                    table.ForeignKey(
                        name: "FK_emailaddressee_email_emailid",
                        column: x => x.emailid,
                        principalTable: "email",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "passwordtoken",
                columns: table => new
                {
                    value = table.Column<string>(type: "text", nullable: false),
                    expirationtime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isused = table.Column<bool>(type: "boolean", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passwordtoken", x => x.value);
                    table.ForeignKey(
                        name: "FK_passwordtoken_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_email_typeid",
                table: "email",
                column: "typeid");

            migrationBuilder.CreateIndex(
                name: "IX_emailaddressee_emailid",
                table: "emailaddressee",
                column: "emailid");

            migrationBuilder.CreateIndex(
                name: "IX_institutionspeciality_educationalformid",
                table: "institutionspeciality",
                column: "educationalformid");

            migrationBuilder.CreateIndex(
                name: "IX_institutionspeciality_institutionid",
                table: "institutionspeciality",
                column: "institutionid");

            migrationBuilder.CreateIndex(
                name: "IX_institutionspeciality_researchareaid",
                table: "institutionspeciality",
                column: "researchareaid");

            migrationBuilder.CreateIndex(
                name: "IX_institutionspeciality_specialityid",
                table: "institutionspeciality",
                column: "specialityid");

            migrationBuilder.CreateIndex(
                name: "IX_passwordtoken_userid",
                table: "passwordtoken",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_speciality_educationalqualificationid",
                table: "speciality",
                column: "educationalqualificationid");

            migrationBuilder.CreateIndex(
                name: "IX_user_roleid",
                table: "user",
                column: "roleid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emailaddressee");

            migrationBuilder.DropTable(
                name: "institutionspeciality");

            migrationBuilder.DropTable(
                name: "passwordtoken");

            migrationBuilder.DropTable(
                name: "email");

            migrationBuilder.DropTable(
                name: "educationformtype");

            migrationBuilder.DropTable(
                name: "institution");

            migrationBuilder.DropTable(
                name: "researcharea");

            migrationBuilder.DropTable(
                name: "speciality");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "emailtype");

            migrationBuilder.DropTable(
                name: "educationalqualification");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
