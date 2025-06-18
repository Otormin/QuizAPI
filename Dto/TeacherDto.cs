using System.ComponentModel.DataAnnotations;

namespace QuizWebApiProject.Dto
{
    public class TeacherDto
    {
        public string Email { get; set; }
        public string CourseCode { get; set; }
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
}
