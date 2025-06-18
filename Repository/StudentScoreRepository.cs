using QuizWebApiProject.Data;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;

namespace QuizWebApiProject.Repository
{
    public class StudentScoreRepository : IStudentScoreRepository
    {
        private readonly DataContext _context;

        public StudentScoreRepository(DataContext context)
        {
            _context = context;
        }

        //make a variable that takes complex type
        //public bool CreateScore(StudentScore score)
        //{
        //    /*var studentScore = new StudentScore 
        //    { 
        //        Score = score 
        //    };*/
        //      _context.StudentScores.Add(studentScore);
        //    return Save();
        //}

        public bool CreateScore(string Matricnumber, int score)
        {
            var studentScore = _context.StudentScores.FirstOrDefault(q => q.MatricNumber == Matricnumber);

            if (studentScore == null)
            {
                return false;
            }

            studentScore.Score = score;

            _context.Update(studentScore);
            return Save();
        }

        public StudentScore GetScoreByMatricNumber(string matricNumber)
        {
            return _context.StudentScores.Where(g => g.MatricNumber == matricNumber).FirstOrDefault();
        }

        public ICollection<StudentScore> GetStudentScores()
        {
            return _context.StudentScores.OrderBy(s => s.Id).ToList();
        }

        public StudentScore GetStudentScoresById(int id)
        {
            return _context.StudentScores.Where(g => g.Id == id).FirstOrDefault();
        }

        public bool MatricNumberExists(string matricNumber)
        {
            return _context.StudentScores.Any(g => g.MatricNumber == matricNumber);
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

        public bool StudentScoreCourseExists(string courseCode)
        {
            return _context.Courses.Any(t => t.CourseCode == courseCode);
        }

        public bool UpdateScore(string Matricnumber, int score)
        {
            var studentScore = _context.StudentScores.FirstOrDefault(q => q.MatricNumber == Matricnumber);

            if (studentScore == null)
            {
                return false;
            }

            studentScore.Score = score;

            _context.Update(studentScore);            
            return Save();
        }

        public bool DeleteScore(StudentScore studentScore)
        {
            _context.Remove(studentScore);
            return Save();
        }

        public bool CreateStudentScore(StudentScore studentScore)
        {
            _context.Add(studentScore);
            return Save();
        }

        public StudentScore GetStudentScoreById(int id)
        {
            return _context.StudentScores.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool StudentScoreExistsById(int id)
        {
            return _context.StudentScores.Any(s => s.Id == id);
        }
    }
}
