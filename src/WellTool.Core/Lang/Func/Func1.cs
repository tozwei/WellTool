using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 只有一个参数的函数对象
    /// 一个函数接口代表一个函数，用于包装一个函数为对象
    /// </summary>
    /// <typeparam name="P">参数类型</typeparam>
    /// <typeparam name="R">返回值类型</typeparam>
    public delegate R Func1<in P, out R>(P parameter);

    /// <summary>
    /// Func1 扩展方法
    /// </summary>
    public static class Func1Extensions
    {
        /// <summary>
        /// 执行函数，异常包装为RuntimeException
        /// </summary>
        /// <typeparam name="P">参数类型</typeparam>
        /// <typeparam name="R">返回值类型</typeparam>
        /// <param name="func">函数</param>
        /// <param name="parameter">参数</param>
        /// <returns>函数执行结果</returns>
        public static R CallWithRuntimeException<P, R>(this Func1<P, R> func, P parameter)
        {
            try
            {
                return func(parameter);
            }
            catch (Exception e)
            {
                throw new SystemException("Function execution failed", e);
            }
        }
    }
}