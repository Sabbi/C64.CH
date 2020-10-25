using C64.Data.Entities;
using System;

namespace C64.Data.History
{
    public class HistoryHandlerFactory
    {
        public static IHistoryHandler Get(HistoryEntity historyentity, IUnitOfWork unitOfWork, object entity, string userId, string userIp)
        {
            return historyentity switch
            {
                HistoryEntity.Production => new ProductionHistoryHandler(unitOfWork, (Production)entity, userId, userIp),
                HistoryEntity.Group => new GroupHistoryHandler(unitOfWork, (Group)entity, userId, userIp),
                HistoryEntity.Party => new PartyHistoryHandler(unitOfWork, (Party)entity, userId, userIp),
                HistoryEntity.Scener => new ScenerHistoryHandler(unitOfWork, (Scener)entity, userId, userIp),
                _ => throw new NotImplementedException(),
            };
        }
    }
}