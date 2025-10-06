namespace Online_Exam_System.Models.Questions
{
    public class Question : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Type { get; set; } = default!; // text - Image

        #region Relations 
        public int ExamId { get; set; }
        public Exam Exam { get; set; } = default!;
        public ICollection<Choice> Choices { get; set; }

        #endregion


    }
}
