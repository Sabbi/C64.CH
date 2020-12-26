using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class GuestbookEntry
    {
        public int GuestbookEntryId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Homepage { get; set; }

        [MaxLength(65535)]
        public string Comment { get; set; }

        public DateTime WrittenAt { get; set; }

        [MinLength(36)]
        [MaxLength(36)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [MaxLength(40)]
        public string Ip { get; set; }

        public string HomepageFixed
        {
            get
            {
                if (string.IsNullOrEmpty(Homepage) || Homepage == "http://" || Homepage.Length < 10)
                    return null;

                return Homepage;
            }
        }

        public string EmailFixed
        {
            get
            {
                if (string.IsNullOrEmpty(Email))
                    return null;

                return Email.Replace("@", "[A]");
            }
        }
    }
}