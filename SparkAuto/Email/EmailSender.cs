using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.Email
{
    public class EmailSender : IEmailSender
    {
        public EmailOptions Options { get; set; }

        public EmailSender(IOptions<EmailOptions> emailOptions)
        {
            Options = emailOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(Options.SendGridApiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("akshaytiwari2008@gmail.com", "Spark Auto"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            msg.AddContent(MimeType.Text, "and easy to do anywhere, even with C#");
            msg.AddTo(new EmailAddress(email));

            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
