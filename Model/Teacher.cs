using System.Diagnostics.Metrics;

namespace QuizWebApiProject.Model
{
    public class Teacher
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string CourseCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
