using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 日期修改器，用于修改日期的各个字段
    /// </summary>
    public class DateModifier
    {
        private DateTime _date;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="date">初始日期</param>
        public DateModifier(DateTime date)
        {
            _date = date;
        }

        /// <summary>
        /// 创建修改器
        /// </summary>
        public static DateModifier Of(DateTime date)
        {
            return new DateModifier(date);
        }

        /// <summary>
        /// 设置年份
        /// </summary>
        public DateModifier SetYear(int year)
        {
            _date = new DateTime(year, _date.Month, _date.Day, _date.Hour, _date.Minute, _date.Second, _date.Millisecond, _date.Kind);
            return this;
        }

        /// <summary>
        /// 设置月份
        /// </summary>
        public DateModifier SetMonth(int month)
        {
            if (month < 1) month = 1;
            if (month > 12) month = 12;
            var maxDay = DateTime.DaysInMonth(_date.Year, month);
            var day = Math.Min(_date.Day, maxDay);
            _date = new DateTime(_date.Year, month, day, _date.Hour, _date.Minute, _date.Second, _date.Millisecond, _date.Kind);
            return this;
        }

        /// <summary>
        /// 设置日期
        /// </summary>
        public DateModifier SetDay(int day)
        {
            var maxDay = DateTime.DaysInMonth(_date.Year, _date.Month);
            day = Math.Min(day, maxDay);
            _date = new DateTime(_date.Year, _date.Month, day, _date.Hour, _date.Minute, _date.Second, _date.Millisecond, _date.Kind);
            return this;
        }

        /// <summary>
        /// 设置小时
        /// </summary>
        public DateModifier SetHour(int hour)
        {
            if (hour < 0) hour = 0;
            if (hour > 23) hour = 23;
            _date = new DateTime(_date.Year, _date.Month, _date.Day, hour, _date.Minute, _date.Second, _date.Millisecond, _date.Kind);
            return this;
        }

        /// <summary>
        /// 设置分钟
        /// </summary>
        public DateModifier SetMinute(int minute)
        {
            if (minute < 0) minute = 0;
            if (minute > 59) minute = 59;
            _date = new DateTime(_date.Year, _date.Month, _date.Day, _date.Hour, minute, _date.Second, _date.Millisecond, _date.Kind);
            return this;
        }

        /// <summary>
        /// 设置秒
        /// </summary>
        public DateModifier SetSecond(int second)
        {
            if (second < 0) second = 0;
            if (second > 59) second = 59;
            _date = new DateTime(_date.Year, _date.Month, _date.Day, _date.Hour, _date.Minute, second, _date.Millisecond, _date.Kind);
            return this;
        }

        /// <summary>
        /// 设置毫秒
        /// </summary>
        public DateModifier SetMillisecond(int millisecond)
        {
            if (millisecond < 0) millisecond = 0;
            if (millisecond > 999) millisecond = 999;
            _date = new DateTime(_date.Year, _date.Month, _date.Day, _date.Hour, _date.Minute, _date.Second, millisecond, _date.Kind);
            return this;
        }

        /// <summary>
        /// 添加年份
        /// </summary>
        public DateModifier AddYears(int years)
        {
            _date = _date.AddYears(years);
            return this;
        }

        /// <summary>
        /// 添加月份
        /// </summary>
        public DateModifier AddMonths(int months)
        {
            _date = _date.AddMonths(months);
            return this;
        }

        /// <summary>
        /// 添加天数
        /// </summary>
        public DateModifier AddDays(int days)
        {
            _date = _date.AddDays(days);
            return this;
        }

        /// <summary>
        /// 添加小时
        /// </summary>
        public DateModifier AddHours(int hours)
        {
            _date = _date.AddHours(hours);
            return this;
        }

        /// <summary>
        /// 添加分钟
        /// </summary>
        public DateModifier AddMinutes(int minutes)
        {
            _date = _date.AddMinutes(minutes);
            return this;
        }

        /// <summary>
        /// 添加秒
        /// </summary>
        public DateModifier AddSeconds(int seconds)
        {
            _date = _date.AddSeconds(seconds);
            return this;
        }

        /// <summary>
        /// 添加毫秒
        /// </summary>
        public DateModifier AddMilliseconds(int milliseconds)
        {
            _date = _date.AddMilliseconds(milliseconds);
            return this;
        }

        /// <summary>
        /// 获取结果日期
        /// </summary>
        public DateTime Get()
        {
            return _date;
        }

        /// <summary>
        /// 隐式转换为DateTime
        /// </summary>
        public static implicit operator DateTime(DateModifier modifier)
        {
            return modifier._date;
        }

        /// <summary>
        /// 转换为DateTime
        /// </summary>
        public DateTime ToDateTime()
        {
            return _date;
        }
    }
}
