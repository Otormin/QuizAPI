using QuizWebApiProject.Dto;
using QuizWebApiProject.Model;

namespace QuizWebApiProject.Interfaces
{
    public interface IQuestionAndAnswerRepository
    {
        ICollection<QuestionAndAnswer> GetQuestionsAndAnswers();
        ICollection<QuestionAndAnswer> GetQuestionAndAnswerByCourseCode(string course);
        QuestionAndAnswer GetQuestionAndAnswer(int id);
        string GetAnswersByQuestionId(int id);


        bool QuestionAndAnswerExists (int id);
        bool QuestionAndAnswerCourseExists(string course);


        bool CreateQuestionsAndAnswers(QuestionAndAnswer questionAndAnswer);

        bool UpdateQuestionsAndAnswers(QuestionAndAnswer questionAndAnswer);

        bool DeleteQuestionAndAnswers(QuestionAndAnswer questionAndAnswer);


        bool Save();
    }
}
