using Online_Exam_System.Models;

namespace Online_Exam_System.Contarcts
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);

    }
}
