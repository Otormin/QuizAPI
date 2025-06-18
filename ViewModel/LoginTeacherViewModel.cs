using System.ComponentModel.DataAnnotations;

namespace QuizWebApiProject.ViewModel
{
    public class LoginTeacherViewModel
    {
        [Required]
        [StringLength(50)]
        public string StaffId { get; set; }

        [Required]
        [StringLength(50), MinLength(8)]
        public string Password { get; set; }
    }
}
