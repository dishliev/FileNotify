using System.Net;
using System.Net.Mail;

namespace EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string smtpServer = "smtp.example.com";
        private readonly int smtpPort = 587;
        private readonly string smtpUsername = "your_username";
        private readonly string smtpPassword = "your_password";
        private readonly string senderEmail = "sender@example.com";

        public void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new(senderEmail, recipientEmail, subject, body);

                SmtpClient client = new(smtpServer, smtpPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true
                };

                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
