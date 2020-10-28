using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using C64.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xunit;

namespace C64.Tests.History
{
    public class DescriptionTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private List<HistoryRecord> addedHistoriesMock = new List<HistoryRecord>();

        public DescriptionTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            var addedHistory = new HistoryRecord();
            unitOfWorkMock.Setup(p => p.Productions.AddHistory(It.IsAny<HistoryRecord>())).Callback<HistoryRecord>(p => addedHistoriesMock.Add(p));

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [Fact]
        public void ChangeName()
        {
            var production = new Production { Name = "OldName" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Name, "NewName");
            historyHandler.Apply();

            Assert.Equal("Name changed from 'OldName' to 'NewName'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void ChangeAka()
        {
            var production = new Production { Aka = "OldAka" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Aka, "NewAka");
            historyHandler.Apply();

            Assert.Equal("Aka changed from 'OldAka' to 'NewAka'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void AddAka()
        {
            var production = new Production { };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Aka, "NewAka");
            historyHandler.Apply();

            Assert.Equal("Aka set to 'NewAka'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void RemoveAka()
        {
            var production = new Production { Aka = "OldAka" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Aka, string.Empty);
            historyHandler.Apply();

            Assert.Equal("Removed Aka 'OldAka'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void ChangeReleaseDate()
        {
            var production = new Production { ReleaseDate = new DateTime(2020, 01, 01), ReleaseDateType = DateType.YearMonth };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ReleaseDate, new PartialDate() { Date = new DateTime(2020, 02, 02), Type = DateType.YearMonthDay });
            historyHandler.Apply();

            Assert.Equal("Release date changed from 'January 2020' to 'February 2nd, 2020'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void RemoveReleaseDate()
        {
            var production = new Production { ReleaseDate = new DateTime(2020, 01, 01), ReleaseDateType = DateType.YearMonth };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ReleaseDate, new PartialDate() { Date = new DateTime(1900, 1, 1), Type = DateType.None });
            historyHandler.Apply();

            Assert.Equal("Release date removed", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void AddReleaseDate()
        {
            var production = new Production { ReleaseDate = new DateTime(1900, 01, 01), ReleaseDateType = DateType.None };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ReleaseDate, new PartialDate() { Date = new DateTime(2020, 02, 02), Type = DateType.YearMonthDay });
            historyHandler.Apply();

            Assert.Equal("Release date set to 'February 2nd, 2020'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void ChangePlatform()
        {
            var production = new Production { Platform = Platform.C64 };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Platform, Platform.C128);
            historyHandler.Apply();

            Assert.Equal("Platform changed from 'C64' to 'C128'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void ChangeSubcategory()
        {
            var production = new Production { SubCategory = SubCategory.Demo };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.SubCategory, SubCategory.Intro);
            historyHandler.Apply();

            Assert.Equal("Subcategory changed from 'Demo' to 'Intro'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void ChangeParty()
        {
            var production = new Production() { ProductionsParties = new List<ProductionsParties>() { new ProductionsParties() { PartyId = 1, Party = new Party { Name = "OldParty" }, PartyCategoryId = 1, PartyCategory = new PartyCategory { Name = "OldCategory" }, Rank = 2 } } };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            var partyapplierData = new PartyApplierData() { CategoryId = 2, CategoryName = "NewCategory", PartyName = "NewParty", PartyId = 2, Rank = 1 };

            historyHandler.AddHistory(HistoryEditProperty.Party, partyapplierData);
            historyHandler.Apply();

            Assert.Equal("Partyinformation changed from 'OldParty/OldCategory/2' to 'NewParty/NewCategory/1'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void AddParty()
        {
            var production = new Production() { ProductionsParties = new List<ProductionsParties>() };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            var partyapplierData = new PartyApplierData() { CategoryId = 2, CategoryName = "NewCategory", PartyName = "NewParty", PartyId = 2, Rank = 1 };

            historyHandler.AddHistory(HistoryEditProperty.Party, partyapplierData);
            historyHandler.Apply();

            Assert.Equal("Partyinformation added: 'NewParty/NewCategory/1'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void RemoveParty()
        {
            var production = new Production() { ProductionsParties = new List<ProductionsParties>() { new ProductionsParties() { PartyId = 1, Party = new Party { Name = "OldParty" }, PartyCategoryId = 1, PartyCategory = new PartyCategory { Name = "OldCategory" }, Rank = 2 } } };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            var partyapplierData = new PartyApplierData() { PartyId = 0 };

            historyHandler.AddHistory(HistoryEditProperty.Party, partyapplierData);
            historyHandler.Apply();

            Assert.Equal("Partyinformation removed", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void NotChangeParty()
        {
            var production = new Production() { ProductionsParties = new List<ProductionsParties>() { new ProductionsParties() { PartyId = 1, Party = new Party { Name = "OldParty" }, PartyCategoryId = 1, PartyCategory = new PartyCategory { Name = "OldCategory" }, Rank = 2 } } };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            var partyapplierData = new PartyApplierData() { CategoryId = 1, CategoryName = "OldCategory", PartyName = "OldParty", PartyId = 1, Rank = 2 };

            historyHandler.AddHistory(HistoryEditProperty.Party, partyapplierData);
            historyHandler.Apply();

            Assert.Null(addedHistoriesMock.FirstOrDefault());
        }

        [Fact]
        public void ChangeRemarks()
        {
            var production = new Production { Remarks = "OldRemark" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Remarks, "NewRemarkverylongdonotdisplayall");
            historyHandler.Apply();

            Assert.Equal("Remark changed to 'NewRemarkverylo...'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void AddRemarks()
        {
            var production = new Production { };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Remarks, "NewRemarkverylongdonotdisplayall");
            historyHandler.Apply();

            Assert.Equal("Remark changed to 'NewRemarkverylo...'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void RemoveRemark()
        {
            var production = new Production { Remarks = "OldRemarks" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Remarks, string.Empty);
            historyHandler.Apply();

            Assert.Equal("Remark removed", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void ChangeHiddenParts()
        {
            var production = new Production { HiddenParts = new List<HiddenPart>() { new HiddenPart { HiddenPartId = 1, Description = "OldHiddenPartlongdesc" } } };
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            var newHiddenParts = new List<HiddenPart>();
            newHiddenParts.Add(new HiddenPart { HiddenPartId = 1, Description = "1NewHiddenPartlongedesc" });

            historyHandler.AddHistory(HistoryEditProperty.HiddenParts, newHiddenParts);
            historyHandler.Apply();

            Assert.Equal("Hiddenparts updated", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void AddHiddenParts()
        {
            var production = new Production();
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            var newHiddenParts = new List<HiddenPart>();
            newHiddenParts.Add(new HiddenPart { HiddenPartId = 1, Description = "1NewHiddenPartlongedesc" });
            newHiddenParts.Add(new HiddenPart { HiddenPartId = 2, Description = "2NewHiddenPartlongedesc" });
            historyHandler.AddHistory(HistoryEditProperty.HiddenParts, newHiddenParts);
            historyHandler.Apply();

            Assert.Equal("Hiddenparts added: '1NewHiddenPartl...', '2NewHiddenPartl...'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void RemoveHiddenParts()
        {
            var production = new Production { HiddenParts = new List<HiddenPart>() { new HiddenPart { HiddenPartId = 1, Description = "OldHiddenPartlongdesc" } } };
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            var newHiddenParts = new List<HiddenPart>();
            historyHandler.AddHistory(HistoryEditProperty.HiddenParts, newHiddenParts);
            historyHandler.Apply();

            Assert.Equal("Hiddenparts removed", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void ChangeVideoType()
        {
            var production = new Production { VideoType = VideoType.Unknown };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.VideoType, VideoType.Pal);
            historyHandler.Apply();

            Assert.Equal("Videotype changed from 'Unknown' to 'Pal'", addedHistoriesMock.FirstOrDefault().Description);
        }

        [Fact]
        public void AddProduction()
        {
            var production = new Production { Name = "NewDemo", SubCategory = SubCategory.Demo };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.AddProduction, production);
            historyHandler.Apply();

            Assert.Equal("Added 'NewDemo', a new Demo", addedHistoriesMock.FirstOrDefault().Description);
        }
    }
}