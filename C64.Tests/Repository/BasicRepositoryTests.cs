using C64.Data;
using C64.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace C64.Tests.Repository
{
    public class BasicRepositoryTests
    {
        private ApplicationDbContext context;

        private ApplicationDbContext CreateContext(SqliteConnection connection)
        {
            var configuration = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value).Returns("false");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;

            var context = new ApplicationDbContext(options, new NullLoggerFactory(), configuration.Object);
            context.Database.EnsureCreated();
            return context;
        }

        private IUnitOfWork CreateUnitOfWork(SqliteConnection connection)
        {
            connection.Open();
            context = CreateContext(connection);
            var unitOfWork = new UnitOfWork(context, new NullLogger<UnitOfWork>());
            return unitOfWork;
        }

        [Fact]
        public async Task GetWithProductions_ShouldReturnCountry()
        {
            using var connection = new SqliteConnection("DataSource=:memory:");

            var unitOfWork = CreateUnitOfWork(connection);

            context.Groups.Add(new Group { GroupId = 1, Name = "TestGroup", CountryId = "CH" });
            await context.SaveChangesAsync();

            var group = await unitOfWork.Groups.GetWithProductions(1);

            Assert.NotNull(group.Country);
            Assert.Equal("Switzerland", group.Country.Name);
        }
    }
}