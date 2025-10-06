namespace Online_Exam_System.Features.Auth.UpdateUserProfile
{
    public record UpdateUserProfileRequest
 (
     string? FirstName,
     string? LastName,
     string? PhoneNumber,
     IFormFile? ProfileImage
 );
}
