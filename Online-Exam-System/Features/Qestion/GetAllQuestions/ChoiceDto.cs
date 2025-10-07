namespace Online_Exam_System.Features.Qestion.GetAllQuestions
{
    public class ChoiceDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public bool IsCorrect { get; set; }
    }
}
