using System.Collections.Generic;
using System.Linq;

namespace C64.Data.Entities
{
    public class ProductionCredit
    {
        public int ProductionCreditId { get; set; }
        public int ProductionId { get; set; }
        public Production Production { get; set; }

        public int ScenerId { get; set; }
        public Scener Scener { get; set; }

        public Credit Credit { get; set; }

        public static (IEnumerable<ProductionCredit> Added, IEnumerable<ProductionCredit> Removed) CompareLists(IEnumerable<ProductionCredit> oldList, IEnumerable<ProductionCredit> newList)
        {
            var retValAdded = new List<ProductionCredit>();
            var retValRemoved = new List<ProductionCredit>();

            foreach (var entry in oldList)
            {
                var existsInNew = newList.Any(p => p.Equals(entry));
                if (!existsInNew)
                    retValRemoved.Add(new ProductionCredit { Credit = entry.Credit, ScenerId = entry.ScenerId, ProductionId = entry.ProductionId });
            }

            foreach (var entry in newList)
            {
                var existsInOld = oldList.Any(p => p.Equals(entry));

                if (!existsInOld)
                    retValAdded.Add(new ProductionCredit { Credit = entry.Credit, ScenerId = entry.ScenerId, ProductionId = entry.ProductionId });
            }

            return (retValAdded, retValRemoved);
        }
    }
}