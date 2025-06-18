using System.ComponentModel.DataAnnotations;

namespace QuizWebApiProject.ViewModel
{
    public class LoginStudentViewModel
    {
        [Required]
        [StringLength(50)]
        public string MatricNumber { get; set; }

        [Required]
        [StringLength(50), MinLength(8)]
        public string Password { get; set; }
    }
}

/*
using System.ComponentModel.DataAnnotations;

namespace QuizWebApiProject.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50), MinLength(8)]
        public string Password { get; set; }
    }
}

 */