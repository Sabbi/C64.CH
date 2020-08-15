using C64.Data.Entities;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetWithFavorites(string userId);
    }
}