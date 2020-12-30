using System;

namespace C64.Data.Entities
{
    public class Statistic
    {
        public int StatisticId { get; set; }
        public DateTime LastUpdate { get; set; }
        public int NumberOfDownloads { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfProductions { get; set; }
        public int NumberOfSceners { get; set; }
        public int NumberOfParties { get; set; }
        public int NumberOfGroups { get; set; }
        public int NumberOfRatings { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfGuestbookEntries { get; set; }
        public int NumberOfLinks { get; set; }
    }
}