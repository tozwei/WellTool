using System;

namespace WellTool.Core.Lang.Ref
{
    /// <summary>
    /// 引用类型，用于包装对象但不影响GC
    /// </summary>
    public class Reference<T> where T : class
    {
        private WeakReference<T> _weakRef;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Reference(T value)
        {
            _weakRef = new WeakReference<T>(value);
        }

        /// <summary>
        /// 获取引用对象
        /// </summary>
        public T Get()
        {
            if (_weakRef.TryGetTarget(out var target))
            {
                return target;
            }
            return null;
        }

        /// <summary>
        /// 是否仍被引用
        /// </summary>
        public bool IsAlive => _weakRef.TryGetTarget(out _);

        /// <summary>
        /// 清空引用
        /// </summary>
        public void Clear()
        {
            _weakRef = null;
        }
    }

    /// <summary>
    /// 引用扩展
    /// </summary>
    public static class ReferenceExtensions
    {
        /// <summary>
        /// 创建引用
        /// </summary>
        public static Reference<T> AsRef<T>(this T value) where T : class
        {
            return new Reference<T>(value);
        }

        /// <summary>
        /// 获取值，为null时返回默认值
        /// </summary>
        public static T GetValueOrDefault<T>(this Reference<T> reference, T defaultValue = null) where T : class
        {
            return reference?.Get() ?? defaultValue;
        }
    }
}
