using System;

namespace WellTool.Core.Convert
{
    public interface TypeConverter
    {
        object Convert(Type targetType, object value);
    }
}
