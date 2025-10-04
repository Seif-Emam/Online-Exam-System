namespace Online_Exam_System.Features.Diploma.AddDiploma
{
    public class AddDiplomaDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? PictureUrl { get; set; }
    }
}
