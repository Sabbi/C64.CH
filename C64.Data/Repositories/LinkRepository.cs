using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public class LinkRepository : Repository<Link>, ILinkRepository
    {
        public LinkRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<ICollection<LinkCategory>> GetCategories()
        {
            return await context.Set<LinkCategory>().Where(p => p.Selectable).ToListAsync();
        }
    }
}