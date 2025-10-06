using MediatR;
using Microsoft.AspNetCore.Identity;
using Online_Exam_System.Features.Auth.GetCurrentUser;
using Online_Exam_System.Models;

namespace Online_Exam_System.Features.Auth.Login
{
    public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetCurrentUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<LoginResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var roles = await _userManager.GetRolesAsync(user);

            return new LoginResponse
            {
                Success = true,
                Message = "User retrieved successfully",
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                lastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                Roles = roles,

            };
        }
    }
}
