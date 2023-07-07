namespace EmailService
{
    public interface IEmailService
    {
        void SendEmail(string recipientEmail, string subject, string body);
    }
}
