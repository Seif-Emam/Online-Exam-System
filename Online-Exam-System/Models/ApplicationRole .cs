using Microsoft.AspNetCore.Identity;

namespace Online_Exam_System.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }
}
