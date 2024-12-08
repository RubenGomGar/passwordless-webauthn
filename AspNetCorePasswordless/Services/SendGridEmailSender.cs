using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AspNetCorePasswordless.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly ILogger<SendGridEmailSender> _logger;
        private readonly SendGridOptions options;

        public SendGridEmailSender(
            ILogger<SendGridEmailSender> logger,
            IOptions<SendGridOptions> options)
        {
            _logger = logger;
            this.options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(options.ApiKey);
            var from = new EmailAddress(options.FromEmail, options.FromName);
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlMessage);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode >= HttpStatusCode.BadRequest)
            {
                _logger.LogError("Unable to send email to {to}. Status code: {statusCode}", email, response.StatusCode);
            }
            else
            {
                _logger.LogDebug("Email successfully sent to {to}", email);
            }
        }
    }
}
