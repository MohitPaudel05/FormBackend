using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBackend.Migrations
{
    /// <inheritdoc />
    public partial class AfterParentEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermanentAddresses");

            migrationBuilder.DropTable(
                name: "TemporaryAddresses");

            migrationBuilder.DropColumn(
                name: "AlternateEmail",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "CitizenshipIssueDate",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "CitizenshipIssueDistrict",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "CitizenshipNumber",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "DisabilityPercentage",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "DisabilityType",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "EmergencyContactName",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNumber",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "EmergencyContactRelation",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "Ethnicity",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "HasDisability",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "PrimaryMobile",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "SecondaryMobile",
                table: "PersonalDetails");

            migrationBuilder.AlterColumn<int>(
                name: "Relation",
                table: "ParentDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_PersonalDetails_StudentId",
                        column: x => x.StudentId,
                        principalTable: "PersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CitizenshipInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CitizenshipNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssueDistrict = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenshipInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitizenshipInfos_PersonalDetails_StudentId",
                        column: x => x.StudentId,
                        principalTable: "PersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryMobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SecondaryMobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmergencyContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmergencyContactRelation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmergencyContactNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInfos_PersonalDetails_StudentId",
                        column: x => x.StudentId,
                        principalTable: "PersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    HasDisability = table.Column<bool>(type: "bit", nullable: false),
                    DisabilityType = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    DisabilityPercentage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disabilities_PersonalDetails_StudentId",
                        column: x => x.StudentId,
                        principalTable: "PersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ReligionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ethnicity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Religions_PersonalDetails_StudentId",
                        column: x => x.StudentId,
                        principalTable: "PersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StudentId",
                table: "Addresses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenshipInfos_StudentId",
                table: "CitizenshipInfos",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_StudentId",
                table: "ContactInfos",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Disabilities_StudentId",
                table: "Disabilities",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Religions_StudentId",
                table: "Religions",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CitizenshipInfos");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "Disabilities");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.AddColumn<string>(
                name: "AlternateEmail",
                table: "PersonalDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CitizenshipIssueDate",
                table: "PersonalDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenshipIssueDistrict",
                table: "PersonalDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CitizenshipNumber",
                table: "PersonalDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DisabilityPercentage",
                table: "PersonalDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisabilityType",
                table: "PersonalDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PersonalDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactName",
                table: "PersonalDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNumber",
                table: "PersonalDetails",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactRelation",
                table: "PersonalDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ethnicity",
                table: "PersonalDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasDisability",
                table: "PersonalDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryMobile",
                table: "PersonalDetails",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "PersonalDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondaryMobile",
                table: "PersonalDetails",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Relation",
                table: "ParentDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "PermanentAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tole = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WardNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermanentAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermanentAddresses_PersonalDetails_StudentId",
                        column: x => x.StudentId,
                        principalTable: "PersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tole = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WardNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryAddresses_PersonalDetails_StudentId",
                        column: x => x.StudentId,
                        principalTable: "PersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermanentAddresses_StudentId",
                table: "PermanentAddresses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryAddresses_StudentId",
                table: "TemporaryAddresses",
                column: "StudentId");
        }
    }
}
