using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace C64.Data.Repositories
{
    public class LinkRepository : Repository<Link>, ILinkRepository
    {
        public LinkRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}