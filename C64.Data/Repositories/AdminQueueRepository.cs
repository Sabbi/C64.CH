using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace C64.Data.Repositories
{
    public class AdminQueueRepository : Repository<AdminQueue>, IAdminQueueRepository
    {
        public AdminQueueRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}