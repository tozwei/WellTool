namespace WellTool.Core.Exceptions;

using WellTool.Core.Lang.Func;
using System.Runtime.ExceptionServices;

/// <summary>
/// 方便的执行会抛出受检查类型异常的方法调用或者代码段
/// </summary>
/// <remarks>
/// 该工具通过函数式的方式将那些需要抛出受检查异常的表达式或者代码段转化成一个 Func 对象
/// 
/// 使用示例:
/// <code>
/// // 以前需要这样写
/// try {
///     var result = SomeMethod();
/// } catch (Exception e) {
///     throw new RuntimeException(e);
/// }
/// 
/// // 现在可以这样写：
/// var result = CheckedUtil.Uncheck(() => SomeMethod()).Call();
/// </code>
/// </remarks>
/// <author>conder</author>
/// <since>5.7.19</since>
public class CheckedUtil
{
    /// <summary>
    /// 接收一个可以转化成 Func 的 Lambda 表达式，当执行表达式抛出任何异常的时候，都会转化成运行时异常
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <typeparam name="T">参数类型</typeparam>
    /// <typeparam name="R">返回类型</typeparam>
    /// <returns>FuncRt</returns>
    public static FuncRt<T, R> Uncheck<T, R>(System.Func<T, R> expression)
    {
        return Uncheck(expression, e => new RuntimeException(e));
    }

    /// <summary>
    /// 接收一个可以转化成 Func0 的 Lambda 表达式，当执行表达式抛出任何异常的时候，都会转化成运行时异常
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <typeparam name="R">返回类型</typeparam>
    /// <returns>Func0Rt</returns>
    public static Func0Rt<R> Uncheck<R>(Func0<R> expression)
    {
        return Uncheck(expression, e => new RuntimeException(e));
    }



    /// <summary>
    /// 接收一个可以转化成 VoidFunc 的 Lambda 表达式，当执行表达式抛出任何异常的时候，都会转化成运行时异常
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <typeparam name="T">参数类型</typeparam>
    /// <returns>VoidFuncRt</returns>
    public static VoidFuncRt<T> Uncheck<T>(VoidFunc<T> expression)
    {
        return Uncheck(expression, e => new RuntimeException(e));
    }

    /// <summary>
    /// 接收一个可以转化成 VoidFunc 的 Lambda 表达式，当执行表达式抛出任何异常的时候，都会转化成运行时异常
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <returns>VoidFunc0Rt</returns>
    public static VoidFunc0Rt Uncheck(VoidFunc expression)
    {
        return Uncheck(expression, e => new RuntimeException(e));
    }



    /// <summary>
    /// 接收一个可以转化成 Func 的 Lambda 表达式，和一个可以把 Exception 转化成 RuntimeException 的表达式
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <param name="rteSupplier">转化运行时异常的表达式</param>
    /// <typeparam name="T">参数类型</typeparam>
    /// <typeparam name="R">返回类型</typeparam>
    /// <returns>FuncRt</returns>
    public static FuncRt<T, R> Uncheck<T, R>(System.Func<T, R> expression, System.Func<System.Exception, RuntimeException> rteSupplier)
    {
       #if NET6_0_OR_GREATER
    // .NET 6 及以上版本：使用新 API
    ArgumentNullException.ThrowIfNull(expression);
#else
    // 旧版本 (.NET Framework / .NET Core 2.x - 5.x)：使用传统写法
    if (expression == null)
    {
        throw new ArgumentNullException(nameof(expression));
    }
#endif
        return t =>
        {
            try
            {
                return expression(t);
            }
            catch (System.Exception e)
            {
                throw rteSupplier == null ? new RuntimeException(e) : rteSupplier(e);
            }
        };
    }

    /// <summary>
    /// 接收一个可以转化成 Func0 的 Lambda 表达式，和一个可以把 Exception 转化成 RuntimeException 的表达式
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <param name="rteSupplier">转化运行时异常的表达式</param>
    /// <typeparam name="R">返回类型</typeparam>
    /// <returns>Func0Rt</returns>
    public static Func0Rt<R> Uncheck<R>(Func0<R> expression, System.Func<System.Exception, RuntimeException> rteSupplier)
    {
#if NET6_0_OR_GREATER
        // .NET 6 及以上版本：使用新 API
        ArgumentNullException.ThrowIfNull(expression);
#else
    // 旧版本 (.NET Framework / .NET Core 2.x - 5.x)：使用传统写法
    if (expression == null)
    {
        throw new ArgumentNullException(nameof(expression));
    }
#endif
        return () =>
        {
            try
            {
                return expression();
            }
            catch (System.Exception e)
            {
                throw rteSupplier == null ? new RuntimeException(e) : rteSupplier(e);
            }
        };
    }



    /// <summary>
    /// 接收一个可以转化成 VoidFunc 的 Lambda 表达式，和一个可以把 Exception 转化成 RuntimeException 的表达式
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <param name="rteSupplier">转化运行时异常的表达式</param>
    /// <typeparam name="T">参数类型</typeparam>
    /// <returns>VoidFuncRt</returns>
    public static VoidFuncRt<T> Uncheck<T>(VoidFunc<T> expression, System.Func<System.Exception, RuntimeException> rteSupplier)
    {
#if NET6_0_OR_GREATER
        // .NET 6 及以上版本：使用新 API
        ArgumentNullException.ThrowIfNull(expression);
#else
    // 旧版本 (.NET Framework / .NET Core 2.x - 5.x)：使用传统写法
    if (expression == null)
    {
        throw new ArgumentNullException(nameof(expression));
    }
#endif
        return t =>
        {
            try
            {
                expression(t);
            }
            catch (System.Exception e)
            {
                throw rteSupplier == null ? new RuntimeException(e) : rteSupplier(e);
            }
        };
    }

    /// <summary>
    /// 接收一个可以转化成 VoidFunc 的 Lambda 表达式，和一个 RuntimeException
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <param name="rte">期望抛出的运行时异常</param>
    /// <returns>VoidFunc0Rt</returns>
    public static VoidFunc0Rt Uncheck(VoidFunc expression, RuntimeException rte)
    {
#if NET6_0_OR_GREATER
        // .NET 6 及以上版本：使用新 API
        ArgumentNullException.ThrowIfNull(expression);
#else
    // 旧版本 (.NET Framework / .NET Core 2.x - 5.x)：使用传统写法
    if (expression == null)
    {
        throw new ArgumentNullException(nameof(expression));
    }
#endif
        return () =>
        {
            try
            {
                expression();
            }
            catch (System.Exception e)
            {
                if (rte == null)
                {
                    throw new RuntimeException(e);
                }
                else
                {
                    rte.Data["Cause"] = e;
                    throw rte;
                }
            }
        };
    }


}

/// <summary>
/// Func 接口的运行时版本
/// </summary>
public delegate R FuncRt<T, R>(T t);

/// <summary>
/// Func0 接口的运行时版本
/// </summary>
public delegate R Func0Rt<out R>();

/// <summary>
/// Func1 接口的运行时版本
/// </summary>
public delegate R Func1Rt<in P, out R>(P parameter);

/// <summary>
/// VoidFunc 接口的运行时版本
/// </summary>
public delegate void VoidFuncRt<T>(T t);

/// <summary>
/// VoidFunc0 接口的运行时版本
/// </summary>
public delegate void VoidFunc0Rt();

/// <summary>
/// VoidFunc1 接口的运行时版本
/// </summary>
public delegate void VoidFunc1Rt<in P>(P parameter);
