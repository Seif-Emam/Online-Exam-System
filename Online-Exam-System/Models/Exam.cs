namespace Online_Exam_System.Models
{
    public class Exam : BaseEntity
    {
        public required string Title { set; get; }
        public string? PictureUrl { get; set; } = default!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly Duration { get; set; }

        public Guid DiplomaId { get; set; }           // Foreign Key
        public Diploma? Diploma { get; set; }

        //add -after relations Diploma Dropdownlist
        //
    }
}
