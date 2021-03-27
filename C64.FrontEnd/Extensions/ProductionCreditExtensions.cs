using C64.Data;
using C64.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace C64.FrontEnd.Extensions
{
    public static class ProductionCreditExtensions
    {
        public static ICollection<ProductionCredit> GetSorted(this ICollection<ProductionCredit> credits)
        {
            var retVal = credits.Where(p => p.Credit != Credit.Unspecified).OrderBy(p => p.Credit).ToList();
            retVal.AddRange(credits.Where(p => p.Credit == Credit.Unspecified));
            return retVal;
        }
    }
}