
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SmartTicket.Models;

namespace SmartTicket.Services
{
    public class MailService : IEmailService
    {

        public MailService(IOptions<EmailConfig> _config)
        {
            emailConfig = _config.Value;
        }

        private readonly EmailConfig emailConfig;

        public async Task<string?> sendMailAsync(string maailTo, string subject, string body)
        {
            var message = new MimeMessage
            {
                Sender = MailboxAddress.Parse(emailConfig.Username),
                Subject = subject,
            };

            message.To.Add(MailboxAddress.Parse(maailTo));

            var emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.HtmlBody = body;

            message.Body = emailBodyBuilder.ToMessageBody();
            message.From.Add(new MailboxAddress(emailConfig.From, emailConfig.Username));

            var smtp = new SmtpClient();
            smtp.Connect(emailConfig.Smtp, emailConfig.Port, true);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Authenticate(emailConfig.Username, emailConfig.Password);

            var res = await smtp.SendAsync(message);

            return res;

        }
    }
}
