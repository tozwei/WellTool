using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 两个参数的Supplier
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <typeparam name="P1">参数一类型</typeparam>
    /// <typeparam name="P2">参数二类型</typeparam>
    public delegate T Supplier2<out T, in P1, in P2>(P1 p1, P2 p2);

    /// <summary>
    /// Supplier2 扩展方法
    /// </summary>
    public static class Supplier2Extensions
    {
        /// <summary>
        /// 将带有参数的Supplier转换为无参Func
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <typeparam name="P2">参数二类型</typeparam>
        /// <param name="supplier">带参数的供应者</param>
        /// <param name="p1">参数1</param>
        /// <param name="p2">参数2</param>
        /// <returns>无参Func</returns>
        public static Func<T> ToSupplier<T, P1, P2>(this Supplier2<T, P1, P2> supplier, P1 p1, P2 p2)
        {
            return () => supplier(p1, p2);
        }
    }
}