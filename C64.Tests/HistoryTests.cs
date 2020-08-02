//using C64.Data.Entities;
//using C64.Data.History;
//using Xunit;

//namespace C64.Tests
//{
//    public class HistoryTests
//    {
//        private Production productionUnderTest;

//        public HistoryTests()
//        {
//            productionUnderTest = new Production
//            {
//                ProductionId = 1,
//                Name = "TestProduction",
//                Aka = "TestProductionAka"
//            };
//        }

//        [Fact]
//        public void ChangeProductionName()
//        {
//            var historyHandler = new ProductionHistoryHandler(productionUnderTest);

//            historyHandler.AddHistory(HistoryDefinitions.Get("Name"), "NewName");
//            historyHandler.AddHistory(HistoryDefinitions.Get("Aka"), "NewAka");
//            historyHandler.Apply();

//            Assert.Equal("NewAka", productionUnderTest.Aka);
//            Assert.Equal("NewName", productionUnderTest.Name);
//        }
//    }
//}