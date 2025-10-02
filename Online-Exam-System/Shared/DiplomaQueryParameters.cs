namespace Online_Exam_System.Shared
{
    public class DiplomaQueryParameters
    {
        public string? Search { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
