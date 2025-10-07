using MediatR;

namespace Online_Exam_System.Features.Qestion.GetAllQuestions
{
    public class QuestionDto 
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string ExamName { get; set; } = default!;
        public List<ChoiceDto> Choices { get; set; } = new();
    }
}
