using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C64.Services
{
    public class ToLogEmailSender : IEmailSender
    {
        private readonly ILogger<ToLogEmailSender> _logger;

        public ToLogEmailSender(ILogger<ToLogEmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendAdminEmailAsync(MailAddress sender, string subject, string htmlMessage, IEnumerable<MailAttachment> attachments = null, MailAddress replyTo = null, IEnumerable<MailAddress> Cc = null, IEnumerable<MailAddress> Bcc = null)
        {
            _logger.LogInformation($"NullMail to Admin");
            _logger.LogInformation("Subject: {subject}", subject);
            _logger.LogInformation("HtmlMessage: {htmlMessage}", htmlMessage);
            return Task.FromResult(0);
        }

        public Task SendEmailAsync(MailAddress sender, IEnumerable<MailAddress> recipients, string subject, string htmlMessage, IEnumerable<MailAttachment> attachments = null, MailAddress replyTo = null, IEnumerable<MailAddress> Cc = null, IEnumerable<MailAddress> Bcc = null)
        {
            _logger.LogInformation("Email to {@recipients}", recipients);
            _logger.LogInformation("Subject: {subject}", subject);
            _logger.LogInformation("HtmlMessage: {htmlMessage}", htmlMessage);
            return Task.FromResult(0);
        }

        public Task SendUserEmailAsync(MailAddress recipient, string subject, string htmlMessage, IEnumerable<MailAttachment> attachments = null, MailAddress replyTo = null, IEnumerable<MailAddress> Cc = null, IEnumerable<MailAddress> Bcc = null)
        {
            _logger.LogInformation("User Email to {@recipient}", recipient);
            _logger.LogInformation("Subject: {subject}", subject);
            _logger.LogInformation("HtmlMessage: {htmlMessage}", htmlMessage);
            return Task.FromResult(0);
        }
    }
}