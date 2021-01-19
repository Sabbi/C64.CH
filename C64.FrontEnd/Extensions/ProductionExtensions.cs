using C64.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace C64.FrontEnd.Extensions
{
    public static class ProductionExtensions
    {
        public static Dictionary<string, IEnumerable<ILinkJoinable>> GetCreatorLinks(this Production production)
        {
            var multiJoinables = new Dictionary<string, IEnumerable<ILinkJoinable>>();

            if (production.ProductionsGroups.Any())
                multiJoinables.Add("/groups/{0}/{1}", production.ProductionsGroups.Select(p => p.Group));

            if (production.ProductionsSceners.Any())
                multiJoinables.Add("/sceners/{0}/{1}", production.ProductionsSceners.Select(p => p.Scener));

            return multiJoinables;
        }
    }
}