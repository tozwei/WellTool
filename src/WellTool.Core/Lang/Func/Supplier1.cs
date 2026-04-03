using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 1参数Supplier
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <typeparam name="P1">参数一类型</typeparam>
    public delegate T Supplier1<out T, in P1>(P1 p1);

    /// <summary>
    /// Supplier1 扩展方法
    /// </summary>
    public static class Supplier1Extensions
    {
        /// <summary>
        /// 将带有参数的Supplier转换为无参Func
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <param name="supplier">带参数的供应者</param>
        /// <param name="p1">参数1</param>
        /// <returns>无参Func</returns>
		public static System.Func<T> ToSupplier<T, P1>(this Supplier1<T, P1> supplier, P1 p1)
		{
			return () => supplier(p1);
		}
    }
}