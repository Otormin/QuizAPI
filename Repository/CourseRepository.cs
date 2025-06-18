using QuizWebApiProject.Data;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;

namespace QuizWebApiProject.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public bool CourseCodeExists(string courseCode)
        {
            return _context.Courses.Any(c => c.CourseCode == courseCode);
        }

        public bool CourseIdExists(int id)
        {
            return _context.Courses.Any(c => c.Id == id);
        }

        public bool CreateCourse(Course course)
        {
            _context.Add(course);
            return Save();
        }

        public bool DeleteCourse(Course course)
        {
            _context.Remove(course);
            return Save();
        }

        public Course GetCourse(int id)
        {
           return _context.Courses.Where(c => c.Id == id).FirstOrDefault();
        }

        public Course GetCourseByCourseCode(string courseCode)
        {
           return _context.Courses.Where(c => c.CourseCode == courseCode).FirstOrDefault();
        }

        public ICollection<Course> GetCourses()
        {
            return _context.Courses.OrderBy(r => r.Id).ToList();
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

        public bool UpdateCourse(Course course)
        {
            _context.Update(course);
            return Save();
        }
    }
}
