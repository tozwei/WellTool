using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 5参数Supplier
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <typeparam name="P1">参数一类型</typeparam>
    /// <typeparam name="P2">参数二类型</typeparam>
    /// <typeparam name="P3">参数三类型</typeparam>
    /// <typeparam name="P4">参数四类型</typeparam>
    /// <typeparam name="P5">参数五类型</typeparam>
    public delegate T Supplier5<out T, in P1, in P2, in P3, in P4, in P5>(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);

    /// <summary>
    /// Supplier5 扩展方法
    /// </summary>
    public static class Supplier5Extensions
    {
        /// <summary>
        /// 将带有参数的Supplier转换为无参Func
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <typeparam name="P2">参数二类型</typeparam>
        /// <typeparam name="P3">参数三类型</typeparam>
        /// <typeparam name="P4">参数四类型</typeparam>
        /// <typeparam name="P5">参数五类型</typeparam>
        /// <param name="supplier">带参数的供应者</param>
        /// <param name="p1">参数1</param>
        /// <param name="p2">参数2</param>
        /// <param name="p3">参数3</param>
        /// <param name="p4">参数4</param>
        /// <param name="p5">参数5</param>
        /// <returns>无参Func</returns>
        public static System.Func<T> ToSupplier<T, P1, P2, P3, P4, P5>(this Supplier5<T, P1, P2, P3, P4, P5> supplier, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            return () => supplier(p1, p2, p3, p4, p5);
        }
    }
}