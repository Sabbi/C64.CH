using C64.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace C64.Data.History
{
    public class ProductionHistoryHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Production production;
        private readonly string userId;
        private List<HistoryProduction> history = new List<HistoryProduction>();

        public ProductionHistoryHandler(IUnitOfWork unitOfWork, Production production, string userId)
        {
            this.unitOfWork = unitOfWork;
            this.production = production;
            this.userId = userId;
        }

        public void AddHistory(ProductionEditProperty property, object newValue, HistoryStatus status)
        {
            var applier = HistoryApplierFactory.Get(property);

            var dbhistory = applier.CreateHistoryProduction(property, production, newValue, status);
            if (dbhistory.NewValue == dbhistory.OldValue)
                return;

            dbhistory.UserId = userId;

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
            if (hist.Status == HistoryStatus.Applied)
            {
                var type = Type.GetType(hist.Type, true);
                var des = JsonConvert.DeserializeObject(hist.OldValue, type);
                var property = typeof(Production).GetProperty(hist.Property);

                property.SetValue(production, des);
                hist.Undid = DateTime.Now;
                hist.Status = HistoryStatus.Undid;
            }
        }
    }

    //public class HistoryDefinition
    //{
    //    public string Type { get; set; }
    //    public string Name { get; set; }
    //}

    //public static class HistoryDefinitions
    //{
    //    private static List<HistoryDefinition> definitions = new List<HistoryDefinition>()
    //    {
    //         new HistoryDefinition{ Name ="Name", Type="System.string"},
    //         new HistoryDefinition{ Name ="Aka", Type="System.string"},
    //         new HistoryDefinition{ Name ="ReleaseDate", Type="System.string"},
    //         new HistoryDefinition{ Name ="ReleaseDateType", Type="System.string"},
    //    };

    //    public static HistoryDefinition Get(string name)
    //    {
    //        return definitions.First(p => p.Name == name);
    //    }
    //}

    public enum ProductionEditProperty
    {
        Name,
        Aka,
        ReleaseDate,
        ReleaseDateType,
        SubCategory,
        Party,
        Groups
    }
}