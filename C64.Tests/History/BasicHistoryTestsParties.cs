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
    public class BasicHistoryTestsParties
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private List<HistoryRecord> addedHistoriesMock = new List<HistoryRecord>();

        public BasicHistoryTestsParties()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(p => p.Productions.AddHistory(It.IsAny<HistoryRecord>())).Callback<HistoryRecord>(p => addedHistoriesMock.Add(p));
        }

        [Fact]
        public void ChangePartyName()
        {
            var party = new Party { PartyId = 1, Name = "OldName" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Party, unitOfWorkMock.Object, party, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.PartyName, "NewName");
            historyHandler.Apply();

            Assert.Equal("OldName", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("NewName", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Party, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedPartyId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("NewName", party.Name);
        }

        [Fact]
        public void ChangePartyDescription()
        {
            var party = new Party { PartyId = 1, Description = "OldDescription" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Party, unitOfWorkMock.Object, party, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.PartyDescription, "NewDescription");
            historyHandler.Apply();

            Assert.Equal("OldDescription", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("NewDescription", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Party, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedPartyId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("NewDescription", party.Description);
        }

        [Fact]
        public void ChangePartyFrom()
        {
            var party = new Party { PartyId = 1, From = new DateTime(2020, 1, 1) };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Party, unitOfWorkMock.Object, party, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.PartyFrom, new DateTime(2021, 1, 1));
            historyHandler.Apply();

            Assert.Equal(new DateTime(2020, 1, 1), JsonConvert.DeserializeObject<DateTime>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal(new DateTime(2021, 1, 1), JsonConvert.DeserializeObject<DateTime>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Party, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedPartyId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal(new DateTime(2021, 1, 1), party.From);
        }

        [Fact]
        public void ChangePartyTo()
        {
            var party = new Party { PartyId = 1, To = new DateTime(2020, 1, 1) };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Party, unitOfWorkMock.Object, party, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.PartyTo, new DateTime(2021, 1, 1));
            historyHandler.Apply();

            Assert.Equal(new DateTime(2020, 1, 1), JsonConvert.DeserializeObject<DateTime>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal(new DateTime(2021, 1, 1), JsonConvert.DeserializeObject<DateTime>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Party, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedPartyId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal(new DateTime(2021, 1, 1), party.To);
        }

        [Fact]
        public void ChangePartyUrl()
        {
            var party = new Party { PartyId = 1, Url = "Old" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Party, unitOfWorkMock.Object, party, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.PartyUrl, "New");
            historyHandler.Apply();

            Assert.Equal("Old", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("New", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Party, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedPartyId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("New", party.Url);
        }

        [Fact]
        public void ChangePartyEmail()
        {
            var party = new Party { PartyId = 1, Email = "Old" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Party, unitOfWorkMock.Object, party, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.PartyEmail, "New");
            historyHandler.Apply();

            Assert.Equal("Old", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("New", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Party, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedPartyId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("New", party.Email);
        }

        [Fact]
        public void ChangePartyCountryId()
        {
            var party = new Party { PartyId = 1, CountryId = "Old" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Party, unitOfWorkMock.Object, party, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.PartyCountryId, "New");
            historyHandler.Apply();

            Assert.Equal("Old", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("New", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Party, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedPartyId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("New", party.CountryId);
        }

        [Fact]
        public void ChangePartyLocation()
        {
            var party = new Party { PartyId = 1, Location = "Old" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Party, unitOfWorkMock.Object, party, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.PartyLocation, "New");
            historyHandler.Apply();

            Assert.Equal("Old", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("New", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Party, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedPartyId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("New", party.Location);
        }

        [Fact]
        public void ChangePartyOrganizers()
        {
            var party = new Party { PartyId = 1, Organizers = "Old" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Party, unitOfWorkMock.Object, party, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.PartyOrganizers, "New");
            historyHandler.Apply();

            Assert.Equal("Old", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("New", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal(HistoryEntity.Party, addedHistoriesMock.FirstOrDefault().AffectedEntity);
            Assert.Equal(1, addedHistoriesMock.FirstOrDefault().AffectedPartyId);
            Assert.Null(addedHistoriesMock.FirstOrDefault().AffectedProductionId);
            Assert.Equal("New", party.Organizers);
        }
    }
}