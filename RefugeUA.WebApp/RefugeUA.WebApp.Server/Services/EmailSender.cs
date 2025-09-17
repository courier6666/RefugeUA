using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using RefugeUA.WebApp.Server.Options;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace RefugeUA.WebApp.Server.Services
{
    /// <summary>
    /// Service for sending emails.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="options">The email settings options.</param>
        public EmailSender(IOptions<EmailSettings> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            this.emailSettings = options.Value;
        }

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="htmlMessage">The HTML message body of the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Task.Run(() =>
            {
                // Create the email message
                MailMessage message = new MailMessage
                {
                    From = new MailAddress(this.emailSettings.FromEmail),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };
                message.To.Add(email);

                // Configure the SMTP client
                using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(this.emailSettings.FromEmail, this.emailSettings.Password),
                    EnableSsl = true,
                };

                // Send the email
                smtpClient.Send(message);
            });
        }
    }
}
