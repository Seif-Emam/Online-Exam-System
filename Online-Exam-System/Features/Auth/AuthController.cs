﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Features.Auth.ChangePassword;
using Online_Exam_System.Features.Auth.ForgetPassword.OTP;
using Online_Exam_System.Features.Auth.ForgetPassword.ResetPassword;
using Online_Exam_System.Features.Auth.GetCurrentUser;
using Online_Exam_System.Features.Auth.Login;
using Online_Exam_System.Features.Auth.Register;
using Online_Exam_System.Features.Auth.UpdateUserProfile;

namespace Online_Exam_System.Features.Auth
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UpdateUserProfileOrchestrator _updateUserProfileOrchestrator;  
        public AuthController(IMediator mediator , UpdateUserProfileOrchestrator updateUserProfileOrchestrator) { 
        
            _mediator = mediator;
            _updateUserProfileOrchestrator = updateUserProfileOrchestrator;


        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var command = new RegisterCommand(dto);
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("user-info")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { statusCode = 401, message = "Invalid token or not authenticated." });

            var result = await _mediator.Send(new GetCurrentUserQuery(userId));
            return Ok(result);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Password changed successfully." });
        }

        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateUserProfileRequest request)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { statusCode = 401, message = "Invalid token or not authenticated." });

            var response = await _updateUserProfileOrchestrator.UpdateUserProfileAsync(
                Guid.Parse(userId),
                request
            );

            return Ok(response);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] SendOtpCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "OTP sent successfully to your email." });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "OTP verified successfully." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Password reset successfully." });
        }


    }
}
