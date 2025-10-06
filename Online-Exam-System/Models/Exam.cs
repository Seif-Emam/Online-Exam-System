using Online_Exam_System.Models.Questions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Exam_System.Models
{
    public class Exam : BaseEntity
    {
        public required string Title { set; get; }
        public string? PictureUrl { get; set; } = default!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly Duration { get; set; }
        public Guid? UserId { get; set; }

        #region Relations
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
        public Guid DiplomaId { get; set; }           // Foreign Key
        public Diploma? Diploma { get; set; }

        public ICollection<Question> Questions { get; set; }
        #endregion
    }
}
