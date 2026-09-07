using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace C64.Data
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly ILogger<UnitOfWork> logger;

        public UnitOfWorkFactory(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILogger<UnitOfWork> logger)
        {
            this.dbContextFactory = dbContextFactory;
            this.logger = logger;
        }

        public IUnitOfWork Create() => new UnitOfWork(dbContextFactory.CreateDbContext(), logger);
    }
}
