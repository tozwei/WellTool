using System;

namespace WellTool.Core.Date
{
    public class DateBetween
    {
        private readonly DateTime start;
        private readonly DateTime end;

        public DateBetween(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
        }

        public long Between(DateUnit unit)
        {
            var duration = end - start;
            switch (unit)
            {
                case DateUnit.Year:
                    return (end.Year - start.Year);
                case DateUnit.Month:
                    return (end.Year - start.Year) * 12 + (end.Month - start.Month);
                case DateUnit.Day:
                    return (long)duration.TotalDays;
                case DateUnit.Hour:
                    return (long)duration.TotalHours;
                case DateUnit.Minute:
                    return (long)duration.TotalMinutes;
                case DateUnit.Second:
                    return (long)duration.TotalSeconds;
                case DateUnit.Millisecond:
                    return (long)duration.TotalMilliseconds;
                default:
                    return 0;
            }
        }

        public string ToString(DateUnit unit, BetweenFormatter.Level formatLevel)
        {
            var value = Between(unit);
            return $"{value} {unit.ToString()}";
        }
    }
}