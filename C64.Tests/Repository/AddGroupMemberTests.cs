using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using C64.FrontEnd.Pages;
using C64.FrontEnd.Shared;
using Markdig.Renderers.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace C64.Tests.Repository
{
    public class AddGroupMemberTests
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
        public async Task AddScenerToGroupShouldNotDeleteRelations()
        {
            using var connection = new SqliteConnection("DataSource=:memory:");
            var unitOfWork = CreateUnitOfWork(connection);


            // Arrange - Create Production with a Scener, and an empty group.
            var user = new User() { Id = "1", UserName = "Test" };
            var scener1 = new Scener() { Id = 100, Handle = "Scener1" };
            var production = new Production() { Id = 101, Name = "TestProd" };
            var group1 = new Group() { Id = 102, Name = "Group1" };

            unitOfWork.Users.Add(user);
            unitOfWork.Groups.Add(group1);
            unitOfWork.Sceners.Add(scener1);
            unitOfWork.Productions.Add(production);
            production.ProductionsSceners.Add(new ProductionsSceners() { ProductionId = 101, ScenerId = 100 });
            await unitOfWork.Commit();


            // Act - Add the scener to a group
            var scener = await unitOfWork.Sceners.GetDetails(100);
            var group = await unitOfWork.Groups.GetDetails(102);

            var groupHistory = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWork, group, "1", "1.1.1.1.");

            var add = new AddGroupMember() { Scener = scener, JoinedDateType = DateType.None, LeftDateType = DateType.None, SelectedJobs = new List<Job>() { Job.Coder } };

            groupHistory.AddHistory(HistoryEditProperty.AddGroupMember, add);
            groupHistory.Apply();

            await unitOfWork.Commit();


            // Assert - scener still needs to be in production
            var scenertest = await unitOfWork.Sceners.GetDetails(100);
            Assert.NotEmpty(scenertest.ProductionsSceners);
        }

        [Fact]
        public async Task RemoveScenerToGroupShouldNotDeleteRelations()
        {
            using var connection = new SqliteConnection("DataSource=:memory:");
            var unitOfWork = CreateUnitOfWork(connection);

            // Arrange - Create Production with a Scener, and an empty group.
            var user = new User() { Id = "1", UserName = "Test" };
            var scener1 = new Scener() { Id = 100, Handle = "Scener1" };
            var production = new Production() { Id = 101, Name = "TestProd" };
            var group1 = new Group() { Id = 102, Name = "Group1" };

            unitOfWork.Users.Add(user);
            unitOfWork.Groups.Add(group1);
            unitOfWork.Sceners.Add(scener1);
            unitOfWork.Productions.Add(production);
            production.ProductionsSceners.Add(new ProductionsSceners() { ProductionId = 101, ScenerId = 100 });
            group1.ScenersGroups.Add(new ScenersGroups() { GroupId = 102, ScenerId = 100 });
            await unitOfWork.Commit();


            // Act - Remove the scener from a group
            var group = await unitOfWork.Groups.GetDetails(102);

            var groupHistory = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWork, group, "1", "1.1.1.1.");
            groupHistory.AddHistory(HistoryEditProperty.DeleteGroupMember, 100);
            groupHistory.Apply();

            await unitOfWork.Commit();


            // Assert - scener still needs to be in production
            var scenertest = await unitOfWork.Sceners.GetDetails(100);
            Assert.NotEmpty(scenertest.ProductionsSceners);
        }
    }
}
