using System.Collections.Generic;
using System.Threading.Tasks;

namespace C64.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailAddress sender, IEnumerable<MailAddress> recipients, string subject, string htmlMessage, IEnumerable<MailAttachment> attachments = null, MailAddress replyTo = null, IEnumerable<MailAddress> Cc = null, IEnumerable<MailAddress> Bcc = null);

        Task SendAdminEmailAsync(MailAddress sender, string subject, string htmlMessage, IEnumerable<MailAttachment> attachments = null, MailAddress replyTo = null, IEnumerable<MailAddress> Cc = null, IEnumerable<MailAddress> Bcc = null);

        Task SendUserEmailAsync(MailAddress recipient, string subject, string htmlMessage, IEnumerable<MailAttachment> attachments = null, MailAddress replyTo = null, IEnumerable<MailAddress> Cc = null, IEnumerable<MailAddress> Bcc = null);
    }
}