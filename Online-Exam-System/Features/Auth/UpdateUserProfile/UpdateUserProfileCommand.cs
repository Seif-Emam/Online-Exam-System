using MediatR;

namespace Online_Exam_System.Features.Auth.UpdateUserProfile
{
    public record UpdateUserProfileCommand(
     Guid UserId,
     string? FirstName,
     string? LastName,
     string? PhoneNumber,
     string? ProfileImage
 ) : IRequest<UpdateUserProfileResponse>;
}
