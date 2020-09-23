using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace C64.Tests.History
{
    public class BasicHistoryTestsSceners
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private List<HistoryRecord> addedHistoriesMock = new List<HistoryRecord>();

        public BasicHistoryTestsSceners()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(p => p.Productions.AddHistory(It.IsAny<HistoryRecord>())).Callback<HistoryRecord>(p => addedHistoriesMock.Add(p));
        }

        [Fact]
        public void AddScener()
        {
            var scener = new Scener { Handle = "Handle", RealName = "Realname" };

            var historyHandler = HistoryHandlerFactory.Get(HistoryEntity.Scener, unitOfWorkMock.Object, scener, "1", "127.0.0.0");

            historyHandler.AddHistory(HistoryEditProperty.AddScener, scener);
            historyHandler.Apply();

            Assert.Equal(HistoryEntity.Scener, addedHistoriesMock.FirstOrDefault().AffectedEntity);
        }
    }
}