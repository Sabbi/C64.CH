using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace C64.Data.Repositories
{
    public class GuestbookEntryRepository : Repository<GuestbookEntry>, IGuestbookEntryRepository
    {
        public GuestbookEntryRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}