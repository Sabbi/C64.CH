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
    }
}