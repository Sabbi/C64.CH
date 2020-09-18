using C64.Data.Entities;
using System;

namespace C64.Data.History
{
    public class HistoryHandlerFactory
    {
        public static IHistoryHandler Get(HistoryEntity historyentity, IUnitOfWork unitOfWork, object entity, string userId, string userIp)
        {
            switch (historyentity)
            {
                case HistoryEntity.Production:
                    return new ProductionHistoryHandler(unitOfWork, (Production)entity, userId, userIp);

                case HistoryEntity.Group:
                    return new GroupHistoryHandler(unitOfWork, (Group)entity, userId, userIp);

                case HistoryEntity.Party:
                    return new PartyHistoryHandler(unitOfWork, (Party)entity, userId, userIp);

                case HistoryEntity.Scener:
                    throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }
    }
}