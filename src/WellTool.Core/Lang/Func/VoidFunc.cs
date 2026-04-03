using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 无参无返回值委托
    /// </summary>
    public delegate void VoidFunc();

    /// <summary>
    /// 单参数无返回值委托
    /// </summary>
    public delegate void VoidFunc<T>(T arg);

    /// <summary>
    /// 双参数无返回值委托
    /// </summary>
    public delegate void VoidFunc<T1, T2>(T1 arg1, T2 arg2);

    /// <summary>
    /// Lambda表达式工具
    /// </summary>
    public static class LambdaUtil
    {
        /// <summary>
        /// 将值类型的工厂方法转换为引用类型
        /// </summary>
        public static Func<T> BoxValue<T>(Func<T> factory) where T : struct
        {
            return () => factory();
        }

        /// <summary>
        /// 安全调用
        /// </summary>
        public static void SafeCall(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch
            {
                // 忽略异常
            }
        }

        /// <summary>
        /// 安全调用
        /// </summary>
        public static TResult SafeCall<TResult>(Func<TResult> func, TResult defaultValue = default)
        {
            try
            {
                return func?.Invoke() ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 延迟执行
        /// </summary>
        public static IDisposable Delay(Action action, TimeSpan delay)
        {
            var timer = new System.Timers.Timer(delay.TotalMilliseconds);
            timer.Elapsed += (s, e) =>
            {
                action?.Invoke();
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
            return timer;
        }

        /// <summary>
        /// 执行并计时
        /// </summary>
        public static TimeSpan Execute(Action action)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            action?.Invoke();
            sw.Stop();
            return sw.Elapsed;
        }
    }
}
