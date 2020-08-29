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
    public class BasicHistoryTests
    {
        private Production productionUnderTest;
        private Production productionWithRelations;

        public BasicHistoryTests()
        {
            productionUnderTest = new Production
            {
                ProductionId = 1,
                Name = "TestProduction",
                Aka = "TestProductionAka",
                ReleaseDate = new DateTime(2000, 1, 1),
                ReleaseDateType = DateType.YearMonthDay,
                Platform = Platform.C64,
                SubCategory = SubCategory.Demo,
                Remarks = "OldRemark",
                VideoType = VideoType.Pal,
            };

            productionWithRelations = new Production
            {
                ProductionId = 1,
                Name = "TestProduction",
                Aka = "TestProductionAka",
                ReleaseDate = new DateTime(2000, 1, 1),
                ReleaseDateType = DateType.YearMonthDay,
                Platform = Platform.C64,
                SubCategory = SubCategory.Demo,
                ProductionsParties = new List<ProductionsParties>() { new ProductionsParties { PartyId = 1, PartyCategoryId = 2, Rank = 3 } },
                ProductionsGroups = new List<ProductionsGroups>() { new ProductionsGroups { GroupId = 1 }, new ProductionsGroups { GroupId = 2 } },
                HiddenParts = new List<HiddenPart>() { new HiddenPart { HiddenPartId = 1, Description = "OldHiddenPart" } },
                ProductionVideos = new List<ProductionVideo>() { new ProductionVideo { ProductionId = 1, ProductionVideoId = 1, VideoId = "YTID", VideoProvider = VideoProvider.Youtube } },
                ProductionPictures = new List<ProductionPicture>() { new ProductionPicture { ProductionId = 1, ProductionPictureId = 1, Filename = "Test" } },
                ProductionFiles = new List<ProductionFile>() { new ProductionFile { ProductionId = 1, ProductionFileId = 1, Filename = "Test" } },
                ProductionCredits = new List<ProductionCredit>() { new ProductionCredit { ProductionCreditId = 1, ProductionId = 1, ScenerId = 1, Scener = new Scener { ScenerId = 1, Handle = "Hande", ScenersGroups = new List<ScenersGroups>() } } }
            };

            unitOfWorkMock = new Mock<IUnitOfWork>();

            var addedHistory = new HistoryRecord();

            unitOfWorkMock.Setup(p => p.Productions.AddHistory(It.IsAny<HistoryRecord>())).Callback<HistoryRecord>(p => AddHistoryMock(p));
        }

        private Mock<IUnitOfWork> unitOfWorkMock;
        private List<HistoryRecord> addedHistoriesMock = new List<HistoryRecord>();

        private void AddHistoryMock(HistoryRecord addedHistory)
        {
            addedHistoriesMock.Add(addedHistory);
        }

        [Fact]
        public void ChangeProductionName()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Name, "NewName");
            historyHandler.Apply();

            Assert.Equal("TestProduction", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().OldValue));
            Assert.Equal("NewName", JsonConvert.DeserializeObject<string>(addedHistoriesMock.FirstOrDefault().NewValue));
            Assert.Equal("NewName", productionUnderTest.Name);
        }

        [Fact]
        public void ChangeAka()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Aka, "NewAka");
            historyHandler.Apply();

            Assert.Equal("NewAka", productionUnderTest.Aka);
        }

        [Fact]
        public void ChangeReleaseDate()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.ReleaseDate, new PartialDateApplierData { Date = new DateTime(1980, 1, 1), Type = DateType.Year });
            historyHandler.Apply();

            Assert.Single(addedHistoriesMock);
            Assert.Equal(new DateTime(1980, 1, 1), productionUnderTest.ReleaseDate);
            Assert.Equal(DateType.Year, productionUnderTest.ReleaseDateType);
        }

        [Fact]
        public void ChangePlatform()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Platform, Platform.C128);
            historyHandler.Apply();

            Assert.Equal(Platform.C128, productionUnderTest.Platform);
        }

        [Fact]
        public void ChangeSubCategory()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.SubCategory, SubCategory.Magazine);
            historyHandler.Apply();

            Assert.Equal(SubCategory.Magazine, productionUnderTest.SubCategory);
        }

        [Fact]
        public void AddParty()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Party, new PartyApplierData { CategoryId = 1, PartyId = 2, Rank = 3 });
            historyHandler.Apply();

            Assert.Equal(1, productionUnderTest.ProductionsParties.FirstOrDefault().PartyCategoryId);
            Assert.Equal(2, productionUnderTest.ProductionsParties.FirstOrDefault().PartyId);
            Assert.Equal(3, productionUnderTest.ProductionsParties.FirstOrDefault().Rank);
        }

        [Fact]
        public void RemoveParty()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Party, new PartyApplierData { PartyId = 0 });
            historyHandler.Apply();

            Assert.False(productionWithRelations.ProductionsParties.Any());
        }

        [Fact]
        public void EditParty()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Party, new PartyApplierData { CategoryId = 4, PartyId = 5, Rank = 6 });
            historyHandler.Apply();

            Assert.Equal(4, productionUnderTest.ProductionsParties.FirstOrDefault().PartyCategoryId);
            Assert.Equal(5, productionUnderTest.ProductionsParties.FirstOrDefault().PartyId);
            Assert.Equal(6, productionUnderTest.ProductionsParties.FirstOrDefault().Rank);
        }

        [Fact]
        public void AddGroups()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Groups, new List<Group>() { new Group { GroupId = 1 }, new Group { GroupId = 2 } });
            historyHandler.Apply();

            Assert.Equal(2, productionUnderTest.ProductionsGroups.Count());
        }

        [Fact]
        public void RemoveGroups()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Groups, new List<Group>() { });
            historyHandler.Apply();

            Assert.False(productionWithRelations.ProductionsGroups.Any());
        }

        [Fact]
        public void EditGroups()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Groups, new List<Group>() { new Group { GroupId = 5 }, new Group { GroupId = 2 }, new Group { GroupId = 4 } });
            historyHandler.Apply();

            Assert.True(productionWithRelations.ProductionsGroups.Any());
            Assert.Equal(5, productionWithRelations.ProductionsGroups.FirstOrDefault().GroupId);
            Assert.Equal(4, productionWithRelations.ProductionsGroups.LastOrDefault().GroupId);
        }

        [Fact]
        public void AddRemark()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.Remarks, "NewRemark");
            historyHandler.Apply();

            Assert.Equal("NewRemark", productionUnderTest.Remarks);
        }

        [Fact]
        public void AddHiddenPart()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            var newHiddenPart = new List<HiddenPart>();

            newHiddenPart.Add(new HiddenPart { HiddenPartId = 0, Description = "NewHidden" });

            historyHandler.AddHistory(HistoryEditProperty.HiddenParts, newHiddenPart);
            historyHandler.Apply();

            Assert.Single(productionUnderTest.HiddenParts);
        }

        [Fact]
        public void RemoveHiddenPart()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var newHiddenPart = new List<HiddenPart>();

            historyHandler.AddHistory(HistoryEditProperty.HiddenParts, newHiddenPart);
            historyHandler.Apply();

            Assert.False(productionWithRelations.HiddenParts.Any());
        }

        [Fact]
        public void EditHiddenPart()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var newHiddenPart = productionWithRelations.HiddenParts;
            newHiddenPart.FirstOrDefault().Description = "NewHiddenPart";

            historyHandler.AddHistory(HistoryEditProperty.HiddenParts, newHiddenPart);
            historyHandler.Apply();

            Assert.True(productionWithRelations.HiddenParts.Any());
            Assert.Equal("NewHiddenPart", productionWithRelations.HiddenParts.FirstOrDefault().Description);
        }

        [Fact]
        public void ChangeVideoType()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.VideoType, VideoType.Ntsc);
            historyHandler.Apply();

            Assert.Equal(VideoType.Ntsc, productionUnderTest.VideoType);
        }

        [Fact]
        public void EditVideos()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var videos = new List<ProductionVideo>
            {
                new ProductionVideo { ProductionId = 1, ProductionVideoId = 1, VideoId = "NewVideoId" }
            };

            historyHandler.AddHistory(HistoryEditProperty.ProductionVideos, videos);
            historyHandler.Apply();

            Assert.Single(productionWithRelations.ProductionVideos);
            Assert.Equal("NewVideoId", productionWithRelations.ProductionVideos.FirstOrDefault().VideoId);
        }

        [Fact]
        public void RemoveVideos()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var videos = new List<ProductionVideo>();

            historyHandler.AddHistory(HistoryEditProperty.ProductionVideos, videos);
            historyHandler.Apply();

            Assert.Empty(productionWithRelations.ProductionVideos);
        }

        [Fact]
        public void EditPictures()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var pictures = new List<ProductionPicture>();
            pictures.Add(new ProductionPicture { ProductionId = 1, ProductionPictureId = 1, Filename = "Test2" });

            historyHandler.AddHistory(HistoryEditProperty.ProductionPictures, pictures);
            historyHandler.Apply();

            Assert.Single(productionWithRelations.ProductionPictures);
            Assert.Equal("Test2", productionWithRelations.ProductionPictures.FirstOrDefault().Filename);
        }

        [Fact]
        public void RemovePicture()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var pictures = new List<ProductionPicture>();

            historyHandler.AddHistory(HistoryEditProperty.ProductionPictures, pictures);
            historyHandler.Apply();

            Assert.Empty(productionWithRelations.ProductionPictures);
        }

        [Fact]
        public void EditFiles()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var files = new List<ProductionFile>();
            files.Add(new ProductionFile { ProductionId = 1, ProductionFileId = 1, Filename = "Test2" });

            historyHandler.AddHistory(HistoryEditProperty.ProductionFiles, files);
            historyHandler.Apply();

            Assert.Single(productionWithRelations.ProductionFiles);
            Assert.Equal("Test2", productionWithRelations.ProductionFiles.FirstOrDefault().Filename);
        }

        [Fact]
        public void RemoveFiles()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var files = new List<ProductionFile>();

            historyHandler.AddHistory(HistoryEditProperty.ProductionFiles, files);
            historyHandler.Apply();

            Assert.Empty(productionWithRelations.ProductionFiles);
        }

        [Fact]
        public void AddCredits()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionUnderTest, "1", "127.0.0.0");

            var credits = new List<EditCredit>();

            credits.Add(new EditCredit { Id = 1, ScenerId = 1, Added = true, Deleted = false });

            historyHandler.AddHistory(HistoryEditProperty.ProductionCredits, credits);
            historyHandler.Apply();

            Assert.Single(productionUnderTest.ProductionCredits);
        }

        [Fact]
        public void ReplaceCredit()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var credits = new List<EditCredit>();

            credits.Add(new EditCredit { Id = productionWithRelations.ProductionCredits.FirstOrDefault().ProductionCreditId, ScenerHandle = "hmm", ScenerId = productionWithRelations.ProductionCredits.FirstOrDefault().ScenerId, Credit = productionWithRelations.ProductionCredits.FirstOrDefault().Credit });

            credits.FirstOrDefault().Deleted = true;
            credits.Add(new EditCredit { Id = 2, ScenerId = 2, Added = true, Deleted = false });

            historyHandler.AddHistory(HistoryEditProperty.ProductionCredits, credits);
            historyHandler.Apply();

            Assert.Single(productionWithRelations.ProductionCredits);
            Assert.Equal(2, productionWithRelations.ProductionCredits.FirstOrDefault().ScenerId);
        }

        [Fact]
        public void RemoveCredits()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            var credits = new List<EditCredit>();

            credits.Add(new EditCredit { Id = productionWithRelations.ProductionCredits.FirstOrDefault().ProductionCreditId, ScenerHandle = "hmm", ScenerId = productionWithRelations.ProductionCredits.FirstOrDefault().ScenerId, Credit = productionWithRelations.ProductionCredits.FirstOrDefault().Credit });

            credits.FirstOrDefault().Deleted = true;

            historyHandler.AddHistory(HistoryEditProperty.ProductionCredits, credits);
            historyHandler.Apply();

            Assert.Empty(productionWithRelations.ProductionCredits);
        }

        [Fact]
        public void AddProduction()
        {
            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, productionWithRelations, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.AddProduction, null);
            historyHandler.Apply();

            Assert.Equal(2, productionWithRelations.ProductionsGroups.Count());
            Assert.Single(productionWithRelations.ProductionVideos);
            Assert.Single(productionWithRelations.ProductionCredits);
        }
    }
}