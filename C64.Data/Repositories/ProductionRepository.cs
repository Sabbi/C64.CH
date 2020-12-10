using C64.Data.Entities;
using C64.Data.Models;
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
    public class ProductionRepository : Repository<Production>, IProductionRepository
    {
        public ProductionRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<Production> GetDetails(int productionId)
        {
            var production = await context.Set<Production>()
                .Include(p => p.ProductionPictures)
                .Include(p => p.ProductionFiles)
                .Include(p => p.ProductionsGroups).ThenInclude(p => p.Group)
                .Include(p => p.HiddenParts)
                .Include(p => p.ProductionVideos)
                .Include(p => p.ProductionsParties).ThenInclude(p => p.Party)
                .Include(p => p.ProductionsParties).ThenInclude(p => p.PartyCategory)
                .Include(p => p.User)
                .Include(p => p.SubmitterUser)
                .FirstOrDefaultAsync(p => p.ProductionId == productionId);

            if (production == null)
                return production;

            // Inject comments, ratings and credits with separate queries, they might blow up the record quite a bit otherwise
            var comments = await context.Set<Comment>().Include(p => p.User).Where(p => p.ProductionId == productionId).ToListAsync();
            production.Comments = comments;

            var ratings = await context.Set<Rating>().Include(p => p.User).Where(p => p.ProductionId == productionId).ToListAsync();
            production.Ratings = ratings;

            var credits = await context.Set<ProductionCredit>().Include(p => p.Scener).ThenInclude(p => p.ScenersGroups).ThenInclude(p => p.Group).Where(p => p.ProductionId == productionId).ToListAsync();
            production.ProductionCredits = credits;

            // Resort Pictures
            production.ProductionPictures = production.ProductionPictures.OrderBy(p => p.Sort).ToList();

            return production;
        }

        public async Task<ProductionFile> GetProductionFileByProductionId(int productionId)
        {
            var productionFiles = await context.Set<ProductionFile>().Where(p => p.ProductionId == productionId).ToListAsync();
            return productionFiles.OrderByDescending(p => p.ProductionFileId).FirstOrDefault();
        }

        public async Task<IEnumerable<Production>> GetForScener(int scenerId)
        {
            var productions = await context.Set<Production>().Include(p => p.ProductionsGroups).ThenInclude(p => p.Group).Include(p => p.ProductionPictures).Include(p => p.ProductionCredits).ThenInclude(p => p.Scener).Where(p => p.ProductionCredits.Any(q => q.ScenerId == scenerId)).ToListAsync();
            return productions;
        }

        public async Task AddDownload(string filename, string remoteIp, string referer, string userId = null)
        {
            var productionFile = await context.Set<ProductionFile>().FirstOrDefaultAsync(p => p.Filename == filename);

            if (productionFile != null)
            {
                productionFile.Downloads++;

                var download = new Download(productionFile.ProductionFileId, remoteIp, referer, userId);
                context.Add(download);
            }
        }

        public async Task<IEnumerable<ProductionFile>> ProductionFiles(int productionId)
        {
            return await context.Set<ProductionFile>().Where(p => p.ProductionId == productionId).ToListAsync();
        }

        public async Task<IEnumerable<ProductionPicture>> ProductionPictures(int productionId)
        {
            return await context.Set<ProductionPicture>().Where(p => p.ProductionId == productionId).ToListAsync();
        }

        public Task<ProductionFile> GetFile(int productionFileId)
        {
            return context.Set<ProductionFile>().FirstOrDefaultAsync(p => p.ProductionFileId == productionFileId);
        }

        public Task<PaginatedResult<Production>> GetPaginatedWithGroups(Expression<Func<Production, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize)
        {
            var query = context.Set<Production>().Include(p => p.ProductionPictures).Include(p => p.ProductionsGroups).ThenInclude(p => p.Group);
            return FindPaginated(query, predicate, orderBy, isSortedAscending, page, pageSize);
        }

        public async Task<Production> GetRandomProduction()
        {
            var count = await context.Set<Production>().CountAsync();

            var skip = new Random().Next(count);

            return await context.Set<Production>().Include(p => p.ProductionPictures).Include(p => p.ProductionsGroups).ThenInclude(p => p.Group).Skip(skip).Take(1).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Rating>> GetRatings(int productionId)
        {
            return await context.Set<Rating>().Where(p => p.ProductionId == productionId).AsNoTracking().ToListAsync();
        }

        public void UpdateProductionStats()
        {
            var sw = new Stopwatch();
            sw.Start();

            var productions = context.Set<Production>().Include(p => p.ProductionFiles).Include(p => p.Ratings).Include(p => p.ProductionVideos).Include(p => p.HiddenParts).Include(p => p.Comments).Include(p => p.ProductionsParties).OrderBy(p => p.ProductionId);

            foreach (var production in productions)
            {
                UpdateSingleProductionStat(production);
            }

            sw.Stop();
            logger.LogInformation("UpdateProductionStats finished in {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
        }

        public void UpdateSingleProductionStat(Production production)
        {
            production.SumOfRatings = production.Ratings.Sum(p => p.Value);
            production.NumberOfRatings = production.Ratings.Count();
            production.AverageRating = production.NumberOfRatings >= 5 ? (decimal)production.SumOfRatings / production.NumberOfRatings : 0M;
            production.Downloads = production.ProductionFiles.Sum(p => p.Downloads);

            production.IsPartyRelease = production.ProductionsParties.Any();
            production.NumberOfComments = production.Comments.Count();
            production.NumberOfHiddenParts = production.HiddenParts.Count();
            production.NumberOfVideos = production.ProductionVideos.Count();
        }

        public async Task<IEnumerable<Download>> GetDownloads(int productionId, int numberOfMonths)
        {
            var startFrom = DateTime.Now.AddMonths(numberOfMonths * -1);

            var allProductionFiles = await context.Set<ProductionFile>().Where(p => p.ProductionId == productionId).Select(p => p.ProductionFileId).ToListAsync();

            return await context.Set<Download>().Where(p => p.DownloadedOn > startFrom && allProductionFiles.Contains(p.ProductionFileId)).ToListAsync();
        }

        public async Task<IEnumerable<HistoryRecord>> GetHistory(int productionId)
        {
            return await context.Set<HistoryRecord>().Where(p => p.AffectedProductionId == productionId).Include(p => p.User).ToListAsync();
        }

        public void AddHistory(HistoryRecord historyProduction)
        {
            context.Set<HistoryRecord>().Add(historyProduction);
        }
    }
}