namespace Online_Exam_System.Features.Diploma.UpdateDiploma
{
    public class UpdateDiplomaDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? PictureUrl { get; set; }

    }
}
