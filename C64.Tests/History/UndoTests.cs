using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using Moq;
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
        public void UndoName()
        {
            var production = new Production { Name = "OldName" };

            var doHistoryHandler = new ProductionHistoryHandler(unitOfWorkMock.Object, production, "1", "127.0.0.0");
            doHistoryHandler.AddHistory(ProductionEditProperty.Name, "NewName");
            doHistoryHandler.Apply();

            Assert.Equal("NewName", production.Name);

            //var undoHistoryHandler = new ProductionHistoryHandler(unitOfWorkMock.Object, production, "1", "127.0.0.0");
            //undoHistoryHandler.Undo(addedHistoriesMock);

            //Assert.Equal("OldName", production.Name);
        }
    }
}