using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using C64.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace C64.Tests.History
{
    public class UndoTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private List<HistoryRecord> addedHistoriesMock = new List<HistoryRecord>();

        public UndoTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            var addedHistory = new HistoryRecord();
            unitOfWorkMock.Setup(p => p.Productions.AddHistory(It.IsAny<HistoryRecord>())).Callback<HistoryRecord>(p => addedHistoriesMock.Add(p));
        }

        [Fact]
        public void UndoNameandAka()
        {
            var production = new Production { Name = "OldName", Aka = null };

            var doHistoryHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");
            doHistoryHandler.AddHistory(HistoryEditProperty.Name, "NewName");
            doHistoryHandler.AddHistory(HistoryEditProperty.Aka, "NewAka");
            doHistoryHandler.Apply();

            Assert.Equal("NewName", production.Name);

            var undoHistoryHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");
            undoHistoryHandler.Undo(addedHistoriesMock);
            undoHistoryHandler.Apply();

            Assert.Equal("OldName", production.Name);
            Assert.Null(production.Aka);
        }

        [Fact]
        public void MultipleUndo()
        {
            var production = new Production { Name = "OldName" };

            var doHistoryHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");
            doHistoryHandler.AddHistory(HistoryEditProperty.Name, "NewName1");
            doHistoryHandler.Apply();

            Assert.Equal("NewName1", production.Name);

            doHistoryHandler.AddHistory(HistoryEditProperty.Name, "NewName2");
            doHistoryHandler.Apply();

            Assert.Equal("NewName2", production.Name);

            var undoHistoryHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");
            undoHistoryHandler.Undo(addedHistoriesMock);
            undoHistoryHandler.Apply();

            Assert.Equal("OldName", production.Name);
            Assert.Null(production.Aka);
        }

        [Fact]
        public void UndoReleaseDate()
        {
            var production = new Production { ReleaseDate = new DateTime(2000, 1, 1), ReleaseDateType = DateType.Year };

            var doHistoryHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");
            doHistoryHandler.AddHistory(HistoryEditProperty.ReleaseDate, new PartialDate { Date = new DateTime(2001, 2, 3), Type = DateType.YearMonthDay });
            doHistoryHandler.Apply();

            Assert.Equal(new DateTime(2001, 2, 3), production.ReleaseDate);
            Assert.Equal(DateType.YearMonthDay, production.ReleaseDateType);

            var undoHistoryHandler = HistoryHandlerFactory.Get(HistoryEntity.Production, unitOfWorkMock.Object, production, "1", "127.0.0.0");
            undoHistoryHandler.Undo(addedHistoriesMock);
            undoHistoryHandler.Apply();

            Assert.Equal(new DateTime(2000, 1, 1), production.ReleaseDate);
            Assert.Equal(DateType.Year, production.ReleaseDateType);
            Assert.Null(production.Aka);
        }
    }
}