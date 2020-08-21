using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace C64.Tests.History
{
    public class UndoTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private List<HistoryProduction> addedHistoriesMock = new List<HistoryProduction>();

        public UndoTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            var addedHistory = new HistoryProduction();
            unitOfWorkMock.Setup(p => p.Productions.AddHistory(It.IsAny<HistoryProduction>())).Callback<HistoryProduction>(p => addedHistoriesMock.Add(p));
        }

        [Fact]
        public void UndoNameandAka()
        {
            var production = new Production { Name = "OldName", Aka = null };

            var doHistoryHandler = new ProductionHistoryHandler(unitOfWorkMock.Object, production, "1", "127.0.0.0");
            doHistoryHandler.AddHistory(ProductionEditProperty.Name, "NewName");
            doHistoryHandler.AddHistory(ProductionEditProperty.Aka, "NewAka");
            doHistoryHandler.Apply();

            Assert.Equal("NewName", production.Name);

            var undoHistoryHandler = new ProductionHistoryHandler(unitOfWorkMock.Object, production, "1", "127.0.0.0");
            undoHistoryHandler.Undo(addedHistoriesMock);
            undoHistoryHandler.Apply();

            Assert.Equal("OldName", production.Name);
            Assert.Null(production.Aka);
        }

        [Fact]
        public void MultipleUndo()
        {
            var production = new Production { Name = "OldName" };

            var doHistoryHandler = new ProductionHistoryHandler(unitOfWorkMock.Object, production, "1", "127.0.0.0");
            doHistoryHandler.AddHistory(ProductionEditProperty.Name, "NewName1");
            doHistoryHandler.Apply();

            Assert.Equal("NewName1", production.Name);

            doHistoryHandler.AddHistory(ProductionEditProperty.Name, "NewName2");
            doHistoryHandler.Apply();

            Assert.Equal("NewName2", production.Name);

            var undoHistoryHandler = new ProductionHistoryHandler(unitOfWorkMock.Object, production, "1", "127.0.0.0");
            undoHistoryHandler.Undo(addedHistoriesMock);
            undoHistoryHandler.Apply();

            Assert.Equal("OldName", production.Name);
            Assert.Null(production.Aka);
        }

        [Fact]
        public void UndoReleaseDate()
        {
            var production = new Production { ReleaseDate = new DateTime(2000, 1, 1), ReleaseDateType = DateType.Year };

            var doHistoryHandler = new ProductionHistoryHandler(unitOfWorkMock.Object, production, "1", "127.0.0.0");
            doHistoryHandler.AddHistory(ProductionEditProperty.ReleaseDate, new PartialDateApplierData { Date = new DateTime(2001, 2, 3), Type = DateType.YearMonthDay });
            doHistoryHandler.Apply();

            Assert.Equal(new DateTime(2001, 2, 3), production.ReleaseDate);
            Assert.Equal(DateType.YearMonthDay, production.ReleaseDateType);

            var undoHistoryHandler = new ProductionHistoryHandler(unitOfWorkMock.Object, production, "1", "127.0.0.0");
            undoHistoryHandler.Undo(addedHistoriesMock);
            undoHistoryHandler.Apply();

            Assert.Equal(new DateTime(2000, 1, 1), production.ReleaseDate);
            Assert.Equal(DateType.Year, production.ReleaseDateType);
            Assert.Null(production.Aka);
        }
    }
}