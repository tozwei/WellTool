using System;

namespace WellTool.Core.Converter
{
    public interface TypeConverter
    {
        object Convert(Type targetType, object value);
    }
}
