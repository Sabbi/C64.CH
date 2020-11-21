using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OldDataImporter.Models;

namespace OldDataImporter
{
    public class OldApplicationDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public string RequiredDbVersion = "3.9.0.1";

        public OldApplicationDbContext(DbContextOptions<OldApplicationDbContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
            Database.SetCommandTimeout(300);
        }

        public virtual DbSet<OldParty> OldParties { get; set; }
        public virtual DbSet<OldGroup> OldGroups { get; set; }
        public virtual DbSet<OldDemo> OldDemos { get; set; }
        public virtual DbSet<OldPartyLink> OldPartyLinks { get; set; }
        public virtual DbSet<OldDownload> OldDownloads { get; set; }
        public virtual DbSet<OldDownloadOld> OldDownloadOlds { get; set; }
        public virtual DbSet<OldUser> OldUsers { get; set; }
        public virtual DbSet<OldUserData> OldUserDatas { get; set; }
        public virtual DbSet<OldVote> OldVotes { get; set; }
        public virtual DbSet<OldNews> OldNews { get; set; }
        public virtual DbSet<OldGuestbook> OldGuestbooks { get; set; }
        public virtual DbSet<OldLink> OldLinks { get; set; }
    }
}