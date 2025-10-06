﻿namespace Online_Exam_System.Features.Auth.Login
{
    using FluentValidation;

    namespace Online_Exam_System.Features.Auth.Login
    {
        public class LoginValidator : AbstractValidator<LoginCommand>
        {
            public LoginValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Invalid email format.");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.");
            }
        }
    }

}
