using QuizWebApiProject.Data;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Migrations;
using QuizWebApiProject.Model;

namespace QuizWebApiProject.Repository
{
    public class AnsweredQuestionRepository : IAnsweredQuestionRepository
    {
        private readonly DataContext _context;

        public AnsweredQuestionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSelectedQuestion(string matricNumber, int questionId, string courseCode)
        {
            var selectedQuestion = new AnsweredQuestion
            {
                MatricNumber = matricNumber,
                QuestionId = questionId,
                CourseCode = courseCode
            };

            _context.AnsweredQuestions.Add(selectedQuestion);
            return Save();
        }

        public bool SelectedQuestionExists(string matricNumber, int questionId, string courseCode)
        {
            var selectedQuestionExists = _context.AnsweredQuestions.Any(s => s.MatricNumber == matricNumber && s.QuestionId == questionId && s.CourseCode == courseCode);
            return selectedQuestionExists;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            if(saved > 0)
            {
                return true;
            }
            return false;
        }

        public AnsweredQuestion GetSelectedQuestionById(int id)
        {
            return _context.AnsweredQuestions.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool SelectedQuestionExistsById(int id)
        {
            return _context.AnsweredQuestions.Any(s => s.Id == id);
        }

        public bool DeleteSelectedQuestions(AnsweredQuestion course)
        {
            _context.Remove(course);
            return Save();
        }

        public int TotalAmountOfQuestionsAnswered(string matricNumber, string courseCode)
        {
            return _context.AnsweredQuestions
                   .Count(aq => aq.MatricNumber == matricNumber
                             && aq.CourseCode == courseCode);
        }
    }
}
