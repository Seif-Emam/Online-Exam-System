using MediatR;
using Microsoft.AspNetCore.Identity;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Models;

namespace Online_Exam_System.Features.Auth.UpdateUserProfile
{
    public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, UpdateUserProfileResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public UpdateUserProfileHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<UpdateUserProfileResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            // تحديث البيانات لو متاحة
            user.FirstName = request.FirstName ?? user.FirstName;
            user.LastName = request.LastName ?? user.LastName;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.ProfileImageUrl = request.ProfileImage ?? user.ProfileImageUrl;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to update profile: {errors}");
            }

            // ✅ Generate new token if needed
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);

            return new UpdateUserProfileResponse
            {
                Success = true,
                Message = "Profile updated successfully",
                UserId = user.Id,
                UserName = user.UserName,
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
