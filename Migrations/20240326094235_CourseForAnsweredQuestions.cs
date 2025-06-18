using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class CourseForAnsweredQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "AnsweredQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "AnsweredQuestions");
        }
    }
}
