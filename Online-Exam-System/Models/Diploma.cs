namespace Online_Exam_System.Models
{
    public class Diploma : BaseEntity
    {
        public required string Title { set; get; }
        public  string? Description { set; get; }
        public  string? PictureUrl { set; get; }

        public ICollection<Exam>? Exams { set; get; } = new List<Exam>();




    }
}
