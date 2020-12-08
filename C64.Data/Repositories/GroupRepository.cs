using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public Task<Group> GetWithProductions(int groupId)
        {
            return context.Set<Group>().Include(p => p.Country).Include(p => p.ProductionsGroups).ThenInclude(p => p.Production).FirstOrDefaultAsync(p => p.GroupId == groupId);
        }

        public async Task<Group> GetDetails(int groupId)
        {
            var group = await GetWithProductions(groupId);

            var members = await context.Set<ScenersGroups>().Include(p => p.Scener).Include(p => p.ScenerGroupJobs).Where(p => p.GroupId == groupId).ToListAsync();
            group.ScenersGroups = members;

            return group;
        }

        public async Task<IEnumerable<Scener>> FindWithGroups(Expression<Func<Scener, bool>> predicate)
        {
            var sceners = await context.Set<Scener>()
               .Include(p => p.ScenersGroups).ThenInclude(p => p.Group)
               .Where(predicate).ToListAsync();

            return sceners;
        }

        public async Task<IEnumerable<Group>> FindWithSceners(Expression<Func<Group, bool>> predicate)
        {
            var groups = await context.Set<Group>().Include(p => p.ScenersGroups).ThenInclude(p => p.Scener).Where(predicate).ToListAsync();
            return groups;
        }

        public async Task<IEnumerable<HistoryRecord>> GetHistory(int groupId)
        {
            return await context.Set<HistoryRecord>().Where(p => p.AffectedGroupId == groupId).Include(p => p.User).Include(p => p.AffectedScener).ToListAsync();
        }

        public void UpdateGroupStats()
        {
            var sw = new Stopwatch();
            sw.Start();

            var groups = context.Set<Group>().Include(p => p.ProductionsGroups).ThenInclude(p => p.Production).ThenInclude(p => p.ProductionFiles).Include(p => p.ProductionsGroups).ThenInclude(p => p.Production).ThenInclude(p => p.Ratings).OrderBy(p => p.GroupId);

            foreach (var group in groups)
            {
                UpdateSingleGroupStat(group);
            }

            sw.Stop();
            logger.LogInformation("UpdateGroupStats finished in {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
        }

        public void UpdateSingleGroupStat(Group group)
        {
            var productions = group.ProductionsGroups.Select(p => p.Production);

            var groupDownloads = 0;
            var groupNumberOfRatings = 0;
            var groupSumOfRatings = 0;

            foreach (var production in productions)
            {
                var downloads = production.ProductionFiles?.Sum(p => p.Downloads) ?? 0;

                //// Calculate ratings
                var sumOfRatings = production.Ratings.Sum(p => p.Value);
                var numberOfRatings = production.Ratings.Count();
                var rating = numberOfRatings >= 5 ? (decimal)sumOfRatings / numberOfRatings : 0M;

                groupDownloads += downloads;
                groupNumberOfRatings += numberOfRatings;
                groupSumOfRatings += sumOfRatings;
            }

            group.Downloads = groupDownloads;
            group.NumberOfRatings = groupNumberOfRatings;
            group.SumOfRatings = groupSumOfRatings;
            group.AverageRating = group.NumberOfRatings > 0 ? ((decimal)group.SumOfRatings / group.NumberOfRatings) : 0;
            group.NumberOfReleases = productions.Count();
        }
    }
}