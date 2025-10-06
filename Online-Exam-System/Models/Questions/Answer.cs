namespace Online_Exam_System.Models.Questions
{
    public class Answer : BaseEntity
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; } = default!;

        public int ChoiceId { get; set; }
        public Choice Choice { get; set; } = default!;

        public string UserId { get; set; } = default!;
    }
}
