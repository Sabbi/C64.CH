using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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

        public Task<bool> IsFavorite(string userId, int productionId)
        {
            return context.Set<UserFavorite>().AnyAsync(p => p.ProductionId == productionId && p.UserId == userId);
        }

        public void SetFavorite(string userId, int productionId, bool set)
        {
            if (!set)
            {
                var favorite = context.Set<UserFavorite>().Where(p => p.UserId == userId && p.ProductionId == productionId);
                context.RemoveRange(favorite);
                return;
            }
            context.Set<UserFavorite>().Add(new UserFavorite { ProductionId = productionId, UserId = userId, Added = DateTime.Now });
        }
    }
}