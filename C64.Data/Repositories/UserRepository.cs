using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public ApplicationDbContext ApplicationDbContext => context as ApplicationDbContext;

        public Task<User> GetWithFavorites(string userId)
        {
            return context.Set<User>().Include(p => p.Favorites).FirstOrDefaultAsync(p => p.Id == userId);
        }
    }
}