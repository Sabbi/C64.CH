using C64.Services.Properties;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace C64.Services
{
    public class MailKitSender : IEmailSender
    {
        private readonly EmailSettings settings;

        public MailKitSender(Microsoft.Extensions.Options.IOptions<EmailSettings> settings)
        {
            this.settings = settings.Value;
        }

        public async Task SendEmailAsync(MailAddress sender, IEnumerable<MailAddress> recipients, string subject, string htmlMessage, IEnumerable<MailAttachment> attachments = null, MailAddress replyTo = null, IEnumerable<MailAddress> cc = null, IEnumerable<MailAddress> bcc = null)
        {
            var message = new MimeMessage();

            AddParticipants(message, sender, recipients, replyTo, cc, bcc);

            message.Subject = $"{settings.SubjectPrefix}{subject}";

            var bodyBuilder = new BodyBuilder();

            var image = bodyBuilder.LinkedResources.Add("maillogo.jpg", Resources.maillogo_jpg);
            image.ContentId = MimeUtils.GenerateMessageId();

            bodyBuilder.HtmlBody = string.Concat(string.Format(_headerHtml, image.ContentId), htmlMessage, _footerHtml);

            AddAttachments(bodyBuilder, attachments);

            message.Body = bodyBuilder.ToMessageBody();

            await SendMessageAsync(message);
        }

        public Task SendAdminEmailAsync(MailAddress sender, string subject, string htmlMessage, IEnumerable<MailAttachment> attachments = null, MailAddress replyTo = null, IEnumerable<MailAddress> Cc = null, IEnumerable<MailAddress> Bcc = null)
        {
            var recipientString = settings.AdminMailRecipients;

            var recipients = new List<MailAddress>();

            foreach (var recipient in recipientString)
            {
                recipients.Add(new MailAddress(recipient));
            }

            return SendEmailAsync(sender, recipients, subject, htmlMessage, attachments, replyTo, Cc, Bcc);
        }

        public Task SendUserEmailAsync(MailAddress recipient, string subject, string htmlMessage, IEnumerable<MailAttachment> attachments = null, MailAddress replyTo = null, IEnumerable<MailAddress> Cc = null, IEnumerable<MailAddress> Bcc = null)
        {
            var sender = new MailAddress(settings.SenderEmail, settings.SenderName);
            return SendEmailAsync(sender, new List<MailAddress>() { recipient }, subject, htmlMessage, attachments, replyTo, Cc, Bcc);
        }

        private void AddAttachments(BodyBuilder bodyBuilder, IEnumerable<MailAttachment> attachments)
        {
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                    bodyBuilder.Attachments.Add(attachment.Filename, attachment.Content);
            }
        }

        private void AddParticipants(MimeMessage message, MailAddress sender, IEnumerable<MailAddress> recipients, MailAddress replyTo, IEnumerable<MailAddress> cc, IEnumerable<MailAddress> bcc)
        {
            if (sender == null)
                message.From.Add(new MailboxAddress(settings.SenderName, settings.SenderEmail));
            else
                message.From.Add(ToMailboxAddress(sender));

            if (replyTo != null)
                message.ReplyTo.Add(ToMailboxAddress(replyTo));

            foreach (var address in recipients)
                message.To.Add(ToMailboxAddress(address));

            if (cc != null)
            {
                foreach (var address in cc)
                    message.Cc.Add(ToMailboxAddress(address));
            }

            if (bcc != null)
            {
                foreach (var address in bcc)
                    message.Bcc.Add(ToMailboxAddress(address));
            }
        }

        private async Task SendMessageAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                // Accept self-signed / invalid certificates
                client.ServerCertificateValidationCallback = delegate (object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return (true); };

                await client.ConnectAsync(settings.MailServer, settings.MailPort);
                if (!string.IsNullOrEmpty(settings.SmtpName))
                    await client.AuthenticateAsync(settings.SmtpName, settings.SmtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return;
            }
        }

        private MailboxAddress ToMailboxAddress(MailAddress input)
        {
            return new MailboxAddress(input.Name, input.Address);
        }

        private string _headerHtml => "<div align=\"right\"><img src = \"cid:{0}\"></div><br>";
        private string _footerHtml => $"<hr><small>&copy {System.DateTime.Now.Year} C64.CH - if you have received this message in error, please notify us by replying to the message</small>";
    }
}