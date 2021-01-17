using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace C64.Data.Repositories
{
    public class ChangeLogRepository : Repository<ChangeLogEntry>, IChangeLogRepository
    {
        public ChangeLogRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}