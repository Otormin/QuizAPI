using QuizWebApiProject.Model;

namespace QuizWebApiProject.Interfaces
{
    public interface ICourseRepository
    {
        ICollection<Course> GetCourses();
        Course GetCourse(int id);
        Course GetCourseByCourseCode(string courseCode);
        bool CourseCodeExists(string courseCode);
        bool CourseIdExists(int id);


        bool CreateCourse(Course course);


        bool DeleteCourse(Course course);


        bool UpdateCourse(Course course);

        bool Save();
    }
}
