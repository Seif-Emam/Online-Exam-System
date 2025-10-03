namespace Online_Exam_System.Features.Exam.UpdateExam
{
    public class UpdateExamDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PictureUrl { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly Duration { get; set; }
    }
}
