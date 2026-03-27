using System;
using System.Globalization;

namespace WellTool.Core.Date
{
    public class DateTime
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

        public static DateTime Of(long timeMillis)
        {
            return new DateTime(timeMillis);
        }

        public static DateTime Of(System.DateTime date)
        {
            if (date is DateTime) {
                return (DateTime)date;
            }
            return new DateTime(date);
        }

        public static DateTime Now()
        {
            return new DateTime();
        }

        public DateTime() : this(System.DateTime.Now)
        {
        }

        public DateTime(TimeZoneInfo timeZone) : this(System.DateTime.Now, timeZone)
        {
        }

        public DateTime(System.DateTime date)
        {
            this.innerDateTime = date;
            this.timeZone = TimeZoneInfo.Local;
            if (date is DateTime) {
                this.timeZone = ((DateTime)date).timeZone;
            }
        }

        public DateTime(System.DateTime date, TimeZoneInfo timeZone)
        {
            this.innerDateTime = date;
            this.timeZone = timeZone ?? TimeZoneInfo.Local;
        }

        public DateTime(long timeMillis) : this(new System.DateTime(timeMillis))
        {
        }

        public DateTime(long timeMillis, TimeZoneInfo timeZone) : this(new System.DateTime(timeMillis), timeZone)
        {
        }

        public DateTime(string dateStr) : this(DateUtil.Parse(dateStr))
        {
        }

        public DateTime(string dateStr, string format) : this(DateUtil.Parse(dateStr, format))
        {
        }

        public DateTime Offset(DateField datePart, int offset)
        {
            var cal = ToCalendar();
            cal.Add((CalendarField)datePart, offset);

            DateTime dt = mutable ? this : new DateTime(this.innerDateTime);
            dt.SetTimeInternal(cal.Ticks);
            return dt;
        }

        public DateTime OffsetNew(DateField datePart, int offset)
        {
            var cal = ToCalendar();
            cal.Add((CalendarField)datePart, offset);

            var dt = new DateTime(this.innerDateTime);
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

        public DateTime SetField(DateField field, int value)
        {
            return SetField((int)field, value);
        }

        public DateTime SetField(int field, int value)
        {
            var calendar = ToCalendar();
            calendar.Set(field, value);

            DateTime dt = this;
            if (!mutable) {
                dt = new DateTime(this.innerDateTime);
            }
            dt.SetTimeInternal(calendar.Ticks);
            return dt;
        }

        public void SetTime(long time)
        {
            if (mutable) {
                innerDateTime = new System.DateTime(time);
            } else {
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
            return Quarter.Of(Quarter());
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
            return Month.Of(Month());
        }

        public int WeekOfYear()
        {
            return GetField(DateField.WeekOfYear);
