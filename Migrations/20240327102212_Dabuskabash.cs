using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class Dabuskabash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "6b281ae0-8b48-4226-8146-2db49d5bbc3d", null, "Admin", "ADMIN" },
                    { "7941c6a6-4850-4884-bb84-bbf1246f0472", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b281ae0-8b48-4226-8146-2db49d5bbc3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7941c6a6-4850-4884-bb84-bbf1246f0472");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1196d470-7528-4ea4-9e2b-e851e1b1fb54", null, "Student", "STUDENT" },
                    { "31fadfc1-bf7e-4808-8d60-0f44e63f4bef", null, "Teacher", "TEACHER" }
                });
        }
    }
}
