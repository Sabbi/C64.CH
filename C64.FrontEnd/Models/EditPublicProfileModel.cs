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

        [MaxLength(255)]
        [NotRequiredEmailAddress]
        public string PublicEmail { get; set; }

        [NotRequiredUrl]
        [MaxLength(1024)]
        public string Homepage { get; set; }

        public string Icq { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }

        public string CountryId { get; set; }

        [MaxLength(255)]
        public string Occupation { get; set; }

        [MaxLength(255)]
        public string Watching { get; set; }

        [MaxLength(65535)]
        public string Blabla { get; set; }

        [MaxLength(1023)]
        public string Groups { get; set; }

        [MaxLength(255)]
        public string FavDemos { get; set; }

        [MaxLength(255)]
        public string FavGroups { get; set; }

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
            Location = user.Location;
            PublicEmail = user.PublicEmail;
            Groups = user.Groups;
            FavDemos = user.FavDemos;
            FavGroups = user.FavGroups;
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
            user.Location = Location;
            user.PublicEmail = PublicEmail;
            user.Groups = Groups;
            user.FavDemos = FavDemos;
            user.FavGroups = FavGroups;
        }
    }
}