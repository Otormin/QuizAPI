using QuizWebApiProject.Model;

namespace QuizWebApiProject.Interfaces
{
    public interface IAnsweredQuestionRepository
    {
        AnsweredQuestion GetSelectedQuestionById(int id);
        bool SelectedQuestionExistsById(int id);


        bool CreateSelectedQuestion(string matricNumber, int questionId, string courseCode);
        bool SelectedQuestionExists(string matricNumber, int questionId, string courseCode);
        int TotalAmountOfQuestionsAnswered(string matricNumber, string courseCode);


        bool DeleteSelectedQuestions(AnsweredQuestion course);
        bool Save();
    }
}
