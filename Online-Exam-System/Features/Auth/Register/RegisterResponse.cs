namespace Online_Exam_System.Features.Auth.Register
{
    public record RegisterResponse(
         bool Success,
         string Message,
         Guid UserId,
         string UserName,
         string FirstName,
         string LastName,
         string FullName,
         string PhoneNumber,
         string Email,
         string ProfileImageUrl,
         IList<string> Roles ,
         string Token

        );
}
