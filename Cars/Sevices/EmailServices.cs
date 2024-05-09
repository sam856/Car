using Cars.Helper;
using Cars.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Cars.NewFolder;
using Cars.Models;

namespace Cars.Services
{
    public class EmailServices : IEmailService
    {
        private readonly Emailsettings emailSettings;
        private readonly AppDbContext appDbContext;

        public EmailServices(IOptions<Emailsettings> options, AppDbContext appDbContext)
        {
            this.emailSettings = options.Value;
            this.appDbContext = appDbContext;
        }

        public async Task SendEmailAsync(Mailrequest mailrequest, string emailBody)
        {
            var email = new MimeMessage
            {
                Subject = "car" // Set subject to "car"
            };

            email.From.Add (new MailboxAddress(emailSettings.Displayname, emailSettings.Email));


            email.To.Add(MailboxAddress.Parse(mailrequest.Email));
            email.Body = new TextPart("plain") { Text = emailBody };
            using var client = new SmtpClient();
            client.Connect(emailSettings.Host, emailSettings.port, SecureSocketOptions.StartTls);
            client.Authenticate(emailSettings.Email, emailSettings.Password);
            await client.SendAsync(email);
            client.Disconnect(true);
        }
    }
}
