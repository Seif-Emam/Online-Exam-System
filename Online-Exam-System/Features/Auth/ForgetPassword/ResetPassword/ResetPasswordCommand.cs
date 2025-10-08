using MediatR;

namespace Online_Exam_System.Features.Auth.ForgetPassword.ResetPassword
{
    public record ResetPasswordCommand(string Email, string NewPassword) : IRequest<bool>;

}
