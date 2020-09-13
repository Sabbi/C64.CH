using System;

namespace C64.Data.Models
{
    public class PartialDate
    {
        public PartialDate()
        {
            Date = DateTime.MinValue;
            Type = DateType.None;
            IsValidDate = true;
        }

        public PartialDate(DateTime date, DateType type)
        {
            Date = date;
            Type = type;
        }

        public bool IsValidDate { get; set; } = true;
        public DateTime Date { get; set; } = DateTime.MinValue;
        public DateType Type { get; set; } = DateType.None;
    }
}