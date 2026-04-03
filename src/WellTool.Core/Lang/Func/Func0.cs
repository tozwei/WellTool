using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 0参数函数
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    public delegate T Func0<T>();

    /// <summary>
    /// 函数扩展
    /// </summary>
    public static class FuncExtensions
    {
        /// <summary>
        /// 组合函数
        /// </summary>
        public static System.Func<T1, R> Compose<T1, T2, R>(this Func1<T2, R> func, System.Func<T1, T2> before)
        {
            return arg => func(before(arg));
        }

        /// <summary>
        /// 先执行
        /// </summary>
        public static System.Action<T1> Before<T1, T2>(this System.Action<T2> action, System.Func<T1, T2> before)
        {
            return arg => action(before(arg));
        }

        /// <summary>
        /// 链式执行
        /// </summary>
        public static System.Action<T1> AndThen<T1>(this System.Action<T1> first, System.Action<T1> second)
        {
            return arg =>
            {
                first(arg);
                second(arg);
            };
        }

        /// <summary>
        /// 链式执行
        /// </summary>
        public static System.Func<T1, R> AndThen<T1, T2, R>(this System.Func<T1, T2> first, System.Func<T2, R> second)
        {
            return arg => second(first(arg));
        }
    }
}
