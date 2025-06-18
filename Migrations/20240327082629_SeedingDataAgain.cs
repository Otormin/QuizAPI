using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "038935a5-8bbf-4fce-b208-d1d65f447ff4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31b48056-3e11-4882-bd68-2e04595ab395");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "320ac215-7dc0-4c69-89de-d591723621fe", null, "Student", "STUDENT" },
                    { "eaec9afb-bf2f-4790-bc16-528b34421447", null, "Teacher", "TEACHER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "038935a5-8bbf-4fce-b208-d1d65f447ff4", null, "Teacher", "TEACHER" },
                    { "31b48056-3e11-4882-bd68-2e04595ab395", null, "Student", "STUDENT" }
                });
        }
    }
}
