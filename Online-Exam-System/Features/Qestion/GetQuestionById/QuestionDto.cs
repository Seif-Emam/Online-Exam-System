using Online_Exam_System.Features.Qestion.GetAllQuestions;

namespace Online_Exam_System.Features.Qestion.GetQuestionById
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? ExamName { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? CategoryName { get; set; }
        public string? Difficulty { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ChoiceDto> Choices { get; set; } = new();
    }
}
