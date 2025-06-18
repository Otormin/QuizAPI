using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "3f94eed3-1a6b-4c5a-a5c3-05f0d27bf61c", null, "Teacher", "TEACHER" },
                    { "a54b6522-daa4-45a7-9b6c-5a17691d70fc", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f94eed3-1a6b-4c5a-a5c3-05f0d27bf61c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a54b6522-daa4-45a7-9b6c-5a17691d70fc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09f77a59-31c7-4bd3-af63-80ac7c2b525a", null, "Teacher", "TEACHER" },
                    { "5524c003-1859-4d83-9394-ab1c9708a886", null, "Student", "STUDENT" }
                });
        }
    }
}
