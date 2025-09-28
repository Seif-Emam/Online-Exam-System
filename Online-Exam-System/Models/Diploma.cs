namespace Online_Exam_System.Models
{
    public class Diploma
    {
        public  Guid id { set; get; } 
        public required string Title { set; get; }
        public  string? Description { set; get; }
        public  string? PictureUrl { set; get; }
        public  DateTime CreatedAt { set; get; } = DateTime.Now;
        public  DateTime UpdatedAt { set; get; } = DateTime.Now;
        public  bool IsDeleted { set; get; } = false;




    }
}
