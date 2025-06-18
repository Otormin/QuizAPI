using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class abidoshaker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "09f77a59-31c7-4bd3-af63-80ac7c2b525a", null, "Teacher", "TEACHER" },
                    { "5524c003-1859-4d83-9394-ab1c9708a886", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09f77a59-31c7-4bd3-af63-80ac7c2b525a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5524c003-1859-4d83-9394-ab1c9708a886");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6b281ae0-8b48-4226-8146-2db49d5bbc3d", null, "Admin", "ADMIN" },
                    { "7941c6a6-4850-4884-bb84-bbf1246f0472", null, "Member", "MEMBER" }
                });
        }
    }
}
