using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBackend.Migrations
{
    /// <inheritdoc />
    public partial class afterFixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgramEnrollments_StudentId",
                table: "ProgramEnrollments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollmentDate",
                table: "ProgramEnrollments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyIncome",
                table: "ParentDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEnrollments_StudentId",
                table: "ProgramEnrollments",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgramEnrollments_StudentId",
                table: "ProgramEnrollments");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EnrollmentDate",
                table: "ProgramEnrollments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyIncome",
                table: "ParentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEnrollments_StudentId",
                table: "ProgramEnrollments",
                column: "StudentId");
        }
    }
}
