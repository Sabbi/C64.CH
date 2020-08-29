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
            var group = new Group { Name = "OldName" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Name, "NewName");
            historyHandler.Apply();

            Assert.Equal("OldName", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("NewName", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal("NewName", group.Name);
        }

        [Fact]
        public void ChangeGroupAka()
        {
            var group = new Group { Aka = "OldAka" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Aka, "NewAka");
            historyHandler.Apply();

            Assert.Equal("OldAka", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("NewAka", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal("NewAka", group.Aka);
        }

        [Fact]
        public void ChangeGroupFoundedDate()
        {
            var group = new Group { FoundedDate = new DateTime(2020, 01, 01), FoundedDateType = DateType.Year };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.FoundedDate, new PartialDateApplierData { Date = new DateTime(1980, 1, 1), Type = DateType.YearMonthDay });

            historyHandler.Apply();

            Assert.Single(addedHistoriesMock);
            Assert.Equal(new DateTime(1980, 1, 1), group.FoundedDate);
            Assert.Equal(DateType.YearMonthDay, group.FoundedDateType);
        }

        [Fact]
        public void ChangeGroupClosedDate()
        {
            var group = new Group { ClosedDate = new DateTime(2020, 01, 01), ClosedDateType = DateType.Year };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Group, unitOfWorkMock.Object, group, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ClosedDate, new PartialDateApplierData { Date = new DateTime(1980, 1, 1), Type = DateType.YearMonthDay });

            historyHandler.Apply();

            Assert.Single(addedHistoriesMock);
            Assert.Equal(new DateTime(1980, 1, 1), group.ClosedDate);
            Assert.Equal(DateType.YearMonthDay, group.ClosedDateType);
        }

        //[Fact]
        //public void ChangeReleaseDate()
        //{
        //    var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

        //    historyHandler.AddHistory(HistoryEditProperty.ReleaseDate, new PartialDateApplierData { Date = new DateTime(1980, 1, 1), Type = DateType.Year });
        //    historyHandler.Apply();

        //    Assert.Single(addedHistoriesMock);
        //    Assert.Equal(new DateTime(1980, 1, 1), productionUnderTest.ReleaseDate);
        //    Assert.Equal(DateType.Year, productionUnderTest.ReleaseDateType);
        //}
    }
}