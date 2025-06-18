using System.ComponentModel.DataAnnotations;

namespace QuizWebApiProject.Dto
{
    public class AnswersDto
    {

        [Required]
        public int QuestionNumber { get; set; }

        [Required]
        public string? CourseCode { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}

/*
{
  "email": "oprosper330@gmail.com",
  "department": "testing",
  "firstName": "Prosper",
  "lastName": "Ogunwole",
  "gender": "Male",
  "password": "Pa$$w0rd",
  "confirmPassword": "Pa$$w0rd"
}



{
  "email": "obasobande@gmail.com",
  "department": "Computer Science",
  "firstName": "Daavid",
  "lastName": "Sobande",
  "gender": "Male",
  "password": "Pa$$w0rd",
  "confirmPassword": "Pa$$w0rd"
}
 */