using System;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 引用类型转换器
    /// </summary>
    public class ReferenceConverter : IConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            // 如果值是 WeakReference 或类似引用类型，提取其目标
            if (value is WeakReference wr)
            {
                return wr.Target;
            }

            // 如果值已经是目标引用类型，直接返回
            if (targetType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            // 尝试转换
            return System.Convert.ChangeType(value, targetType);
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(WeakReference), typeof(object) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(object) };
        }
    }
}
