namespace Online_Exam_System.Features.Exam.GetAll
{
    public class GetAllExamsDTOs
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PictureUrl { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly Duration { get; set; }
    }
}
