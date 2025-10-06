using Microsoft.AspNetCore.Identity;

namespace Online_Exam_System.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? ProfileImageUrl { get; set; }

        // Optional: علاقات إضافية حسب النظام
        // مثال لو المستخدم عنده Exams أو Diplomas أو Roles مخصصة
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Diploma> Diplomas { get; set; } = new List<Diploma>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Convenience Property
        public string FullName { get; set; }
    }
}
