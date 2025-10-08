using MediatR;

namespace Online_Exam_System.Features.Auth.ForgetPassword.OTP
{
    public record SendOtpCommand(string Email) : IRequest<bool>;

}
