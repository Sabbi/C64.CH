using System.Collections.Generic;

namespace C64.Services
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SmtpName { get; set; }
        public string SmtpPassword { get; set; }
        public string SubjectPrefix { get; set; }

        public ICollection<string> AdminMailRecipients { get; set; }
    }
}