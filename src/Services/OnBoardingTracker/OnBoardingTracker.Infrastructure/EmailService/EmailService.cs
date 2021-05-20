using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using OnBoardingTracker.Infrastructure.EmailService.Abstract;
using OnBoardingTracker.Infrastructure.EmailService.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnBoardingTracker.Infrastructure.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions emailOptions;

        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            this.emailOptions = emailOptions.Value;
        }

        public async Task Send(IEnumerable<string> to, string subject, string body)
        {
            // create message
            var from = emailOptions.FromEmail;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            foreach (var recipient in to)
            {
                email.To.Add(MailboxAddress.Parse(recipient));
            }

            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Text) { Text = body };

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(emailOptions.SmtpHost, int.Parse(emailOptions.SmtpPort), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailOptions.SmtpUser, emailOptions.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
