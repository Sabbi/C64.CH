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

    public abstract class HistoryHandler<T> : IHistoryHandler
    {
        protected IUnitOfWork unitOfWork;
        protected T entity;
        protected string userId;
        protected string userIp;
        protected List<HistoryRecord> history = new List<HistoryRecord>();

        public abstract void AddHistory(HistoryEditProperty property, object newValue, HistoryStatus status = HistoryStatus.ToApply);

        public HistoryHandler(IUnitOfWork unitOfWork, T entity, string userId, string userIp)
        {
            this.unitOfWork = unitOfWork;
            this.entity = entity;
            this.userId = userId;
            this.userIp = userIp;
        }

        public void Apply()
        {
            var transactionId = Guid.NewGuid().ToString();

            foreach (var hist in history)
            {
                if (hist.Status != HistoryStatus.Applied)
                {
                    var applier = HistoryApplierFactory.Get(hist.Property);
                    applier.Apply(entity, hist);

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

    public class ProductionHistoryHandler : HistoryHandler<Production>
    {
        public ProductionHistoryHandler(IUnitOfWork unitOfWork, Production production, string userId, string userIp) : base(unitOfWork, production, userId, userIp)
        {
        }

        public override void AddHistory(HistoryEditProperty property, object newValue, HistoryStatus status = HistoryStatus.ToApply)
        {
            var applier = HistoryApplierFactory.Get(property);

            var dbhistory = applier.CreateHistory(property, HistoryEntity.Production, entity, newValue, status);

            if (dbhistory.NewValue == dbhistory.OldValue)
                return;

            dbhistory.AffectedProductionId = entity.ProductionId;
            dbhistory.AffectedGroupId = null;
            dbhistory.UserId = userId;
            dbhistory.IpAdress = userIp;

            history.Add(dbhistory);
        }
    }

    public class GroupHistoryHandler : HistoryHandler<Group>
    {
        public GroupHistoryHandler(IUnitOfWork unitOfWork, Group group, string userId, string userIp) : base(unitOfWork, group, userId, userIp)
        {
        }

        public override void AddHistory(HistoryEditProperty property, object newValue, HistoryStatus status = HistoryStatus.ToApply)
        {
            var applier = HistoryApplierFactory.Get(property);

            var dbhistory = applier.CreateHistory(property, HistoryEntity.Group, entity, newValue, status);

            if (dbhistory.NewValue == dbhistory.OldValue)
                return;

            dbhistory.AffectedProductionId = null;
            dbhistory.AffectedGroupId = entity.GroupId;
            dbhistory.UserId = userId;
            dbhistory.IpAdress = userIp;

            history.Add(dbhistory);
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
        Email,
        AddGroup,
        AddGroupMember,
        EditGroupMember,
    }
}