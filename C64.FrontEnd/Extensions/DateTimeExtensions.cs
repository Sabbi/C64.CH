using C64.Data;
using System;

namespace C64.FrontEnd.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ParseDate(this DateTime date, DateType dateType)
        {
            switch (dateType)
            {
                case DateType.None:
                    return "n/a";

                case DateType.Year:
                    return date.Year.ToString();

                case DateType.YearMonth:
                    return date.ToString("MMMM yyyy");

                case DateType.YearMonthDay:
                    return date.ToString($"MMMM {DayAddEnding(date.Day)}, yyyy");

                default:
                    throw new ArgumentException("Invalid ReleaseDateType");
            }
        }

        public static string ParseDate(this DateTime date)
        {
            return ParseDate(date, DateType.YearMonthDay);
        }

        private static string DayAddEnding(int day)
        {
            switch (day)
            {
                case 1:
                case 11:
                case 21:
                case 31:
                    return $"'{day}st'";

                case 2:
                case 22:
                    return $"'{day}nd'";

                case 3:
                case 23:
                    return $"'{day}rd'";
            }
            return $"'{day}th'";
        }
    }
}