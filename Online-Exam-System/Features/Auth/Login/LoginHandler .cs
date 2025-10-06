﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Models;

namespace Online_Exam_System.Features.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public LoginHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid email or password.");

            // ✅ Get roles
            var roles = await _userManager.GetRolesAsync(user);

            // ✅ Generate JWT using TokenService
            var token = _tokenService.GenerateToken(user, roles);

            return new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                UserId = user.Id,
                UserName= user.UserName,
                FirstName = user.FirstName,
                lastName = user.LastName,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfileImageUrl = user.ProfileImageUrl,
                Roles = roles,
                Token = token
            };
        }
    }
}
