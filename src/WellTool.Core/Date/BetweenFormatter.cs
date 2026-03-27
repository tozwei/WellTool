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

        public static string Format(long betweenMillis, DateUnit unit, Level formatLevel)
        {
            var sb = new StringBuilder();
            long remaining = betweenMillis;

            if (formatLevel >= Level.YEAR)
            {
                long years = remaining / DateUnit.Year.Millis;
                if (years > 0)
                {
                    sb.Append($"{years}年");
                    remaining %= DateUnit.Year.Millis;
                }
            }

            if (formatLevel >= Level.MONTH)
            {
                long months = remaining / DateUnit.Month.Millis;
                if (months > 0)
                {
                    sb.Append($"{months}月");
                    remaining %= DateUnit.Month.Millis;
                }
            }

            if (formatLevel >= Level.DAY)
            {
                long days = remaining / DateUnit.Day.Millis;
                if (days > 0)
                {
                    sb.Append($"{days}天");
                    remaining %= DateUnit.Day.Millis;
                }
            }

            if (formatLevel >= Level.HOUR)
            {
                long hours = remaining / DateUnit.Hour.Millis;
                if (hours > 0)
                {
                    sb.Append($"{hours}小时");
                    remaining %= DateUnit.Hour.Millis;
                }
            }

            if (formatLevel >= Level.MINUTE)
            {
                long minutes = remaining / DateUnit.Minute.Millis;
                if (minutes > 0)
                {
                    sb.Append($"{minutes}分钟");
                    remaining %= DateUnit.Minute.Millis;
                }
            }

            if (formatLevel >= Level.SECOND)
            {
                long seconds = remaining / DateUnit.Second.Millis;
                if (seconds > 0)
                {
                    sb.Append($"{seconds}秒");
                    remaining %= DateUnit.Second.Millis;
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