using Microsoft.AspNetCore.Identity;

namespace Online_Exam_System.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? ProfileImageUrl { get; set; }
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Diploma> Diplomas { get; set; } = new List<Diploma>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string FullName { get; set; }
     
        // 🔁 Refresh Token Support
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
