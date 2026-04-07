using System;

namespace WellTool.Core.Lang.Func
{
    #region Consumer 接口和实现

    /// <summary>
    /// Consumer接口
    /// </summary>
    /// <typeparam name="T">消费类型</typeparam>
    public interface IConsumer<in T>
    {
        void Accept(T t);
    }

    /// <summary>
    /// Consumer实现
    /// </summary>
    /// <typeparam name="T">消费类型</typeparam>
    public class Consumer<T> : IConsumer<T>
    {
        private readonly System.Action<T> _action;

        public Consumer(System.Action<T> action)
        {
            _action = action;
        }

        public void Accept(T t) => _action(t);
    }

    /// <summary>
    /// 三参数消费者接口
    /// </summary>
    /// <typeparam name="T1">参数1类型</typeparam>
    /// <typeparam name="T2">参数2类型</typeparam>
    /// <typeparam name="T3">参数3类型</typeparam>
    public interface IConsumer3<T1, T2, T3>
    {
        /// <summary>
        /// 执行消费操作
        /// </summary>
        void Accept(T1 t1, T2 t2, T3 t3);
    }

    /// <summary>
    /// 三参数消费者实现
    /// </summary>
    /// <typeparam name="T1">参数1类型</typeparam>
    /// <typeparam name="T2">参数2类型</typeparam>
    /// <typeparam name="T3">参数3类型</typeparam>
    public class Consumer3<T1, T2, T3> : IConsumer3<T1, T2, T3>
    {
        private readonly Action<T1, T2, T3> _action;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="action">消费动作</param>
        public Consumer3(Action<T1, T2, T3> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <inheritdoc />
        public void Accept(T1 t1, T2 t2, T3 t3) => _action(t1, t2, t3);

        /// <summary>
        /// 转换为Action
        /// </summary>
        public void Invoke(T1 t1, T2 t2, T3 t3) => _action(t1, t2, t3);
    }

    #endregion
    #region Func 委托定义

    /// <summary>
    /// 0参数函数
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    public delegate T Func0<T>();

    /// <summary>
    /// 只有一个参数的函数对象
    /// 一个函数接口代表一个函数，用于包装一个函数为对象
    /// </summary>
    /// <typeparam name="P">参数类型</typeparam>
    /// <typeparam name="R">返回值类型</typeparam>
    public delegate R Func1<in P, out R>(P parameter);

    #endregion

    #region Func 扩展方法

    /// <summary>
    /// 函数扩展
    /// </summary>
    public static class FuncExtensions
    {
        /// <summary>
        /// 执行函数，异常包装为RuntimeException
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="func">函数</param>
        /// <returns>函数执行结果</returns>
        public static T CallWithRuntimeException<T>(this Func0<T> func)
        {
            try
            {
                return func();
            }
            catch (System.Exception e)
            {
                throw new SystemException("Function execution failed", e);
            }
        }

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
            catch (System.Exception e)
            {
                throw new SystemException("Function execution failed", e);
            }
        }
    }

    #endregion

    #region Func 接口定义

    /// <summary>
    /// 函数对象接口，用于包装一个函数为对象
    /// </summary>
    /// <typeparam name="P">参数类型</typeparam>
    /// <typeparam name="R">返回值类型</typeparam>
    public interface IFunc<P, R>
    {
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="parameters">参数列表</param>
        /// <returns>函数执行结果</returns>
        R Call(params P[] parameters);

        /// <summary>
        /// 执行函数，异常包装为RuntimeException
        /// </summary>
        /// <param name="parameters">参数列表</param>
        /// <returns>函数执行结果</returns>
        R CallWithRuntimeException(params P[] parameters);
    }

    /// <summary>
    /// 无参数的函数对象接口
    /// </summary>
    /// <typeparam name="R">返回值类型</typeparam>
    public interface IFunc0<R>
    {
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <returns>函数执行结果</returns>
        R Call();

        /// <summary>
        /// 执行函数，异常包装为RuntimeException
        /// </summary>
        /// <returns>函数执行结果</returns>
        R CallWithRuntimeException();
    }

    /// <summary>
    /// 只有一个参数的函数对象接口
    /// </summary>
    /// <typeparam name="P">参数类型</typeparam>
    /// <typeparam name="R">返回值类型</typeparam>
    public interface IFunc1<P, R>
    {
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>函数执行结果</returns>
        R Call(P parameter);

        /// <summary>
        /// 执行函数，异常包装为RuntimeException
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>函数执行结果</returns>
        R CallWithRuntimeException(P parameter);
    }

    /// <summary>
    /// 两个参数的函数对象接口
    /// </summary>
    /// <typeparam name="P1">参数1类型</typeparam>
    /// <typeparam name="P2">参数2类型</typeparam>
    /// <typeparam name="R">返回值类型</typeparam>
    public interface IFunc2<P1, P2, R>
    {
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="p1">参数1</param>
        /// <param name="p2">参数2</param>
        /// <returns>函数执行结果</returns>
        R Call(P1 p1, P2 p2);

        /// <summary>
        /// 执行函数，异常包装为RuntimeException
        /// </summary>
        /// <param name="p1">参数1</param>
        /// <param name="p2">参数2</param>
        /// <returns>函数执行结果</returns>
        R CallWithRuntimeException(P1 p1, P2 p2);
    }

    /// <summary>
    /// 三个参数的函数对象接口
    /// </summary>
    /// <typeparam name="P1">参数1类型</typeparam>
    /// <typeparam name="P2">参数2类型</typeparam>
    /// <typeparam name="P3">参数3类型</typeparam>
    /// <typeparam name="R">返回值类型</typeparam>
    public interface IFunc3<P1, P2, P3, R>
    {
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="p1">参数1</param>
        /// <param name="p2">参数2</param>
        /// <param name="p3">参数3</param>
        /// <returns>函数执行结果</returns>
        R Call(P1 p1, P2 p2, P3 p3);

        /// <summary>
        /// 执行函数，异常包装为RuntimeException
        /// </summary>
        /// <param name="p1">参数1</param>
        /// <param name="p2">参数2</param>
        /// <param name="p3">参数3</param>
        /// <returns>函数执行结果</returns>
        R CallWithRuntimeException(P1 p1, P2 p2, P3 p3);
    }

    #endregion

    #region Supplier 接口和类

    /// <summary>
    /// 提供者接口
    /// </summary>
    public interface ISupplier<T>
    {
        /// <summary>
        /// 获取值
        /// </summary>
        T Get();
    }

    /// <summary>
    /// 提供者接口（无参数）
    /// </summary>
    public interface ISupplier
    {
        /// <summary>
        /// 获取值
        /// </summary>
        object Get();
    }

    /// <summary>
    /// 提供者
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class Supplier<T> : ISupplier<T>
    {
        private readonly System.Func<T> _func;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Supplier(System.Func<T> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public T Get()
        {
            return _func();
        }

        /// <summary>
        /// 转换
        /// </summary>
        public R Map<R>(System.Func<T, R> mapper)
        {
            return mapper(_func());
        }
    }

    /// <summary>
    /// 空提供者
    /// </summary>
    public class NullSupplier<T> : ISupplier<T>
    {
        private static readonly NullSupplier<T> _instance = new NullSupplier<T>();

        public static NullSupplier<T> Instance => _instance;

        private NullSupplier() { }

        public T Get()
        {
            return default;
        }
    }

    #endregion

    #region Supplier 委托定义

    /// <summary>
    /// 1参数Supplier
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <typeparam name="P1">参数一类型</typeparam>
    public delegate T Supplier1<out T, in P1>(P1 p1);

    /// <summary>
    /// 2参数提供者
    /// </summary>
    public delegate T2 Supplier2<T1, T2>(T1 arg1);
    public delegate T3 Supplier3<T1, T2, T3>(T1 arg1, T2 arg2);
    public delegate T4 Supplier4<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3);
    public delegate T5 Supplier5<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    #endregion

    #region Supplier 扩展方法

    /// <summary>
    /// 提供者扩展
    /// </summary>
    public static class SupplierExtensions
    {
        /// <summary>
        /// 创建提供者
        /// </summary>
        public static Supplier<T> ToSupplier<T>(this System.Func<T> func)
        {
            return new Supplier<T>(func);
        }

        /// <summary>
        /// 获取值，如果为null则返回默认值
        /// </summary>
        public static T GetOrDefault<T>(this Supplier<T> supplier, T defaultValue = default)
        {
            if (supplier == null)
            {
                return defaultValue;
            }
            return supplier.Get();
        }

        /// <summary>
        /// 获取值，如果为null则使用工厂方法
        /// </summary>
        public static T GetOrDefault<T>(this Supplier<T> supplier, System.Func<T> factory)
        {
            if (supplier == null)
            {
                return factory();
            }
            return supplier.Get();
        }
    }

    /// <summary>
    /// Supplier1 扩展方法
    /// </summary>
    public static class Supplier1Extensions
    {
        /// <summary>
        /// 将带有参数的Supplier转换为无参Func
        /// </summary>
        public static System.Func<T> ToSupplier<T, P1>(this Supplier1<T, P1> supplier, P1 p1)
        {
            return () => supplier(p1);
        }
    }

    #endregion
}