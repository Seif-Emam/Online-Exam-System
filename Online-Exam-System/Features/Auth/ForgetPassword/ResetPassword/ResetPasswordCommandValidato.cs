﻿using FluentValidation;
using Online_Exam_System.Features.Auth.ChangePassword;

namespace Online_Exam_System.Features.Auth.ForgetPassword.ResetPassword
{
    public class ResetPasswordCommandValidato : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidato()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters")
             .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("Password must contain at least one special character");

        }
    }
}