using System;
using System.Globalization;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 基础类型转换器
    /// </summary>
    public class PrimitiveConverter : IConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return GetDefaultValue(targetType);
            }

            var targetTypeCode = Type.GetTypeCode(targetType);

            switch (targetTypeCode)
            {
                case TypeCode.Boolean:
                    return Convert.ToBoolean(value);
                case TypeCode.Byte:
                    return Convert.ToByte(value);
                case TypeCode.Char:
                    return Convert.ToChar(value);
                case TypeCode.DateTime:
                    return Convert.ToDateTime(value);
                case TypeCode.Decimal:
                    return Convert.ToDecimal(value);
                case TypeCode.Double:
                    return Convert.ToDouble(value);
                case TypeCode.Int16:
                    return Convert.ToInt16(value);
                case TypeCode.Int32:
                    return Convert.ToInt32(value);
                case TypeCode.Int64:
                    return Convert.ToInt64(value);
                case TypeCode.SByte:
                    return Convert.ToSByte(value);
                case TypeCode.Single:
                    return Convert.ToSingle(value);
                case TypeCode.UInt16:
                    return Convert.ToUInt16(value);
                case TypeCode.UInt32:
                    return Convert.ToUInt32(value);
                case TypeCode.UInt64:
                    return Convert.ToUInt64(value);
                default:
                    return Convert.ChangeType(value, targetType);
            }
        }

        /// <summary>
        /// 获取默认值
        /// </summary>
        private static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] 
            { 
                typeof(string), typeof(bool), typeof(byte), typeof(char), typeof(DateTime),
                typeof(decimal), typeof(double), typeof(short), typeof(int), typeof(long),
                typeof(sbyte), typeof(float), typeof(ushort), typeof(uint), typeof(ulong)
            };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] 
            { 
                typeof(bool), typeof(byte), typeof(char), typeof(DateTime),
                typeof(decimal), typeof(double), typeof(short), typeof(int), typeof(long),
                typeof(sbyte), typeof(float), typeof(ushort), typeof(uint), typeof(ulong)
            };
        }
    }
}
