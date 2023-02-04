using Application.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using MailKit.Security;

namespace API.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _email;
        private readonly string _password;

        public EmailService(IConfiguration configuration)
        {
            _smtpServer = configuration["SMTP:Server"];
            _smtpPort = int.Parse(configuration["SMTP:Port"]);
            _email = configuration["SMTP:Email"];
            _password = configuration["SMTP:Password"];
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("WoT-STATS", _email));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, true);
                await client.AuthenticateAsync(_email, _password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
