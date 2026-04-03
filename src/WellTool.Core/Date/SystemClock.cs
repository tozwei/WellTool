using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 系统时钟，提供可切换的时间源
    /// </summary>
    public class SystemClock
    {
        private static Func<DateTime> _timeProvider;
        private static Func<long> _millisecondProvider;
        private static Func<long> _tickProvider;

        /// <summary>
        /// 获取当前时间
        /// </summary>
        public static DateTime Now => (_timeProvider ?? (() => DateTime.Now))();

        /// <summary>
        /// 获取当前UTC时间
        /// </summary>
        public static DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// 获取当前毫秒时间戳
        /// </summary>
        public static long CurrentMilliseconds => (_millisecondProvider ?? (() => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond))();

        /// <summary>
        /// 获取当前Tick
        /// </summary>
        public static long CurrentTicks => (_tickProvider ?? (() => DateTime.Now.Ticks))();

        /// <summary>
        /// 设置时间提供者
        /// </summary>
        /// <param name="timeProvider">时间提供函数</param>
        public static void SetTimeProvider(Func<DateTime> timeProvider)
        {
            _timeProvider = timeProvider;
        }

        /// <summary>
        /// 设置毫秒时间戳提供者
        /// </summary>
        /// <param name="millisecondProvider">毫秒时间戳提供函数</param>
        public static void SetMillisecondProvider(Func<long> millisecondProvider)
        {
            _millisecondProvider = millisecondProvider;
        }

        /// <summary>
        /// 重置为系统时钟
        /// </summary>
        public static void Reset()
        {
            _timeProvider = null;
            _millisecondProvider = null;
            _tickProvider = null;
        }

        /// <summary>
        /// 获取Unix时间戳（秒）
        /// </summary>
        public static long UnixSeconds()
        {
            return (Now.Ticks - new DateTime(1970, 1, 1).Ticks) / TimeSpan.TicksPerSecond;
        }

        /// <summary>
        /// 获取Unix时间戳（毫秒）
        /// </summary>
        public static long UnixMilliseconds()
        {
            return (Now.Ticks - new DateTime(1970, 1, 1).Ticks) / TimeSpan.TicksPerMillisecond;
        }

        /// <summary>
        /// 获取Unix时间戳（纳秒）
        /// </summary>
        public static long UnixNanoseconds()
        {
            return (Now.Ticks - new DateTime(1970, 1, 1).Ticks) * 100;
        }

        /// <summary>
        /// 获取毫秒时间戳
        /// </summary>
        public static long Millis()
        {
            return CurrentMilliseconds;
        }

        /// <summary>
        /// 获取秒时间戳
        /// </summary>
        public static long Seconds()
        {
            return CurrentMilliseconds / 1000;
        }

        /// <summary>
        /// 获取秒时间戳
        /// </summary>
        public static long UnixTime()
        {
            return UnixSeconds();
        }
    }
}
