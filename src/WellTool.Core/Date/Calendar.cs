using System;
using System.Globalization;

namespace WellTool.Core.Date
{
    public class Calendar
    {
        public enum CalendarField
        {
            Era = 0,
            Year = 1,
            Month = 2,
            WeekOfYear = 3,
            WeekOfMonth = 4,
            Date = 5,
            DayOfMonth = 5,
            DayOfYear = 6,
            DayOfWeek = 7,
            DayOfWeekInMonth = 8,
            AM_PM = 9,
            Hour = 10,
            HourOfDay = 11,
            Minute = 12,
            Second = 13,
            Millisecond = 14,
            ZoneOffset = 15,
            DSTOffset = 16,
            FieldCount = 17
        }

        private readonly System.Globalization.Calendar calendar;
        private readonly TimeZoneInfo timeZone;
        private readonly CultureInfo culture;
        private DateTime time;

        public Calendar(TimeZoneInfo timeZone, CultureInfo culture)
        {
            this.timeZone = timeZone;
            this.culture = culture;
            this.calendar = culture.Calendar;
        }

        public long Ticks
        {
            get { return time.Ticks; }
        }

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        public void SetFirstDayOfWeek(int firstDayOfWeek)
        {
            // 在.NET中，Calendar的FirstDayOfWeek是只读的，需要通过CultureInfo来设置
        }

        public void SetMinimalDaysInFirstWeek(int minimalDaysInFirstWeek)
        {
            // 在.NET中，Calendar的MinimalDaysInFirstWeek是只读的，需要通过CultureInfo来设置
        }

        public void Add(int field, int amount)
        {
            var calendarField = (CalendarField)field;
            if (calendarField == CalendarField.Date || calendarField == CalendarField.DayOfMonth)
            {
                time = time.AddDays(amount);
            }
            else
            {
                switch (calendarField)
                {
                    case CalendarField.Year:
                        time = time.AddYears(amount);
                        break;
                    case CalendarField.Month:
                        time = time.AddMonths(amount);
                        break;
                    case CalendarField.Hour:
                        time = time.AddHours(amount);
                        break;
                    case CalendarField.Minute:
                        time = time.AddMinutes(amount);
                        break;
                    case CalendarField.Second:
                        time = time.AddSeconds(amount);
                        break;
                    case CalendarField.Millisecond:
                        time = time.AddMilliseconds(amount);
                        break;
                    case CalendarField.WeekOfYear:
                        time = time.AddDays(amount * 7);
                        break;
                    case CalendarField.WeekOfMonth:
                        time = time.AddDays(amount * 7);
                        break;
                    case CalendarField.DayOfYear:
                        time = time.AddDays(amount);
                        break;
                    case CalendarField.DayOfWeek:
                        time = time.AddDays(amount);
                        break;
                    case CalendarField.DayOfWeekInMonth:
                        time = time.AddDays(amount * 7);
                        break;
                    case CalendarField.AM_PM:
                        time = time.AddHours(amount * 12);
                        break;
                    case CalendarField.HourOfDay:
                        time = time.AddHours(amount);
                        break;
                }
            }
        }

        public void Set(int field, int value)
        {
            var calendarField = (CalendarField)field;
            if (calendarField == CalendarField.Date || calendarField == CalendarField.DayOfMonth)
            {
                time = new DateTime(time.Year, time.Month, value, time.Hour, time.Minute, time.Second, time.Millisecond);
            }
            else
            {
                switch (calendarField)
                {
                    case CalendarField.Year:
                        time = new DateTime(value, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
                        break;
                    case CalendarField.Month:
                        time = new DateTime(time.Year, value, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
                        break;
                    case CalendarField.Hour:
                        time = new DateTime(time.Year, time.Month, time.Day, value, time.Minute, time.Second, time.Millisecond);
                        break;
                    case CalendarField.Minute:
                        time = new DateTime(time.Year, time.Month, time.Day, time.Hour, value, time.Second, time.Millisecond);
                        break;
                    case CalendarField.Second:
                        time = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, value, time.Millisecond);
                        break;
                    case CalendarField.Millisecond:
                        time = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, value);
                        break;
                }
            }
        }

        public int Get(int field)
        {
            var calendarField = (CalendarField)field;
            if (calendarField == CalendarField.Date || calendarField == CalendarField.DayOfMonth)
            {
                return time.Day;
            }
            else
            {
                switch (calendarField)
                {
                    case CalendarField.Year:
                        return time.Year;
                    case CalendarField.Month:
                        return time.Month - 1; // .NET的月份从1开始，Java从0开始
                    case CalendarField.Hour:
                        return time.Hour % 12;
                    case CalendarField.Minute:
                        return time.Minute;
                    case CalendarField.Second:
                        return time.Second;
                    case CalendarField.Millisecond:
                        return time.Millisecond;
                    case CalendarField.WeekOfYear:
                        var calendar = new GregorianCalendar();
                        return calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    case CalendarField.WeekOfMonth:
                        var weekOfMonth = (time.Day - 1) / 7 + 1;
                        return weekOfMonth;
                    case CalendarField.DayOfYear:
                        return time.DayOfYear;
                    case CalendarField.DayOfWeek:
                        return (int)time.DayOfWeek; // .NET的DayOfWeek从0开始（周日），Java从1开始（周日）
                    case CalendarField.DayOfWeekInMonth:
                        return (time.Day - 1) / 7 + 1;
                    case CalendarField.AM_PM:
                        return time.Hour < 12 ? 0 : 1;
                    case CalendarField.HourOfDay:
                        return time.Hour;
                    default:
                        return 0;
                }
            }
        }
    }
}