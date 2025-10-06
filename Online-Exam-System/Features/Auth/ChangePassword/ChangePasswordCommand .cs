using MediatR;

namespace Online_Exam_System.Features.Auth.ChangePassword
{
    public record ChangePasswordCommand(
       string CurrentPassword,
       string NewPassword
   ) : IRequest<bool>;
}

