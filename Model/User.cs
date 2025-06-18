using Microsoft.AspNetCore.Identity;

namespace QuizWebApiProject.Model
{
    public class User : IdentityUser
    {
        //teacher
        public string? StaffId { get; set; }
        public string? CourseCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }

        //student
        public string? MatricNumber { get; set; }
        public string? Department { get; set; }
    }
}
