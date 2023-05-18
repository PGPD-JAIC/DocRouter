using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace DocRouter.Infrastructure
{
    public class NotificationService : INotificationService
    {
        private readonly EmailSettings _emailSettings;

        public NotificationService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public Task SendAsync(MessageDto message)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_emailSettings.Name, _emailSettings.EmailId);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(message.To, message.To);
                emailMessage.To.Add(emailTo);
                emailMessage.Subject = message.Subject;
                BodyBuilder emailBodyBuilder = new BodyBuilder
                {
                    HtmlBody = message.Body + $"<p>You can view this submission in DocRouter <a href=\"{_emailSettings.ApplicationBaseUrl}/Submission/Details/{message.SubmissionId}\">here.</a></p>"
                };
                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_emailSettings.Host, _emailSettings.Port, MailKit.Security.SecureSocketOptions.None);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();
                return Task.CompletedTask;
            }
            catch
            {
                throw;
            }
        }
    }
}
