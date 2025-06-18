using QuizWebApiProject.Model;

namespace QuizWebApiProject.Interfaces
{
    public interface IStudentScoreRepository
    {
        ICollection<StudentScore> GetStudentScores();
        StudentScore GetScoreByMatricNumber(string matricNumber);
        StudentScore GetStudentScoreById(int id);
        bool StudentScoreExistsById(int id);



        bool MatricNumberExists(string matricNumber);
        bool StudentScoreCourseExists(string courseCode);


        bool CreateStudentScore(StudentScore studentScore);
        bool CreateScore(string matricNumber, int score);


        bool UpdateScore(string matricNumber, int score);

        bool DeleteScore(StudentScore studentScore);


        bool Save();

    }
}
