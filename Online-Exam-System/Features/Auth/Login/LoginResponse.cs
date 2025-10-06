namespace Online_Exam_System.Features.Auth.Login
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
