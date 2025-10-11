using MediatR;

namespace Online_Exam_System.Features.Auth.Logout
{
    public record LogoutCommand(Guid UserId) : IRequest<LogoutResponse>;

}
