namespace Online_Exam_System.Models.Questions
{
    public class Choice : BaseEntity
    {
        public string Content { get; set; } = default!;
        public bool IsCorrect { get; set; }

        #region relations
        public int QuestionId { get; set; }
        public Question Question { get; set; } = default!;
        #endregion
    }
}
