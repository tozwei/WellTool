using System;
using System.Text;

namespace WellTool.Core.Date
{
    public class BetweenFormatter
    {
        public enum Level
        {
            MILLISECOND,
            SECOND,
            MINUTE,
            HOUR,
            DAY,
            MONTH,
            YEAR
        }

        private const long MILLIS_PER_SECOND = 1000;
        private const long MILLIS_PER_MINUTE = 60 * MILLIS_PER_SECOND;
        private const long MILLIS_PER_HOUR = 60 * MILLIS_PER_MINUTE;
        private const long MILLIS_PER_DAY = 24 * MILLIS_PER_HOUR;
        private const long MILLIS_PER_MONTH = 30 * MILLIS_PER_DAY;
        private const long MILLIS_PER_YEAR = 365 * MILLIS_PER_DAY;

        public static string Format(long betweenMillis, DateUnit unit, Level formatLevel)
        {
            var sb = new StringBuilder();
            long remaining = betweenMillis;

            if (formatLevel >= Level.YEAR)
            {
                long years = remaining / MILLIS_PER_YEAR;
                if (years > 0)
                {
                    sb.Append($"{years}年");
                    remaining %= MILLIS_PER_YEAR;
                }
            }

            if (formatLevel >= Level.MONTH)
            {
                long months = remaining / MILLIS_PER_MONTH;
                if (months > 0)
                {
                    sb.Append($"{months}月");
                    remaining %= MILLIS_PER_MONTH;
                }
            }

            if (formatLevel >= Level.DAY)
            {
                long days = remaining / MILLIS_PER_DAY;
                if (days > 0)
                {
                    sb.Append($"{days}天");
                    remaining %= MILLIS_PER_DAY;
                }
            }

            if (formatLevel >= Level.HOUR)
            {
                long hours = remaining / MILLIS_PER_HOUR;
                if (hours > 0)
                {
                    sb.Append($"{hours}小时");
                    remaining %= MILLIS_PER_HOUR;
                }
            }

            if (formatLevel >= Level.MINUTE)
            {
                long minutes = remaining / MILLIS_PER_MINUTE;
                if (minutes > 0)
                {
                    sb.Append($"{minutes}分钟");
                    remaining %= MILLIS_PER_MINUTE;
                }
            }

            if (formatLevel >= Level.SECOND)
            {
                long seconds = remaining / MILLIS_PER_SECOND;
                if (seconds > 0)
                {
                    sb.Append($"{seconds}秒");
                    remaining %= MILLIS_PER_SECOND;
                }
            }

            if (formatLevel >= Level.MILLISECOND && remaining > 0)
            {
                sb.Append($"{remaining}毫秒");
            }

            return sb.ToString();
        }
    }
}