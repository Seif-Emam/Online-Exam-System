namespace Online_Exam_System.Contarcts
{
    public interface IMailKitEmailService
    {
       
        Task SendEmailAsync(string toEmail, string subject, string body);

    }
}
