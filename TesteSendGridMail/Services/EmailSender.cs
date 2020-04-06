using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TesteSendGridMail.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageOptions> optionsAcessor)
        {
            Options = optionsAcessor.Value;
        }
        public AuthMessageOptions Options { get; }

        public Task SendEmailAsync (string email, string subject, string message)
        {
            return Execute(Options.SendGridKey,  subject,  message, email);
           
        }
        private Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()

            {
                From = new EmailAddress("DionneyScrok15@gmail.com", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message

            };
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);
            return client.SendEmailAsync(msg);
        }

    }
}
