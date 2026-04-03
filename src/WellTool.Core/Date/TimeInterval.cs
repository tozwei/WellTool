using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 时间间隔计算器
    /// </summary>
    public class TimeInterval : GroupTimeInterval
    {
        private const string DEFAULT_ID = "";

        /// <summary>
        /// 构造，默认使用毫秒计数
        /// </summary>
        public TimeInterval() : this(false)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="isNano">是否使用纳秒计数，false则使用毫秒</param>
        public TimeInterval(bool isNano) : base(isNano)
        {
            Start();
        }

        /// <summary>
        /// 开始计时并返回当前时间
        /// </summary>
        public long Start()
        {
            return Start(DEFAULT_ID);
        }

        /// <summary>
        /// 重新计时并返回从开始到当前的持续时间
        /// </summary>
        public long IntervalRestart()
        {
            return IntervalRestart(DEFAULT_ID);
        }

        /// <summary>
        /// 重新开始计算时间（重置开始时间）
        /// </summary>
        public TimeInterval Restart()
        {
            Start(DEFAULT_ID);
            return this;
        }

        /// <summary>
        /// 从开始到当前的间隔时间（毫秒数）
        /// </summary>
        public long Interval()
        {
            return Interval(DEFAULT_ID);
        }

        /// <summary>
        /// 从开始到当前的间隔时间，返回XX天XX小时XX分XX秒XX毫秒
        /// </summary>
        public string IntervalPretty()
        {
            return IntervalPretty(DEFAULT_ID);
        }

        /// <summary>
        /// 从开始到当前的间隔时间（毫秒数）
        /// </summary>
        public long IntervalMs()
        {
            return IntervalMs(DEFAULT_ID);
        }

        /// <summary>
        /// 从开始到当前的间隔秒数，取绝对值
        /// </summary>
        public long IntervalSecond()
        {
            return IntervalSecond(DEFAULT_ID);
        }

        /// <summary>
        /// 从开始到当前的间隔分钟数，取绝对值
        /// </summary>
        public long IntervalMinute()
        {
            return IntervalMinute(DEFAULT_ID);
        }

        /// <summary>
        /// 从开始到当前的间隔小时数，取绝对值
        /// </summary>
        public long IntervalHour()
        {
            return IntervalHour(DEFAULT_ID);
        }

        /// <summary>
        /// 从开始到当前的间隔天数，取绝对值
        /// </summary>
        public long IntervalDay()
        {
            return IntervalDay(DEFAULT_ID);
        }

        /// <summary>
        /// 从开始到当前的间隔周数，取绝对值
        /// </summary>
        public long IntervalWeek()
        {
            return IntervalWeek(DEFAULT_ID);
        }
    }
}