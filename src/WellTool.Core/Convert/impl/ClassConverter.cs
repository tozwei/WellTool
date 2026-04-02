using System;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 类型转换器
    /// </summary>
    public class ClassConverter : IConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的值</returns>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            if (targetType != typeof(Type))
            {
                return value;
            }

            if (value is Type type)
            {
                return type;
            }

            if (value is string str)
            {
                try
                {
                    return Type.GetType(str, false);
                }
                catch
                {
                    // 转换失败，返回原值
                }
            }

            return value;
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(object) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(Type) };
        }
    }
}

