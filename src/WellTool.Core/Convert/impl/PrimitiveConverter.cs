using System;
using WellTool.Core.Convert;
using System.Globalization;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 鍩虹绫诲瀷杞崲锟?
    /// </summary>
    public class PrimitiveConverter : IConverter
    {
        /// <summary>
        /// 杞崲锟?        /// </summary>
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
                    return System.Convert.ToBoolean(value);
                case TypeCode.Byte:
                    return System.Convert.ToByte(value);
                case TypeCode.Char:
                    return System.Convert.ToChar(value);
                case TypeCode.DateTime:
                    return System.Convert.ToDateTime(value);
                case TypeCode.Decimal:
                    return System.Convert.ToDecimal(value);
                case TypeCode.Double:
                    return System.Convert.ToDouble(value);
                case TypeCode.Int16:
                    return System.Convert.ToInt16(value);
                case TypeCode.Int32:
                    return System.Convert.ToInt32(value);
                case TypeCode.Int64:
                    return System.Convert.ToInt64(value);
                case TypeCode.SByte:
                    return System.Convert.ToSByte(value);
                case TypeCode.Single:
                    return System.Convert.ToSingle(value);
                case TypeCode.UInt16:
                    return System.Convert.ToUInt16(value);
                case TypeCode.UInt32:
                    return System.Convert.ToUInt32(value);
                case TypeCode.UInt64:
                    return System.Convert.ToUInt64(value);
                default:
                    return System.Convert.ChangeType(value, targetType);
            }
        }

        /// <summary>
        /// 鑾峰彇榛樿锟?
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
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
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
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被锟?
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

