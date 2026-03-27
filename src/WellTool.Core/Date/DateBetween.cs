using System;

namespace WellTool.Core.Date
{
    public class DateBetween
    {
        private readonly long betweenMillis;
        private readonly System.DateTime start;
        private readonly System.DateTime end;

        public DateBetween(System.DateTime start, System.DateTime end)
        {
            this.start = start;
            this.end = end;
            this.betweenMillis = Math.Abs(end.Ticks - start.Ticks) / TimeSpan.TicksPerMillisecond;
        }

        public long Between(DateUnit unit)
        {
            return unit switch
            {
                DateUnit.Year => betweenMillis / DateUnit.Year.Millis,
                DateUnit.Month => betweenMillis / DateUnit.Month.Millis,
                DateUnit.Day => betweenMillis / DateUnit.Day.Millis,
                DateUnit.Hour => betweenMillis / DateUnit.Hour.Millis,
                DateUnit.Minute => betweenMillis / DateUnit.Minute.Millis,
                DateUnit.Second => betweenMillis / DateUnit.Second.Millis,
                DateUnit.Millisecond => betweenMillis,
                _ => throw new ArgumentException("Unsupported date unit"),
            };
        }

        public string ToString(DateUnit unit, BetweenFormatter.Level formatLevel)
        {
            return BetweenFormatter.Format(betweenMillis, unit, formatLevel);
        }

        public override string ToString()
        {
            return ToString(DateUnit.Millisecond, BetweenFormatter.Level.SECOND);
        }

        public long Ticks => betweenMillis * TimeSpan.TicksPerMillisecond;

        public System.DateTime Start => start;

        public System.DateTime End => end;
    }
}