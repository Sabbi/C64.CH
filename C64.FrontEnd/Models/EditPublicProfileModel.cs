using C64.Data.Entities;
using C64.Data.Models;
using C64.FrontEnd.Helpers;
using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class EditPublicProfileModel
    {
        public PartialDate Birthday { get; set; } = new PartialDate();

        [MaxLength(255)]
        public string RealName { get; set; }

        [NotRequiredUrl]
        [MaxLength(1024)]
        public string Homepage { get; set; }

        public string Icq { get; set; }

        public string CountryId { get; set; }

        [MaxLength(255)]
        public string Occupation { get; set; }

        [MaxLength(255)]
        public string Watching { get; set; }

        [MaxLength(65535)]
        public string Blabla { get; set; }

        public void Load(User user)
        {
            RealName = user.Realname;
            Homepage = user.Homepage;
            Icq = user.Icq;
            CountryId = user.CountryId;
            Occupation = user.Occupation;
            Watching = user.Watching;
            Blabla = user.Blabla;
            Birthday.Date = user.Birthday;
            Birthday.Type = user.BirthdayType;
        }

        public void Update(User user)
        {
            user.Realname = RealName;
            user.Homepage = Homepage;
            user.Icq = Icq;
            user.CountryId = CountryId;
            user.Occupation = Occupation;
            user.Watching = Watching;
            user.Blabla = Blabla;
            user.Birthday = Birthday.Date;
            user.BirthdayType = Birthday.Type;
        }
    }
}