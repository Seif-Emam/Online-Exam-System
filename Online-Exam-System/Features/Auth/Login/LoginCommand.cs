using MediatR;

namespace Online_Exam_System.Features.Auth.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;

}
