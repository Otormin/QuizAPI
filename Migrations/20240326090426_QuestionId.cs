using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class QuestionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "questionId",
                table: "AnsweredQuestions",
                newName: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "AnsweredQuestions",
                newName: "questionId");
        }
    }
}
