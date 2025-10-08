using MediatR;

namespace Online_Exam_System.Features.Auth.ForgetPassword.OTP
{
    public record VerifyOtpCommand(string Email, string OtpCode) : IRequest<bool>;

}
