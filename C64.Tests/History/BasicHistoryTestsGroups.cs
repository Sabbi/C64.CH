using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace C64.Tests.History
{
    public class BasicHistoryTestsGroups
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private List<HistoryRecord> addedHistoriesMock = new List<HistoryRecord>();

        public BasicHistoryTestsGroups()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(p => p.Productions.AddHistory(It.IsAny<HistoryRecord>())).Callback<HistoryRecord>(p => addedHistoriesMock.Add(p));
        }

        [Fact]
        public void AddGroup()
        {
            var group = new Group { GroupId = 1, Name = "NewGroup" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.AddGroup, group);
            historyHandler.Apply();

            Assert.Equal(HistoryEntity.Group, addedHistoriesMock.FirstOrDefault().AffectedEntity);
        }

        [Fact]
        public void ChangeGroupName()
        {
            var group = new Group { GroupId = 1, Name = "OldName" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Name, "NewName");
            historyHandler.Apply();

            Assert.Equal("OldName", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("NewName", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Group, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedGroupId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("NewName", group.Name);
        }

        [Fact]
        public void ChangeGroupAka()
        {
            var group = new Group { GroupId = 1, Aka = "OldAka" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Aka, "NewAka");
            historyHandler.Apply();

            Assert.Equal("OldAka", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("NewAka", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedGroupId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("NewAka", group.Aka);
        }

        [Fact]
        public void ChangeGroupFoundedDate()
        {
            var group = new Group { GroupId = 1, FoundedDate = new DateTime(2020, 01, 01), FoundedDateType = DateType.Year };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.FoundedDate, new PartialDateApplierData { Date = new DateTime(1980, 1, 1), Type = DateType.YearMonthDay });

            historyHandler.Apply();

            Assert.Single(addedHistoriesMock);
            Assert.Equal(new DateTime(1980, 1, 1), group.FoundedDate);
            Assert.Equal(DateType.YearMonthDay, group.FoundedDateType);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedGroupId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
        }

        [Fact]
        public void ChangeGroupClosedDate()
        {
            var group = new Group { GroupId = 1, ClosedDate = new DateTime(2020, 01, 01), ClosedDateType = DateType.Year };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ClosedDate, new PartialDateApplierData { Date = new DateTime(1980, 1, 1), Type = DateType.YearMonthDay });

            historyHandler.Apply();

            Assert.Single(addedHistoriesMock);
            Assert.Equal(new DateTime(1980, 1, 1), group.ClosedDate);
            Assert.Equal(DateType.YearMonthDay, group.ClosedDateType);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedGroupId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
        }

        [Fact]
        public void ChangeGroupHomepage()
        {
            var group = new Group { GroupId = 1, Url = "oldUrl" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Url, "newUrl");
            historyHandler.Apply();

            Assert.Equal("oldUrl", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("newUrl", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal("newUrl", group.Url);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedGroupId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
        }

        [Fact]
        public void ChangeGroupEmail()
        {
            var group = new Group { GroupId = 1, Email = "oldEmail" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Email, "newEmail");
            historyHandler.Apply();

            Assert.Equal("oldEmail", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("newEmail", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal("newEmail", group.Email);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedGroupId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
        }

        [Fact]
        public void AddGroupMember()
        {
            var group = new Group { GroupId = 1 };

            var addGroupMember = new AddGroupMember()
            {
                Scener = new Scener { ScenerId = 2, Handle = "TestHandle" },
                JoinedDate = DateTime.Now,
                JoinedDateType = DateType.YearMonthDay,
                LeftDate = DateTime.Now,
                LeftDateType = DateType.YearMonthDay,
                SelectedJobs = new[] { Job.Coder, Job.Musician }
            };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.1");
            historyHandler.AddHistory(HistoryEditProperty.AddGroupMember, addGroupMember);
            historyHandler.Apply();

            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedGroupId);
            Assert.True(group.ScenerGroups.Any());
        }

        [Fact]
        public void EditGroupMember()
        {
            var group = new Group { GroupId = 1 };

            group.ScenerGroups.Add(new ScenersGroups { ScenerId = 1, ValidFrom = new DateTime(2020, 1, 1), ValidFromType = DateType.YearMonth, ValidTo = new DateTime(2100, 1, 1), ValidToType = DateType.YearMonthDay, ScenerGroupJobs = new List<ScenerGroupJob> { new ScenerGroupJob { Job = Job.Coder }, new ScenerGroupJob { Job = Job.MoralSupport } } });

            var editGroupMember = new AddGroupMember()
            {
                Scener = new Scener { ScenerId = 1 },
                JoinedDate = DateTime.Now,
                JoinedDateType = DateType.YearMonthDay,
                LeftDate = DateTime.Now,
                LeftDateType = DateType.YearMonthDay,
                SelectedJobs = new[] { Job.Coder, Job.Musician, Job.Swapper }
            };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.1");
            historyHandler.AddHistory(HistoryEditProperty.EditGroupMember, editGroupMember);
            historyHandler.Apply();

            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedGroupId);

            Assert.Equal(1, group.ScenerGroups.Count);
            Assert.Equal(3, group.ScenerGroups.FirstOrDefault().ScenerGroupJobs.Count());
            Assert.Contains(Job.Coder, group.ScenerGroups.FirstOrDefault().ScenerGroupJobs.Select(p => p.Job));
            Assert.Contains(Job.Musician, group.ScenerGroups.FirstOrDefault().ScenerGroupJobs.Select(p => p.Job));
            Assert.Contains(Job.Swapper, group.ScenerGroups.FirstOrDefault().ScenerGroupJobs.Select(p => p.Job));

            Assert.True(group.ScenerGroups.Any());
        }

        [Fact]
        public void DeleteGroupMember()
        {
            var group = new Group { GroupId = 1 };

            group.ScenerGroups.Add(new ScenersGroups { ScenerId = 1, Scener = new Scener { ScenerId = 1, Handle = "Handle" }, ValidFrom = new DateTime(2020, 1, 1), ValidFromType = DateType.YearMonth, ValidTo = new DateTime(2100, 1, 1), ValidToType = DateType.YearMonthDay, ScenerGroupJobs = new List<ScenerGroupJob> { new ScenerGroupJob { Job = Job.Coder }, new ScenerGroupJob { Job = Job.MoralSupport } } }); ;

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.1");
            historyHandler.AddHistory(HistoryEditProperty.DeleteGroupMember, 1);
            historyHandler.Apply();

            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedGroupId);

            Assert.Equal(0, group.ScenerGroups.Count);
        }
    }
}