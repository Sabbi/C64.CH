using C64.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public interface IHistoryHandler
    {
        void AddHistory(HistoryEditProperty property, object newValue, HistoryStatus status = HistoryStatus.ToApply);

        void Apply();

        void Undo(IEnumerable<HistoryRecord> historiesToUndo);
    }

    public class ProductionHistoryHandler : IHistoryHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Production production;
        private readonly string userId;
        private readonly string userIp;
        private readonly List<HistoryRecord> history = new List<HistoryRecord>();

        public ProductionHistoryHandler(IUnitOfWork unitOfWork, Production production, string userId, string userIp)
        {
            this.unitOfWork = unitOfWork;
            this.production = production;
            this.userId = userId;
            this.userIp = userIp;
        }

        public void AddHistory(HistoryEditProperty property, object newValue, HistoryStatus status = HistoryStatus.ToApply)
        {
            var applier = HistoryApplierFactory.Get(property);

            var dbhistory = applier.CreateHistory(property, HistoryEntity.Production, production, newValue, status);

            if (dbhistory.NewValue == dbhistory.OldValue)
                return;

            dbhistory.AffectedProductionId = production.ProductionId;
            dbhistory.AffectedGroupId = null;
            dbhistory.UserId = userId;
            dbhistory.IpAdress = userIp;

            history.Add(dbhistory);
        }

        public void Apply()
        {
            var transactionId = Guid.NewGuid().ToString();

            foreach (var hist in history)
            {
                if (hist.Status != HistoryStatus.Applied)
                {
                    var applier = HistoryApplierFactory.Get(hist.Property);
                    applier.Apply(production, hist);

                    hist.Status = HistoryStatus.Applied;
                    hist.Applied = DateTime.Now;
                    hist.TransactionId = transactionId;
                    unitOfWork.Productions.AddHistory(hist);
                }
            };
        }

        public void Undo(IEnumerable<HistoryRecord> historiesToUndo)
        {
            var transactionId = Guid.NewGuid().ToString();
            foreach (var toUndo in historiesToUndo.OrderByDescending(p => p.Applied))
            {
                history.Add(new HistoryRecord
                {
                    AffectedEntity = toUndo.AffectedEntity,
                    AffectedProductionId = toUndo.AffectedProductionId,
                    AffectedGroupId = toUndo.AffectedGroupId,
                    AffectedScenerId = toUndo.AffectedScenerId,
                    Applied = DateTime.Now,
                    NewValue = toUndo.OldValue,
                    OldValue = toUndo.NewValue,
                    Property = toUndo.Property,
                    Type = toUndo.Type,
                    TransactionId = transactionId,
                    UserId = userId,
                    IpAdress = userIp,
                });
                toUndo.Undid = DateTime.Now;
                toUndo.Status = HistoryStatus.Undid;
            }
        }
    }

    public class GroupHistoryHandler : IHistoryHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Group group;
        private readonly string userId;
        private readonly string userIp;
        private readonly List<HistoryRecord> history = new List<HistoryRecord>();

        public GroupHistoryHandler(IUnitOfWork unitOfWork, Group group, string userId, string userIp)
        {
            this.unitOfWork = unitOfWork;
            this.group = group;
            this.userId = userId;
            this.userIp = userIp;
        }

        public void AddHistory(HistoryEditProperty property, object newValue, HistoryStatus status = HistoryStatus.ToApply)
        {
            var applier = HistoryApplierFactory.Get(property);

            var dbhistory = applier.CreateHistory(property, HistoryEntity.Group, group, newValue, status);

            if (dbhistory.NewValue == dbhistory.OldValue)
                return;

            dbhistory.AffectedProductionId = null;
            dbhistory.AffectedGroupId = group.GroupId;
            dbhistory.UserId = userId;
            dbhistory.IpAdress = userIp;

            history.Add(dbhistory);
        }

        public void Apply()
        {
            var transactionId = Guid.NewGuid().ToString();

            foreach (var hist in history)
            {
                if (hist.Status != HistoryStatus.Applied)
                {
                    var applier = HistoryApplierFactory.Get(hist.Property);
                    applier.Apply(group, hist);

                    hist.Status = HistoryStatus.Applied;
                    hist.Applied = DateTime.Now;
                    hist.TransactionId = transactionId;
                    unitOfWork.Productions.AddHistory(hist);
                }
            };
        }

        public void Undo(IEnumerable<HistoryRecord> historiesToUndo)
        {
            var transactionId = Guid.NewGuid().ToString();
            foreach (var toUndo in historiesToUndo.OrderByDescending(p => p.Applied))
            {
                history.Add(new HistoryRecord
                {
                    AffectedEntity = toUndo.AffectedEntity,
                    AffectedProductionId = toUndo.AffectedProductionId,
                    AffectedGroupId = toUndo.AffectedGroupId,
                    AffectedScenerId = toUndo.AffectedScenerId,
                    Applied = DateTime.Now,
                    NewValue = toUndo.OldValue,
                    OldValue = toUndo.NewValue,
                    Property = toUndo.Property,
                    Type = toUndo.Type,
                    TransactionId = transactionId,
                    UserId = userId,
                    IpAdress = userIp,
                });
                toUndo.Undid = DateTime.Now;
                toUndo.Status = HistoryStatus.Undid;
            }
        }
    }

    public enum HistoryEditProperty
    {
        Name,
        Aka,
        ReleaseDate,
        Platform,
        SubCategory,
        Party,
        Groups,
        Remarks,
        HiddenParts,
        VideoType,
        ProductionVideos,
        ProductionPictures,
        ProductionFiles,
        AddProduction,
        ProductionCredits,

        // Groups,

        FoundedDate,
        ClosedDate,
        Url,
        Email
    }
}