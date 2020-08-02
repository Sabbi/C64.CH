using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace C64.Data.Repositories
{
    public class ScenerRepository : Repository<Scener>, IScenerRepository
    {
        public ScenerRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}