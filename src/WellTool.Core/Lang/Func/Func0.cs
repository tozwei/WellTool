using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 无参数的函数对象
    /// 一个函数接口代表一个函数，用于包装一个函数为对象
    /// </summary>
    /// <typeparam name="R">返回值类型</typeparam>
    public delegate R Func0<out R>();

    /// <summary>
    /// Func0 扩展方法
    /// </summary>
    public static class Func0Extensions
    {
        /// <summary>
        /// 执行函数，异常包装为RuntimeException
        /// </summary>
        /// <typeparam name="R">返回值类型</typeparam>
        /// <param name="func">函数</param>
        /// <returns>函数执行结果</returns>
        public static R CallWithRuntimeException<R>(this Func0<R> func)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                throw new SystemException("Function execution failed", e);
            }
        }
    }
}