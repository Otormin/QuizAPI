using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class helpMe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "7f280712-2b3b-405d-aa0c-916b6e1f59a4", null, "Teacher", "TEACHER" },
                    { "e445a875-938c-4e55-b0d1-2f049e7c586d", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f280712-2b3b-405d-aa0c-916b6e1f59a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e445a875-938c-4e55-b0d1-2f049e7c586d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d2eeea32-0ce3-45fd-bbae-105092907dec", null, "Student", "STUDENT" },
                    { "ff36763d-f311-4e0f-9c29-d40e2bfb8fee", null, "Teacher", "TEACHER" }
                });
        }
    }
}
