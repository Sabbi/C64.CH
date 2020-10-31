using C64.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;

namespace C64.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILoggerFactory loggerFactory, IConfiguration configuration) : base(options)
        {
            _loggerFactory = loggerFactory;
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            try
            {
                if (configuration.GetValue<bool>("DetailedErrors"))
                    optionsBuilder.EnableDetailedErrors();
            }
            catch
            {
                ///
            }

            //optionsBuilder.EnableSensitiveDataLogging(true);
        }

        public virtual DbSet<User> ApplicationUsers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Production> Productions { get; set; }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<HiddenPart> HiddenParts { get; set; }
        public virtual DbSet<ProductionFile> ProductionFiles { get; set; }
        public virtual DbSet<ProductionPicture> ProductionPictures { get; set; }
        public virtual DbSet<ProductionVideo> ProductionVideos { get; set; }
        public virtual DbSet<Download> Downloads { get; set; }
        public virtual DbSet<ProductionsGroups> ProductionsGroups { get; set; }
        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<PartiesGroups> PartiesGroups { get; set; }
        public virtual DbSet<PartiesSceners> PartiesSceners { get; set; }

        public virtual DbSet<PartyCategory> PartyCategories { get; set; }

        public virtual DbSet<ProductionsParties> ProductionsParties { get; set; }

        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<SiteInfo> SiteInfos { get; set; }
        public virtual DbSet<GuestbookEntry> GuestbookEntries { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<LinkCategory> LinkCategories { get; set; }

        public virtual DbSet<UserFavorite> UserFavorites { get; set; }

        public virtual DbSet<Scener> Sceners { get; set; }
        public virtual DbSet<ScenersSceners> ScenerSceners { get; set; }
        public virtual DbSet<ProductionCredit> ProductionCredits { get; set; }

        public virtual DbSet<DbFile> DbFiles { get; set; }

        public virtual DbSet<HistoryRecord> HistoryRecords { get; set; }

        public virtual DbSet<Tool> Tools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-To-Many-Relation: Production <-> Group
            modelBuilder.Entity<ProductionsGroups>()
                .HasKey(p => new { p.ProductionId, p.GroupId });

            modelBuilder.Entity<ProductionsGroups>()
                .HasOne(p => p.Production)
                .WithMany(p => p.ProductionsGroups);

            modelBuilder.Entity<ProductionsGroups>()
                .HasOne(p => p.Group)
                .WithMany(p => p.ProductionsGroups);

            // Many-To-Many-Relation: Production <-> ProductionParty
            modelBuilder.Entity<ProductionsParties>()
               .HasKey(p => new { p.ProductionId, p.PartyId });
            modelBuilder.Entity<ProductionsParties>()
                .HasOne(p => p.Production)
                .WithMany(p => p.ProductionsParties);

            modelBuilder.Entity<ProductionsParties>()
                .HasOne(p => p.Party)
                .WithMany(p => p.ProductionsParties);

            // Many-To-Many-Relation: Production <-> User (as Favorites)
            modelBuilder.Entity<UserFavorite>()
                .HasOne(p => p.Production)
                .WithMany(p => p.UserFavorites);

            modelBuilder.Entity<UserFavorite>()
                .HasOne(p => p.Production)
                .WithMany(p => p.UserFavorites);

            modelBuilder.Entity<UserFavorite>()
                .HasKey(p => new { p.UserId, p.ProductionId });

            // Many-to-Many self Scener <-> Scener
            modelBuilder.Entity<ScenersSceners>()
                .HasKey(p => new { p.ScenerId, p.ScenerToId });

            // Many-To-Many Party <-> Groups
            modelBuilder.Entity<PartiesGroups>()
                .HasKey(p => new { p.PartyId, p.GroupId });
            modelBuilder.Entity<PartiesGroups>()
                .HasOne(p => p.Party)
                .WithMany(p => p.PartiesGroups);
            modelBuilder.Entity<PartiesGroups>()
                .HasOne(p => p.Group)
                .WithMany(p => p.PartiesGroups);

            // Many-To-Many Party <-> Sceners
            modelBuilder.Entity<PartiesSceners>()
                .HasKey(p => new { p.PartyId, p.ScenerId });
            modelBuilder.Entity<PartiesSceners>()
                .HasOne(p => p.Party)
                .WithMany(p => p.PartiesSceners);
            modelBuilder.Entity<PartiesSceners>()
                .HasOne(p => p.Scener)
                .WithMany(p => p.PartiesSceners);

            // Additional indexes (or indices, depending on your philosophy)
            modelBuilder.Entity<Download>().HasIndex(p => new { p.ProductionFileId });
            modelBuilder.Entity<Download>().HasIndex(p => new { p.DownloadedOn });
            modelBuilder.Entity<Download>().HasIndex(p => new { p.ProductionFileId, p.DownloadedOn });
            modelBuilder.Entity<DbFile>().HasIndex(p => new { p.Container, p.FileName });
            modelBuilder.Entity<Tool>().HasIndex(p => new { p.Show });

            modelBuilder.Entity<Country>().HasData(GetCountries());
            modelBuilder.Entity<LinkCategory>().HasData(GetLinkCategories());
        }

        private IEnumerable<Country> GetCountries()
        {
            try
            {
                var content = System.IO.File.ReadAllText("Data/countries.json");
                return JsonSerializer.Deserialize<IEnumerable<Country>>(content);
            }
            catch
            {
                return new List<Country>();
            }
        }

        private IEnumerable<LinkCategory> GetLinkCategories()
        {
            try
            {
                var content = System.IO.File.ReadAllText("Data/linkcategories.json");
                return JsonSerializer.Deserialize<IEnumerable<LinkCategory>>(content);
            }
            catch
            {
                return new List<LinkCategory>();
            }
        }
    }
}