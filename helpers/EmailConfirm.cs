using Castle.Core.Smtp;
using System.Net;
using System.Net.Mail;

namespace Project_1.helpers
{
    public class EmailConfirm : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailConfirm(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Your Mailtrap credentials
            var fMail = "fce19e43f8b46f"; // Mailtrap username
            var fPassword = "123fb387d9be02"; // Mailtrap password

            var theMsg = new MailMessage();
            theMsg.From = new MailAddress(fMail);
            theMsg.Subject = subject;
            theMsg.To.Add(email);
            theMsg.Body = $"<html><body>{htmlMessage}</body></html>";
            theMsg.IsBodyHtml = true;

            // Mailtrap SMTP settings
            var smtpClient = new SmtpClient("smtp.mailtrap.io")
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fMail, fPassword),
                Port = 587 // Mailtrap's SMTP port
            };

            await smtpClient.SendMailAsync(theMsg);
        }


        void IEmailSender.Send(string from, string to, string subject, string messageText)
        {
            throw new NotImplementedException();
        }

        void IEmailSender.Send(MailMessage message)
        {
            throw new NotImplementedException();
        }

        void IEmailSender.Send(IEnumerable<MailMessage> messages)
        {
            throw new NotImplementedException();
        }
    }
}
