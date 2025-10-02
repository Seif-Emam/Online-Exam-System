namespace Online_Exam_System.Features.Diploma.GetAllDiplomas
{
    public class GetAllDiplomasDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
    }
}
