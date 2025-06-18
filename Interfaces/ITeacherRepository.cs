using QuizWebApiProject.Model;

namespace QuizWebApiProject.Interfaces
{
    public interface ITeacherRepository
    {
        ICollection<User> GetTeachers();
        ICollection<User> GetStudents();
        User GetTeacherByStaffId(string staffId);
        User GetStudentByMatricNumber(string matricNumber);
        ICollection<User> GetTeachersByCourseCode(string course);
        ICollection<User> GetStudentsByDepartment(string department);



        bool TeacherCourseExists(string course);
        bool TeacherStaffIdExists(string staffId);
        bool StudentMatricNumberExists(string matricNumber);


        bool DeleteTeacher(User teacher);


        bool Save();
    }
}
