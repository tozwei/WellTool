using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 0参数函数
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    public delegate T Func0<T>();

    /// <summary>
    /// 1参数函数
    /// </summary>
    /// <typeparam name="T1">参数1类型</typeparam>
    /// <typeparam name="T">返回值类型</typeparam>
    public delegate T Func1<T1, T>(T1 arg1);

    /// <summary>
    /// 函数扩展
    /// </summary>
    public static class FuncExtensions
    {
        /// <summary>
        /// 组合函数
        /// </summary>
        public static Func<T1, R> Compose<T1, T2, R>(this Func1<T2, R> func, Func<T1, T2> before)
        {
            return arg => func(before(arg));
        }

        /// <summary>
        /// 先执行
        /// </summary>
        public static Action<T1> Before<T1, T2>(this Action<T2> action, Func<T1, T2> before)
        {
            return arg => action(before(arg));
        }

        /// <summary>
        /// 链式执行
        /// </summary>
        public static Action<T1> AndThen<T1>(this Action<T1> first, Action<T1> second)
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
        public static Func<T1, R> AndThen<T1, T2, R>(this Func<T1, T2> first, Func<T2, R> second)
        {
            return arg => second(first(arg));
        }
    }
}
