using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public class StatisticRepository : Repository<Statistic>, IStatisticRepository
    {
        public StatisticRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<Statistic> Get()
        {
            return await context.Set<Statistic>().FirstOrDefaultAsync();
        }

        public async Task UpdateStats()
        {
            var existing = await context.Set<Statistic>().FirstOrDefaultAsync();
            var stat = existing == null ? new Statistic() : existing;

            stat.NumberOfUsers = await context.Set<User>().CountAsync();
            stat.NumberOfDownloads = await context.Set<ProductionFile>().SumAsync(p => p.Downloads);
            stat.NumberOfProductions = await context.Set<Production>().CountAsync();
            stat.NumberOfSceners = await context.Set<Scener>().CountAsync();
            stat.NumberOfParties = await context.Set<Party>().CountAsync();
            stat.NumberOfGroups = await context.Set<Group>().CountAsync();
            stat.NumberOfRatings = await context.Set<Rating>().CountAsync();
            stat.NumberOfComments = await context.Set<Comment>().CountAsync();
            stat.NumberOfGuestbookEntries = await context.Set<GuestbookEntry>().CountAsync();
            stat.NumberOfLinks = await context.Set<Link>().CountAsync();

            stat.LastUpdate = DateTime.Now;

            context.Entry(stat).State = stat.StatisticId == 0 ? EntityState.Added : EntityState.Modified;
        }
    }
}