using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xunit;

namespace C64.Tests.History
{
    public class DescriptionTestsGroups
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private List<HistoryRecord> addedHistoriesMock = new List<HistoryRecord>();

        public DescriptionTestsGroups()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            var addedHistory = new HistoryRecord();
            unitOfWorkMock.Setup(p => p.Productions.AddHistory(It.IsAny<HistoryRecord>())).Callback<HistoryRecord>(p => addedHistoriesMock.Add(p));

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [Fact]
        public void AddGroupMember()
        {
            var group = new Group { GroupId = 1, Name = "TestGroup" };

            var addGroupMember = new AddGroupMember()
            {
                Scener = new Scener { ScenerId = 2, Handle = "TestHandle" }
            };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.1");
            historyHandler.AddHistory(HistoryEditProperty.AddGroupMember, addGroupMember);
            historyHandler.Apply();

            Assert.Equal("Member 'TestHandle' added to group 'TestGroup'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void DeleteGroupMember()
        {
            var group = new Group { GroupId = 1, Name = "TestGroup" };

            group.ScenerGroups.Add(new ScenersGroups { ScenerId = 1, Scener = new Scener { ScenerId = 1, Handle = "Handle" }, ValidFrom = new DateTime(2020, 1, 1), ValidFromType = DateType.YearMonth, ValidTo = new DateTime(2100, 1, 1), ValidToType = DateType.YearMonthDay, ScenerGroupJobs = new List<ScenerGroupJob> { new ScenerGroupJob { Job = Job.Coder }, new ScenerGroupJob { Job = Job.MoralSupport } } }); ;

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.1");
            historyHandler.AddHistory(HistoryEditProperty.DeleteGroupMember, 1);
            historyHandler.Apply();

            Assert.Equal("Member 'Handle' deleted from group 'TestGroup'", addedHistoriesMock.FirstOrDefault().Description);
        }
    }
}