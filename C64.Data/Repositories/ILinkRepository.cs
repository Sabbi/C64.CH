using C64.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface ILinkRepository : IRepository<Link>
    {
        Task<ICollection<LinkCategory>> GetCategories();
    }
}