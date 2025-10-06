using MediatR;
using Online_Exam_System.Features.Auth.Login;

namespace Online_Exam_System.Features.Auth.GetCurrentUser
{
    public record GetCurrentUserQuery(string UserId) : IRequest<LoginResponse>;

}
