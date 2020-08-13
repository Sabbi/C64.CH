using C64.Data.Entities;
using System;
using System.Collections.Generic;

namespace C64.Data.History
{
    public class ProductionHistoryHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Production production;
        private readonly string userId;
        private readonly string userIp;
        private readonly List<HistoryProduction> history = new List<HistoryProduction>();

      public ProductionHistoryHandler(IUnitOfWork unitOfWork, Production production, string userId, string userIp)
        {
            this.unitOfWork = unitOfWork;
            this.production = production;
            this.userId = userId;
            this.userIp = userIp;
        }

        public void AddHistory(ProductionEditProperty property, object newValue, HistoryStatus status = HistoryStatus.ToApply)
        {
            var applier = HistoryApplierFactory.Get(property);

            var dbhistory = applier.CreateHistoryProduction(property, production, newValue, status);

            if (dbhistory.NewValue == dbhistory.OldValue)
                return;

            dbhistory.UserId = userId;
            dbhistory.IpAdress = userIp;

            history.Add(dbhistory);
        }

        public void Apply()
        {
            foreach (var hist in history)
            {
                if (hist.Status != HistoryStatus.Applied)
                {
                    var applier = HistoryApplierFactory.Get(hist.Property);
                    applier.Apply(production, hist);

                    hist.Status = HistoryStatus.Applied;
                    hist.Applied = DateTime.Now;

                    unitOfWork.Productions.AddHistory(hist);
                }
            };
        }

        public void Undo(HistoryProduction hist)
        {
            throw new NotImplementedException();
        }
    }

    public enum ProductionEditProperty
    {
        Name,
        Aka,
        ReleaseDate,
        ReleaseDateType,
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
        ProductionCredits
    }
}