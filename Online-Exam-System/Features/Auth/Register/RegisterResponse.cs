namespace Online_Exam_System.Features.Auth.Register
{
    public record RegisterResponse(
         bool Success,
         string Message,
         string Token,
         string FullName,
         string Email,
         string ProfileImageUrl,
         IList<string> Roles // ✅ جديد
     );
}
