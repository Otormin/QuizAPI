using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabaseAgainAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "320ac215-7dc0-4c69-89de-d591723621fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eaec9afb-bf2f-4790-bc16-528b34421447");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d2eeea32-0ce3-45fd-bbae-105092907dec", null, "Student", "STUDENT" },
                    { "ff36763d-f311-4e0f-9c29-d40e2bfb8fee", null, "Teacher", "TEACHER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2eeea32-0ce3-45fd-bbae-105092907dec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff36763d-f311-4e0f-9c29-d40e2bfb8fee");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "320ac215-7dc0-4c69-89de-d591723621fe", null, "Student", "STUDENT" },
                    { "eaec9afb-bf2f-4790-bc16-528b34421447", null, "Teacher", "TEACHER" }
                });
        }
    }
}
