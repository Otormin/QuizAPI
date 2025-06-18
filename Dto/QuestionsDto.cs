namespace QuizWebApiProject.Dto
{
    public class QuestionsDto
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string Question { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
    }
}
