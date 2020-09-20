using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OldDataImporter.Models
{
    [Table("Users")]
    public class OldUser
    {
        [Key]
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime? DateReg { get; set; }
        public DateTime? LastLogin { get; set; }

        public int? Newsletter { get; set; }

        [Column("ID")]
        public int OldUserDataId { get; set; }

        public virtual OldUserData OldUserData { get; set; }
    }

    [Table("UserData")]
    public class OldUserData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Realname { get; set; }
        public string Email { get; set; }
        public string Homepage { get; set; }
        public string ICQ { get; set; }
        public DateTime? Birthday { get; set; }
        public string Location { get; set; }
        public string Occupation { get; set; }
        public string Groups { get; set; }
        public string FavDemo { get; set; }

        public string FavGroup { get; set; }
        public string Watching { get; set; }
        public string Blabla { get; set; }
    }

    [Table("Demos")]
    public class OldDemo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column("Groups")]
        public string GroupIdStrings { get; set; }

        public int? Jahr { get; set; }
        public int? Monat { get; set; }
        public int? Tag { get; set; }
        public string Picture { get; set; }
        public string Location { get; set; }
        public int Downloads { get; set; }
        public int Votes { get; set; }
        public int VotesCount { get; set; }
        public string Remarks { get; set; }
        public string Upload { get; set; }
        public string Uploader { get; set; }
        public string Submitter { get; set; }
        public int? Video { get; set; }
        public string HiddenParts { get; set; }

        [NotMapped]
        public int Year => Jahr == null ? 0 : Jahr.Value;

        [NotMapped]
        public int Month => Monat ?? 0;

        [NotMapped]
        public int Day => Tag ?? 0;

        [NotMapped]
        public DateTime UploadDate => DateTime.TryParse(Upload, out var outDate) ? outDate : DateTime.MinValue;

        [NotMapped]
        public IEnumerable<string> Pictures => Picture?.Split(';');

        [NotMapped]
        public IEnumerable<int> GroupIds
        {
            get
            {
                var groupArray = GroupIdStrings.Split(';');

                var retVal = new List<int>();

                foreach (var group in groupArray)
                {
                    if (int.TryParse(group, out int outVal))
                        retVal.Add(outVal);
                }
                return retVal;
            }
        }
    }

    [Table("Groups")]
    public class OldGroup
    {
        [Key]
        public int GroupId { get; set; }

        public string GroupName { get; set; }
        public int GroupReleases { get; set; }
        public string GroupLogo { get; set; }
        public DateTime? GroupAddedAt { get; set; }
        public DateTime? GroupLastModifiedAt { get; set; }
    }

    [Table("Party")]
    public class OldParty
    {
        [Column("PartyId")]
        public int Id { get; set; }

        [Column("PartyName")]
        public string Name { get; set; }

        [Column("PartyDateFrom")]
        public string DateFromString { get; set; }

        [Column("PartyDateTo")]
        public string DateToString { get; set; }

        [Column("PartyInformations")]
        public string Information { get; set; }

        [Column("PartyURL")]
        public string Url { get; set; }

        [Column("PartyEMail")]
        public string Email { get; set; }

        [Column("PartyOrganizer")]
        public string Organizer { get; set; }

        [Column("PartyLocation")]
        public string Location { get; set; }

        [Column("PartyCountry")]
        public int? CountryId { get; set; }

        public DateTime From => DateTime.TryParse(DateFromString, out DateTime outDate) ? outDate : DateTime.MinValue;

        public DateTime To => DateTime.TryParse(DateToString, out DateTime outDate) ? outDate : DateTime.MinValue;
    }

    [Table("PartyLink")]
    public class OldPartyLink
    {
        public int Id { get; set; }
        public int DemoId { get; set; }
        public int PartyId { get; set; }
        public int? Ranking { get; set; }
        public string Category { get; set; }
    }

    [Table("Countries")]
    public class OldCountry
    {
        [Column("CountryId")]
        public int Id { get; set; }

        [Column("CountryName")]
        public string Name { get; set; }
    }

    [Table("Votes")]
    public class OldVote
    {
        public int Id { get; set; }
        public int DemoId { get; set; }

        [Column("Datum")]
        public DateTime TimeStamp { get; set; }

        public string Remark { get; set; }

        public string Ip { get; set; }

        [Column("Vote")]
        public int Vote { get; set; }

        public string Writer { get; set; }
        public int? WriterId { get; set; }
    }

    [Table("News")]
    public class OldNews
    {
        public int Id { get; set; }

        [Column("Datum")]
        public DateTime TimeStamp { get; set; }

        public string Text { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
    }

    [Table("FileStorage")]
    public class FileStorage
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Directory { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
        public byte[] Data { get; set; }
    }

    [Table("Downloads")]
    public class OldDownload
    {
        public int ID { get; set; }
        public int DemoId { get; set; }
        public string IP { get; set; }
        public DateTime Datum { get; set; }

        //public DateTime DateParsed
        //{
        //    get
        //    {
        //        DateTime outDate;
        //        return DateTime.TryParse(Datum, out outDate) ? outDate : DateTime.MinValue;
        //    }
        //}
    }

    public class OldDownloadOld
    {
        public int ID { get; set; }
        public int DemoId { get; set; }
        public string IP { get; set; }
        public DateTime Datum { get; set; }

        //public DateTime DateParsed
        //{
        //    get
        //    {
        //        DateTime outDate;
        //        return DateTime.TryParse(Datum, out outDate) ? outDate : DateTime.MinValue;
        //    }
        //}
    }

    [Table("Guestbook")]
    public class OldGuestbook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Homepage { get; set; }
        public string Comment { get; set; }
        public DateTime? Datum { get; set; }
    }

    [Table("Links")]
    public class OldLink
    {
        public int Id { get; set; }

        [MaxLength(63)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Url { get; set; }

        public string Gruppe { get; set; }

        [NotMapped]
        public int GruppeId
        {
            get
            {
                int.TryParse(Gruppe, out var result);
                return result;
            }
        }

        public DateTime? Zeit { get; set; }
        public int? Hits { get; set; }
    }
}