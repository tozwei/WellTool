using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 函数对象
    /// 一个函数接口代表一个一个函数，用于包装一个函数为对象
    /// </summary>
    /// <typeparam name="P">参数类型</typeparam>
    /// <typeparam name="R">返回值类型</typeparam>
    public delegate R Func<in P, out R>(params P[] parameters);

    /// <summary>
    /// Func 扩展方法
    /// </summary>
    public static class FuncExtensions
    {
        /// <summary>
        /// 执行函数，异常包装为RuntimeException
        /// </summary>
        /// <typeparam name="P">参数类型</typeparam>
        /// <typeparam name="R">返回值类型</typeparam>
        /// <param name="func">函数</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>函数执行结果</returns>
        public static R CallWithRuntimeException<P, R>(this Func<P, R> func, params P[] parameters)
        {
            try
            {
                return func(parameters);
            }
            catch (Exception e)
            {
                throw new SystemException("Function execution failed", e);
            }
        }
    }
}