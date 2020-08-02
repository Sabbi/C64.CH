using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace C64.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public ApplicationDbContext ApplicationDbContext => context as ApplicationDbContext;
    }
}