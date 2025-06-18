using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class iDontKnowWhatToNameThis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "1196d470-7528-4ea4-9e2b-e851e1b1fb54", null, "Student", "STUDENT" },
                    { "31fadfc1-bf7e-4808-8d60-0f44e63f4bef", null, "Teacher", "TEACHER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1196d470-7528-4ea4-9e2b-e851e1b1fb54");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31fadfc1-bf7e-4808-8d60-0f44e63f4bef");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f280712-2b3b-405d-aa0c-916b6e1f59a4", null, "Teacher", "TEACHER" },
                    { "e445a875-938c-4e55-b0d1-2f049e7c586d", null, "Student", "STUDENT" }
                });
        }
    }
}
