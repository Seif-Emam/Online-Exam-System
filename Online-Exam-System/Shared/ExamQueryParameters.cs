namespace Online_Exam_System.Shared
{
     public class ExamQueryParameters
        {
            public int PageNumber { get; set; } = 1;
            public int PageSize { get; set; } = 10; 

            public string? Search { get; set; }
          //  public string? CategoryName { get; set; }
            public DateOnly? StartDate { get; set; }
            public DateOnly? EndDate { get; set; }
            public int? Duration { get; set; }
        }

 }
