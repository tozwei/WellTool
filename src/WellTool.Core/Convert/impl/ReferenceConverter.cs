using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.impl
{
    /// <summary>
    /// 引用类型转换�?
    /// </summary>
    public class ReferenceConverter : IConverter
    {
        /// <summary>
        /// 转换�?
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            // 如果值是 WeakReference 或类似引用类型，提取其目�?
            if (value is WeakReference wr)
            {
                return wr.Target;
            }

            // 如果值已经是目标引用类型，直接返�?
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
        /// 获取支持的目标类�?
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(object) };
        }
    }
}
