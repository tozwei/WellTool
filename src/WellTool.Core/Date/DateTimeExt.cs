using System;
using System.Globalization;

namespace WellTool.Core.Date
{
    public class DateTimeExt
    {
        private static bool useJdkToStringStyle = false;

        public static void SetUseJdkToStringStyle(bool customUseJdkToStringStyle)
        {
            useJdkToStringStyle = customUseJdkToStringStyle;
        }

        private bool mutable = true;
        private Week firstDayOfWeek = Week.Monday;
        private TimeZoneInfo timeZone;
        private int minimalDaysInFirstWeek;
        private System.DateTime innerDateTime;

        public static DateTimeExt Of(long timeMillis)
        {
            return new DateTimeExt(timeMillis);
        }

        public static DateTimeExt Of(System.DateTime date)
        {
            if (date is DateTimeExt)
            {
                return (DateTimeExt)date;
            }
            return new DateTimeExt(date);
        }

        public static DateTimeExt Now()
        {
            return new DateTimeExt();
        }

        public DateTimeExt() : this(TimeZoneInfo.Local)
        {
        }

        public DateTimeExt(TimeZoneInfo timeZone)
        {
            this.innerDateTime = System.DateTime.Now;
            this.timeZone = timeZone ?? TimeZoneInfo.Local;
        }

        public DateTimeExt(System.DateTime date)
        {
            this.innerDateTime = date;
            this.timeZone = TimeZoneInfo.Local;
            if (date is DateTimeExt)
            {
                this.timeZone = ((DateTimeExt)date).timeZone;
            }
        }

        public DateTimeExt(System.DateTime date, TimeZoneInfo timeZone)
        {
            this.innerDateTime = date;
            this.timeZone = timeZone ?? TimeZoneInfo.Local;
        }

        public DateTimeExt(long timeMillis)
        {
            this.innerDateTime = new System.DateTime(timeMillis);
            this.timeZone = TimeZoneInfo.Local;
        }

        public DateTimeExt(long timeMillis, TimeZoneInfo timeZone)
        {
            this.innerDateTime = new System.DateTime(timeMillis);
            this.timeZone = timeZone ?? TimeZoneInfo.Local;
        }

        public DateTimeExt(string dateStr)
        {
            this.innerDateTime = DateUtil.Parse(dateStr);
            this.timeZone = TimeZoneInfo.Local;
        }

        public DateTimeExt(string dateStr, string format)
        {
            this.innerDateTime = DateUtil.Parse(dateStr, format);
            this.timeZone = TimeZoneInfo.Local;
        }

        public DateTimeExt Offset(DateField datePart, int offset)
        {
            var cal = ToCalendar();
            cal.Add((int)datePart, offset);

            DateTimeExt dt = mutable ? this : new DateTimeExt(this.innerDateTime);
            dt.SetTimeInternal(cal.Ticks);
            return dt;
        }

        public DateTimeExt OffsetNew(DateField datePart, int offset)
        {
            var cal = ToCalendar();
            cal.Add((int)datePart, offset);

            var dt = new DateTimeExt(this.innerDateTime);
            dt.SetTimeInternal(cal.Ticks);
            return dt;
        }

        public int GetField(DateField field)
        {
            return GetField((int)field);
        }

        public int GetField(int field)
        {
            return ToCalendar().Get(field);
        }

        public DateTimeExt SetField(DateField field, int value)
        {
            return SetField((int)field, value);
        }

        public DateTimeExt SetField(int field, int value)
        {
            var calendar = ToCalendar();
            calendar.Set(field, value);

            DateTimeExt dt = this;
            if (!mutable)
            {
                dt = new DateTimeExt(this.innerDateTime);
            }
            dt.SetTimeInternal(calendar.Ticks);
            return dt;
        }

        public void SetTime(long time)
        {
            if (mutable)
            {
                innerDateTime = new System.DateTime(time);
            }
            else
            {
                throw new DateException("This is not a mutable object!");
            }
        }

        public int Year()
        {
            return GetField(DateField.Year);
        }

        public int Quarter()
        {
            return Month() / 3 + 1;
        }

        public Quarter QuarterEnum()
        {
            return QuarterExtensions.Of(Quarter()).Value;
        }

        public int Month()
        {
            return GetField(DateField.Month);
        }

        public int MonthBaseOne()
        {
            return Month() + 1;
        }

        public int MonthStartFromOne()
        {
            return Month() + 1;
        }

        public Month MonthEnum()
        {
            return MonthExtensions.Of(Month()).Value;
        }

        public int WeekOfYear()
        {
            return GetField(DateField.WeekOfYear);
        }

        public int WeekOfMonth()
        {
            return GetField(DateField.WeekOfMonth);
        }

        public int DayOfMonth()
        {
            return GetField(DateField.DayOfMonth);
        }

        public int DayOfYear()
        {
            return GetField(DateField.DayOfYear);
        }

        public int DayOfWeek()
        {
            return GetField(DateField.DayOfWeek);
        }

        public int DayOfWeekInMonth()
        {
            return GetField(DateField.DayOfWeekInMonth);
        }

        public Week DayOfWeekEnum()
        {
            return (Week)DayOfWeek();
        }

        public int Hour(bool is24HourClock)
        {
            return GetField(is24HourClock ? DateField.HourOfDay : DateField.Hour);
        }

        public int Minute()
        {
            return GetField(DateField.Minute);
        }

        public int Second()
        {
            return GetField(DateField.Second);
        }

        public int Millisecond()
        {
            return GetField(DateField.Millisecond);
        }

        public bool IsAM()
        {
            return GetField(DateField.AmPm) == 0; // 0 for AM
        }

        public bool IsPM()
        {
            return GetField(DateField.AmPm) == 1; // 1 for PM
        }

        public bool IsWeekend()
        {
            var dayOfWeek = DayOfWeek();
            return dayOfWeek == 1 || dayOfWeek == 7; // 1 for Sunday, 7 for Saturday
        }

        public bool IsLeapYear()
        {
            return DateUtil.IsLeapYear(Year());
        }

        public Calendar ToCalendar()
        {
            return ToCalendar(CultureInfo.CurrentCulture);
        }

        public Calendar ToCalendar(CultureInfo culture)
        {
            return ToCalendar(this.timeZone, culture);
        }

        public Calendar ToCalendar(TimeZoneInfo zone)
        {
            return ToCalendar(zone, CultureInfo.CurrentCulture);
        }

        public Calendar ToCalendar(TimeZoneInfo zone, CultureInfo culture)
        {
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            var cal = new Calendar(zone, culture);
            cal.SetFirstDayOfWeek((int)firstDayOfWeek);
            if (minimalDaysInFirstWeek > 0)
            {
                cal.SetMinimalDaysInFirstWeek(minimalDaysInFirstWeek);
            }
            cal.Time = this.innerDateTime;
            return cal;
        }

        public System.DateTime ToJdkDate()
        {
            return innerDateTime;
        }

        public DateBetween Between(System.DateTime date)
        {
            return new DateBetween(this.innerDateTime, date);
        }

        public long Between(System.DateTime date, DateUnit unit)
        {
            return new DateBetween(this.innerDateTime, date).Between(unit);
        }

        public string Between(System.DateTime date, DateUnit unit, BetweenFormatter.Level formatLevel)
        {
            return new DateBetween(this.innerDateTime, date).ToString(unit, formatLevel);
        }

        public bool IsIn(System.DateTime beginDate, System.DateTime endDate)
        {
            long beginMills = beginDate.Ticks;
            long endMills = endDate.Ticks;
            long thisMills = this.innerDateTime.Ticks;

            return thisMills >= System.Math.Min(beginMills, endMills) && thisMills <= System.Math.Max(beginMills, endMills);
        }

        public bool IsBefore(System.DateTime date)
        {
            return this.innerDateTime.CompareTo(date) < 0;
        }

        public bool IsBeforeOrEquals(System.DateTime date)
        {
            return this.innerDateTime.CompareTo(date) <= 0;
        }

        public bool IsAfter(System.DateTime date)
        {
            return this.innerDateTime.CompareTo(date) > 0;
        }

        public bool IsAfterOrEquals(System.DateTime date)
        {
            return this.innerDateTime.CompareTo(date) >= 0;
        }

        public bool IsMutable()
        {
            return mutable;
        }

        public DateTimeExt SetMutable(bool mutable)
        {
            this.mutable = mutable;
            return this;
        }

        public Week GetFirstDayOfWeek()
        {
            return firstDayOfWeek;
        }

        public DateTimeExt SetFirstDayOfWeek(Week firstDayOfWeek)
        {
            this.firstDayOfWeek = firstDayOfWeek;
            return this;
        }

        public TimeZoneInfo GetTimeZone()
        {
            return this.timeZone;
        }

        public DateTimeExt SetTimeZone(TimeZoneInfo timeZone)
        {
            this.timeZone = timeZone ?? TimeZoneInfo.Local;
            return this;
        }

        public DateTimeExt SetMinimalDaysInFirstWeek(int minimalDaysInFirstWeek)
        {
            this.minimalDaysInFirstWeek = minimalDaysInFirstWeek;
            return this;
        }

        public bool IsLastDayOfMonth()
        {
            return DayOfMonth() == GetLastDayOfMonth();
        }

        public int GetLastDayOfMonth()
        {
            return MonthEnum().GetLastDay(IsLeapYear());
        }

        public override string ToString()
        {
            if (useJdkToStringStyle)
            {
                return innerDateTime.ToString();
            }
            return ToString(this.timeZone);
        }

        public string ToStringDefaultTimeZone()
        {
            return ToString(TimeZoneInfo.Local);
        }

        public string ToString(TimeZoneInfo timeZone)
        {
            if (timeZone != null)
            {
                var dateTimeInZone = TimeZoneInfo.ConvertTime(this.innerDateTime, timeZone);
                return dateTimeInZone.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return innerDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string ToDateStr()
        {
            if (this.timeZone != null)
            {
                var dateTimeInZone = TimeZoneInfo.ConvertTime(this.innerDateTime, timeZone);
                return dateTimeInZone.ToString("yyyy-MM-dd");
            }
            return innerDateTime.ToString("yyyy-MM-dd");
        }

        public string ToTimeStr()
        {
            if (this.timeZone != null)
            {
                var dateTimeInZone = TimeZoneInfo.ConvertTime(this.innerDateTime, timeZone);
                return dateTimeInZone.ToString("HH:mm:ss");
            }
            return innerDateTime.ToString("HH:mm:ss");
        }

        public string ToMsStr()
        {
            return innerDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        private void SetTimeInternal(long time)
        {
            innerDateTime = new System.DateTime(time);
        }

        // 提供隐式转换到System.DateTime
        public static implicit operator System.DateTime(DateTimeExt dateTime)
        {
            return dateTime.innerDateTime;
        }

        // 提供隐式转换从System.DateTime
        public static implicit operator DateTimeExt(System.DateTime dateTime)
        {
            return new DateTimeExt(dateTime);
        }


    }
}