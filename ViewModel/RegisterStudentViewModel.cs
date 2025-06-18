using System.ComponentModel.DataAnnotations;

namespace QuizWebApiProject.ViewModel
{
    public class RegisterStudentViewModel
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Department { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50), MinLength(4)]
        public string Gender { get; set; }

        [Required]
        [StringLength(50), MinLength(8)]
        public string Password { get; set; }

        [Required]
        [StringLength(50), MinLength(8)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
