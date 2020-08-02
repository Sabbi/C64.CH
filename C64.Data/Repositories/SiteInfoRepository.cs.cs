using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace C64.Data.Repositories
{
    public class SiteInfoRepository : Repository<SiteInfo>, ISiteInfoRepository
    {
        public SiteInfoRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}