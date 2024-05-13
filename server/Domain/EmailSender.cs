using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Security;

namespace Koleo.Models
{
    public class EmailSender
    {
        public Task SendEmailAsync(string email, string subject, string messageText, string? attachmentPath)
        {
            var ourEmail = "courier-hub@outlook.com";
            var pw = ConvertToSecureString("Stachson");
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(ourEmail, pw)
            };
            var message = new MailMessage(from: ourEmail, to: email, subject, messageText);
            if(attachmentPath!=null) message.Attachments.Add(new Attachment(attachmentPath));
            return client.SendMailAsync(message);
        }
        private SecureString ConvertToSecureString(string password)
        {
            var secureString = new SecureString();
            foreach (char c in password)
            {
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();
            return secureString;
        }
    }
}