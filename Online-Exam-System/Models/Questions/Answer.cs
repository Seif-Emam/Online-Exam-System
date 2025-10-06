namespace Online_Exam_System.Models.Questions
{
    public class Answer : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; } = default!;

        public Guid ChoiceId { get; set; }
        public Choice Choice { get; set; } = default!;

        public string UserId { get; set; } = default!;
    }
}
