using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class Download
    {
        public Download()
        { }

        public Download(int productionFileId, string ip, string referer, string userId)
        {
            ProductionFileId = productionFileId;
            Ip = ip;
            Referer = referer;
            UserId = userId;
        }

        public int DownloadId { get; set; }
        public int ProductionFileId { get; set; }
        public virtual ProductionFile ProductionFile { get; set; }

        public DateTime DownloadedOn { get; set; } = DateTime.Now;

        [MaxLength(40)] // 39 characters is appropriate to store IPv6 addresses (Stack Overflow)
        public string Ip { get; set; }

        [MaxLength(36)]
        public string UserId { get; set; }

        [MaxLength(2047)]
        public string Referer { get; set; }

        public virtual User User { get; set; }
    }
}