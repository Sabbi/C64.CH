using C64.Data.Entities;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetWithFavorites(string userId);
        Task<bool> IsFavorite(string userId, int productionId);
        void SetFavorite(string userId, int productionId, bool set);
    }
}