using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using C64.Data.Models;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace C64.Tests.History
{
    public class BasicHistoryTestsSceners
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private List<HistoryRecord> addedScenersMock = new List<HistoryRecord>();

        public BasicHistoryTestsSceners()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(p => p.Productions.AddHistory(It.IsAny<HistoryRecord>())).Callback<HistoryRecord>(p => addedScenersMock.Add(p));
        }

        [Fact]
        public void AddScener()
        {
            var scener = new Scener { Handle = "Handle", RealName = "Realname" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Scener, unitOfWorkMock.Object, scener, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.AddScener, scener);
            historyHandler.Apply();

            Assert.Equal(HistoryEntity.Scener, addedScenersMock.FirstOrDefault().AffectedEntity);
        }

        [Fact]
        public void ChangeScenerHandle()
        {
            var scener = new Scener { ScenerId = 1, Handle = "Handle" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Scener, unitOfWorkMock.Object, scener, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ScenerHandle, "NewHandle");
            historyHandler.Apply();

            Assert.Equal("Handle", JsonConvert.DeserializeObject<string>(addedScenersMock.FirstOrDefault().OldValue));
            Assert.Equal("NewHandle", JsonConvert.DeserializeObject<string>(addedScenersMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Scener, addedScenersMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedScenersMock.FirstOrDefault().AffectedScenerId);
            Assert.Null(addedScenersMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("NewHandle", scener.Handle);
        }

        [Fact]
        public void ChangeScenerAka()
        {
            var scener = new Scener { ScenerId = 1, Aka = "Aka" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Scener, unitOfWorkMock.Object, scener, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ScenerAka, "NewAka");
            historyHandler.Apply();

            Assert.Equal("Aka", JsonConvert.DeserializeObject<string>(addedScenersMock.FirstOrDefault().OldValue));
            Assert.Equal("NewAka", JsonConvert.DeserializeObject<string>(addedScenersMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Scener, addedScenersMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedScenersMock.FirstOrDefault().AffectedScenerId);
            Assert.Null(addedScenersMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("NewAka", scener.Aka);
        }

        [Fact]
        public void ChangeScenerRealname()
        {
            var scener = new Scener { ScenerId = 1, RealName = "Realname" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Scener, unitOfWorkMock.Object, scener, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ScenerRealName, "NewRealname");
            historyHandler.Apply();

            Assert.Equal("Realname", JsonConvert.DeserializeObject<string>(addedScenersMock.FirstOrDefault().OldValue));
            Assert.Equal("NewRealname", JsonConvert.DeserializeObject<string>(addedScenersMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Scener, addedScenersMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedScenersMock.FirstOrDefault().AffectedScenerId);
            Assert.Null(addedScenersMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("NewRealname", scener.RealName);
        }

        [Fact]
        public void ChangeScenerBirtdhay()
        {
            var scener = new Scener { ScenerId = 1, Birthday = new System.DateTime(1980, 1, 1), BirthdayType = DateType.Year };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Scener, unitOfWorkMock.Object, scener, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Birthday, new PartialDate { Date = new DateTime(1990, 1, 1), Type = DateType.YearMonthDay });
            historyHandler.Apply();

            Assert.Single(addedScenersMock);
            Assert.Equal(new DateTime(1990, 1, 1), scener.Birthday);
            Assert.Equal(DateType.YearMonthDay, scener.BirthdayType);
            Assert.Null(addedScenersMock.FirstOrDefault().AffectedGroupId);
            Assert.Equal(1, addedScenersMock.FirstOrDefault().AffectedScenerId);
        }

        [Fact]
        public void ChangeScenerLocation()
        {
            var scener = new Scener { ScenerId = 1, Location = "OldLocation" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Scener, unitOfWorkMock.Object, scener, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ScenerLocation, "New Location");
            historyHandler.Apply();

            Assert.Single(addedScenersMock);
            Assert.Equal("New Location", scener.Location);
            Assert.Null(addedScenersMock.FirstOrDefault().AffectedGroupId);
            Assert.Equal(1, addedScenersMock.FirstOrDefault().AffectedScenerId);
        }

        [Fact]
        public void ChangeScenerCountryId()
        {
            var scener = new Scener { ScenerId = 1, CountryId = "Old" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Scener, unitOfWorkMock.Object, scener, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ScenerCountryId, "New");
            historyHandler.Apply();

            Assert.Equal("Old", JsonConvert.DeserializeObject<string>(addedScenersMock.FirstOrDefault().OldValue));
            Assert.Equal("New", JsonConvert.DeserializeObject<string>(addedScenersMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Scener, addedScenersMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedScenersMock.FirstOrDefault().AffectedScenerId);
            Assert.Null(addedScenersMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("New", scener.CountryId);
        }

        [Fact]
        public void EditScenerJobs()
        {
            var scener = new Scener { ScenerId = 1, CountryId = "Old" };

            scener.Jobs.Add(new ScenersJobs { Job = Job.Coder });
            scener.Jobs.Add(new ScenersJobs { Job = Job.MoralSupport });

            var selectedJobs = new[] { Job.Coder, Job.Musician, Job.Swapper };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Scener, unitOfWorkMock.Object, scener, "1", "127.0.0.1");
            historyHandler.AddHistory(HistoryEditProperty.ScenerJobs, selectedJobs);
            historyHandler.Apply();

            Assert.Equal(1, addedScenersMock.FirstOrDefault().AffectedScenerId);

            Assert.Equal(3, scener.Jobs.Count());
            Assert.Contains(Job.Coder, scener.Jobs.Select(p => p.Job));
            Assert.Contains(Job.Musician, scener.Jobs.Select(p => p.Job));
            Assert.Contains(Job.Swapper, scener.Jobs.Select(p => p.Job));
        }
    }
}