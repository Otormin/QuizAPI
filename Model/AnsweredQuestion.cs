namespace QuizWebApiProject.Model
{
    public class AnsweredQuestion
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string MatricNumber { get; set; }
        public int QuestionId { get; set; }
    }
}
