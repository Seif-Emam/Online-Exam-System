using MediatR;

namespace Online_Exam_System.Features.Auth.Register
{
    public record RegisterCommand(RegisterDto RegisterDto) : IRequest<RegisterResponse>;

}
