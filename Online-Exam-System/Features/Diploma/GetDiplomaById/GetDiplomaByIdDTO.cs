namespace Online_Exam_System.Features.Diploma.GetDiplomaById
{
    public class GetDiplomaByIdDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
