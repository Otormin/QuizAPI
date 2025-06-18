using QuizWebApiProject.Data;
using QuizWebApiProject.Dto;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;

namespace QuizWebApiProject.Repository
{
    public class QuestionAndAnswerRepository : IQuestionAndAnswerRepository
    {
        private readonly DataContext _context;

        public QuestionAndAnswerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateQuestionsAndAnswers(QuestionAndAnswer questionAndAnswer)
        {
            _context.Add(questionAndAnswer);
            return Save();
        }

        public bool DeleteQuestionAndAnswers(QuestionAndAnswer questionAndAnswer)
        {
            _context.Remove(questionAndAnswer);
            return Save();
        }

        public QuestionAndAnswer GetQuestionAndAnswer(int id)
        {
            return _context.QuestionsAndAnswers.Where(q => q.Id == id).FirstOrDefault();
        }

        public ICollection<QuestionAndAnswer> GetQuestionAndAnswerByCourseCode(string course)
        {
            return _context.QuestionsAndAnswers.Where(t => t.CourseCode == course).ToList();
        }

        public ICollection<QuestionAndAnswer> GetQuestionsAndAnswers()
        {
            return _context.QuestionsAndAnswers.OrderBy(q => q.Id).ToList();
        }

        public bool QuestionAndAnswerCourseExists(string course)
        {
            return _context.QuestionsAndAnswers.Any(q => q.CourseCode == course);
        }

        public bool QuestionAndAnswerExists(int id)
        {
            return _context.QuestionsAndAnswers.Any(q => q.Id == id);
        }

        public string GetAnswersByQuestionId(int userId)
        {
            //pulling the information from the a field from a database
             string answer = _context.QuestionsAndAnswers.Where(q => q.Id == userId).Select(q => q.Answer).FirstOrDefault().ToUpper();
            return answer;
        }

        public bool UpdateQuestionsAndAnswers(QuestionAndAnswer questionAndAnswer)
        {
            _context.Update(questionAndAnswer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            if (saved > 0)
            {
                return true;
            }
            return false;
        }
    }
}
