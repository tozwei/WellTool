using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    public class CharacterConverter : IConverter
    {
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            if (targetType != typeof(char) && targetType != typeof(char?))
            {
                return value;
            }

            if (value is char c)
            {
                return c;
            }

            if (value is string str)
            {
                if (str.Length > 0)
                {
                    return str[0];
                }
            }
            else if (value is IConvertible convertible)
            {
                try
                {
                    return convertible.ToChar(null);
                }
                catch
                {
                }
            }

            return value;
        }

        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(object) };
        }

        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(char), typeof(char?) };
        }
    }
}