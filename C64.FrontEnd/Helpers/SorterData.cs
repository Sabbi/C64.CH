using System.Collections.Generic;

namespace C64.FrontEnd.Helpers
{
    public class SorterData
    {
        public string CurrentSortColumn { get; set; }
        public bool IsSortedAscending { get; set; }
        public IList<SorterItem> SorterItems { get; set; } = new List<SorterItem>();
    }
}