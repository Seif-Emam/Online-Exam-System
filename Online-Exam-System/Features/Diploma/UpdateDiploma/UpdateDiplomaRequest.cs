namespace Online_Exam_System.Features.Diploma.UpdateDiploma
{
    public class UpdateDiplomaRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? PictureUrl { get; set; }
    }
}
