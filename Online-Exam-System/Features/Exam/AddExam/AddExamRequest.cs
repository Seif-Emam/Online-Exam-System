namespace Online_Exam_System.Features.Exam.AddExam
{
    public class AddExamRequest
    {
        public string Title { get; set; } = string.Empty;
        public IFormFile PictureUrl { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly Duration { get; set; }
    }
}
