using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 日期修改器，用于实现自定义某个日期字段的调整
    /// </summary>
    public class DateModifier
    {
        /// <summary>
        /// 修改类型
        /// </summary>
        public enum ModifyType
        {
            /// <summary>
            /// 取指定日期短的起始值
            /// </summary>
            Truncate,
            /// <summary>
            /// 指定日期属性按照四舍五入处理
            /// </summary>
            Round,
            /// <summary>
            /// 指定日期属性按照进一法处理
            /// </summary>
            Ceiling
        }

        /// <summary>
        /// 修改日期
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="dateField">日期字段，即保留到哪个日期字段</param>
        /// <param name="modifyType">修改类型，包括舍去、四舍五入、进一等</param>
        /// <returns>修改后的日期时间</returns>
        public static DateTime Modify(DateTime dateTime, DateField dateField, ModifyType modifyType)
        {
            return Modify(dateTime, dateField, modifyType, false);
        }

        /// <summary>
        /// 修改日期，取起始值或者结束值
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="dateField">日期字段，即保留到哪个日期字段</param>
        /// <param name="modifyType">修改类型，包括舍去、四舍五入、进一等</param>
        /// <param name="truncateMillisecond">是否归零毫秒</param>
        /// <returns>修改后的日期时间</returns>
        public static DateTime Modify(DateTime dateTime, DateField dateField, ModifyType modifyType, bool truncateMillisecond)
        {
            var result = dateTime;

            // 处理每个更小的字段
            for (int i = (int)dateField + 1; i <= (int)DateField.Millisecond; i++)
            {
                var field = (DateField)i;
                ModifyField(ref result, field, modifyType);
            }

            if (truncateMillisecond)
            {
                result = new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0, result.Kind);
            }

            return result;
        }

        private static void ModifyField(ref DateTime dateTime, DateField field, ModifyType modifyType)
        {
            switch (field)
            {
                case DateField.Year:
                    dateTime = SetFieldValue(dateTime, field, GetBeginValue(dateTime, field), GetEndValue(dateTime, field), modifyType);
                    break;
                case DateField.Month:
                    dateTime = SetFieldValue(dateTime, field, GetBeginValue(dateTime, field), GetEndValue(dateTime, field), modifyType);
                    break;
                case DateField.DayOfMonth:
                    dateTime = SetFieldValue(dateTime, field, GetBeginValue(dateTime, field), GetEndValue(dateTime, field), modifyType);
                    break;
                case DateField.Hour:
                    dateTime = SetFieldValue(dateTime, field, GetBeginValue(dateTime, field), GetEndValue(dateTime, field), modifyType);
                    break;
                case DateField.Minute:
                    dateTime = SetFieldValue(dateTime, field, GetBeginValue(dateTime, field), GetEndValue(dateTime, field), modifyType);
                    break;
                case DateField.Second:
                    dateTime = SetFieldValue(dateTime, field, GetBeginValue(dateTime, field), GetEndValue(dateTime, field), modifyType);
                    break;
                case DateField.Millisecond:
                    dateTime = SetFieldValue(dateTime, field, GetBeginValue(dateTime, field), GetEndValue(dateTime, field), modifyType);
                    break;
            }
        }

        private static DateTime SetFieldValue(DateTime dateTime, DateField field, int min, int max, ModifyType modifyType)
        {
            int currentValue = GetFieldValue(dateTime, field);
            int newValue;

            switch (modifyType)
            {
                case ModifyType.Truncate:
                    newValue = min;
                    break;
                case ModifyType.Round:
                    var middle = min + (max - min) / 2;
                    newValue = currentValue < middle ? min : max;
                    break;
                case ModifyType.Ceiling:
                    newValue = max;
                    break;
                default:
                    newValue = currentValue;
                    break;
            }

            return SetField(dateTime, field, newValue);
        }

        private static int GetFieldValue(DateTime dateTime, DateField field)
        {
            switch (field)
            {
                case DateField.Year:
                    return dateTime.Year;
                case DateField.Month:
                    return dateTime.Month;
                case DateField.DayOfMonth:
                    return dateTime.Day;
                case DateField.Hour:
                    return dateTime.Hour;
                case DateField.Minute:
                    return dateTime.Minute;
                case DateField.Second:
                    return dateTime.Second;
                case DateField.Millisecond:
                    return dateTime.Millisecond;
                default:
                    return 0;
            }
        }

        private static DateTime SetField(DateTime dateTime, DateField field, int value)
        {
            switch (field)
            {
                case DateField.Year:
                    return new DateTime(value, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, dateTime.Kind);
                case DateField.Month:
                    return new DateTime(dateTime.Year, value, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, dateTime.Kind);
                case DateField.DayOfMonth:
                    return new DateTime(dateTime.Year, dateTime.Month, value, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, dateTime.Kind);
                case DateField.Hour:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, value, dateTime.Minute, dateTime.Second, dateTime.Millisecond, dateTime.Kind);
                case DateField.Minute:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, value, dateTime.Second, dateTime.Millisecond, dateTime.Kind);
                case DateField.Second:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, value, dateTime.Millisecond, dateTime.Kind);
                case DateField.Millisecond:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, value, dateTime.Kind);
                default:
                    return dateTime;
            }
        }

        private static int GetBeginValue(DateTime dateTime, DateField field)
        {
            switch (field)
            {
                case DateField.Year:
                    return 1;
                case DateField.Month:
                    return 1;
                case DateField.DayOfMonth:
                    return 1;
                case DateField.Hour:
                    return 0;
                case DateField.Minute:
                    return 0;
                case DateField.Second:
                    return 0;
                case DateField.Millisecond:
                    return 0;
                default:
                    return 0;
            }
        }

        private static int GetEndValue(DateTime dateTime, DateField field)
        {
            switch (field)
            {
                case DateField.Year:
                    return 9999;
                case DateField.Month:
                    return 12;
                case DateField.DayOfMonth:
                    return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                case DateField.Hour:
                    return 23;
                case DateField.Minute:
                    return 59;
                case DateField.Second:
                    return 59;
                case DateField.Millisecond:
                    return 999;
                default:
                    return 0;
            }
        }
    }
}