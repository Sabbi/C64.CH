using C64.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace C64.Data.Entities
{
    public class ScenersGroups
    {
        public int ScenersGroupsId { get; set; }
        public int ScenerId { get; set; }
        public virtual Scener Scener { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public DateTime ValidFrom { get; set; } = DateTime.MinValue;
        public DateType ValidFromType { get; set; }
        public DateTime ValidTo { get; set; } = DateTime.MaxValue;
        public DateType ValidToType { get; set; }

        [NotMapped]
        public bool Currently
        {
            get
            {
                return ValidFrom <= DateTime.Now && ValidTo >= DateTime.Now;
            }
        }

      

        public virtual ICollection<ScenerGroupJob> ScenerGroupJobs { get; set; } = new HashSet<ScenerGroupJob>();

        public bool MemberAtTheTime(PartialDate released, DateTime added)
        {
            var (joinFrom, _) = GetRangeFromPartial(new PartialDate(ValidFrom, ValidFromType));
            var (_, leftTo) = GetRangeFromPartial(new PartialDate(ValidTo, ValidToType));
            var (releasedFrom, releasedTo) = GetRangeFromPartial(released);

            if (released != null && released.Type == DateType.None)
            {
                var realAdded = added;
                if (added == DateTime.MinValue)
                    added = new DateTime(1999, 1, 1);
                releasedFrom = added;
                releasedTo = added;
            }

            return joinFrom <= releasedTo && releasedFrom <= leftTo;
        }

        private (DateTime from, DateTime to) GetRangeFromPartial(PartialDate partial)
        {
            if (partial == null)
            {
                // If no date is provided, assume an open-ended range.
                return (DateTime.MinValue, DateTime.MaxValue);
            }

            switch (partial.Type)
            {
                case DateType.YearMonthDay:
                    // Exact day
                    return (partial.Date, partial.Date);

                case DateType.YearMonth:
                    // Month range: from start of the month to the end of the month
                    DateTime startOfMonth = new DateTime(partial.Date.Year, partial.Date.Month, 1);
                    DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Last day of the month
                    return (startOfMonth, endOfMonth);

                case DateType.Year:
                    // Year range: from start of the year to the end of the year
                    DateTime startOfYear = new DateTime(partial.Date.Year, 1, 1);
                    DateTime endOfYear = new DateTime(partial.Date.Year, 12, 31);
                    return (startOfYear, endOfYear);

                default:
                    // If the type is unrecognized, return the entire range
                    return (DateTime.MinValue, DateTime.MaxValue);
            }
        }

    }
}