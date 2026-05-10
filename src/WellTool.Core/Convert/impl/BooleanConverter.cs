using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 甯冨皵杞崲鍣?    /// </summary>
    public class BooleanConverter : IConverter
    {
        /// <summary>
        /// 杞崲
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return false;
            }

            if (value is bool b)
            {
                return b;
            }

            if (value is string str)
            {
                return ToBoolean(str);
            }

            if (value is IConvertible convertible)
            {
                return convertible.ToBoolean(null);
            }

            return ToBoolean(value.ToString());
        }

        private static bool ToBoolean(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            str = str.Trim().ToLower();

            if (str == "true" || str == "false")
            {
                return bool.Parse(str);
            }

            if (str == "yes" || str == "y" || str == "t" || str == "ok" || str == "1" || str == "on")
            {
                return true;
            }

            return false;
        }

        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(string), typeof(bool), typeof(int), typeof(long), typeof(double) };
        }

        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(bool) };
        }
    }
}