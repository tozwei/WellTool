using System;
using System.Collections.Concurrent;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 分组计时器
    /// </summary>
    public class GroupTimeInterval
    {
        private readonly bool _isNano;
        private readonly ConcurrentDictionary<string, long> _groupMap = new ConcurrentDictionary<string, long>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="isNano">是否使用纳秒计数</param>
        public GroupTimeInterval(bool isNano)
        {
            _isNano = isNano;
        }

        /// <summary>
        /// 清空所有计时记录
        /// </summary>
        public void Clear()
        {
            _groupMap.Clear();
        }

        /// <summary>
        /// 为指定分组ID开始计时
        /// </summary>
        public long Start(string id)
        {
            var now = GetTime();
            _groupMap[id] = now;
            return now;
        }

        /// <summary>
        /// 重新开始计时，并返回上一次开始到当前的时间间隔
        /// </summary>
        public long IntervalRestart(string id)
        {
            var now = GetTime();
            var oldTime = _groupMap.GetValueOrDefault(id, now);
            _groupMap[id] = now;
            return now - oldTime;
        }

        /// <summary>
        /// 返回指定分组从开始到当前的时间间隔
        /// </summary>
        public long Interval(string id)
        {
            var startTime = _groupMap.GetValueOrDefault(id, 0);
            if (startTime == 0) return 0;
            return GetTime() - startTime;
        }

        /// <summary>
        /// 返回指定单位的时间间隔
        /// </summary>
        public long Interval(string id, DateUnit unit)
        {
            var interval = Interval(id);
            switch (unit)
            {
                case DateUnit.Millisecond:
                    return interval;
                case DateUnit.Second:
                    return interval / 1000;
                case DateUnit.Minute:
                    return interval / 60000;
                case DateUnit.Hour:
                    return interval / 3600000;
                case DateUnit.Day:
                    return interval / 86400000;
                default:
                    return interval;
            }
        }

        /// <summary>
        /// 从开始到当前的间隔时间（毫秒数）
        /// </summary>
        public long IntervalMs(string id)
        {
            return Interval(id, DateUnit.Millisecond);
        }

        /// <summary>
        /// 从开始到当前的间隔秒数
        /// </summary>
        public long IntervalSecond(string id)
        {
            return Interval(id, DateUnit.Second);
        }

        /// <summary>
        /// 从开始到当前的间隔分钟数
        /// </summary>
        public long IntervalMinute(string id)
        {
            return Interval(id, DateUnit.Minute);
        }

        /// <summary>
        /// 从开始到当前的间隔小时数
        /// </summary>
        public long IntervalHour(string id)
        {
            return Interval(id, DateUnit.Hour);
        }

        /// <summary>
        /// 从开始到当前的间隔天数
        /// </summary>
        public long IntervalDay(string id)
        {
            return Interval(id, DateUnit.Day);
        }

        /// <summary>
        /// 从开始到当前的间隔周数
        /// </summary>
        public long IntervalWeek(string id)
        {
            return IntervalDay(id) / 7;
        }

        /// <summary>
        /// 返回易读的间隔描述
        /// </summary>
        public string IntervalPretty(string id)
        {
            var interval = Interval(id);
            return FormatInterval(interval);
        }

        private string FormatInterval(long intervalMs)
        {
            if (_isNano)
            {
                intervalMs /= 1000000; // 转换为毫秒
            }

            var ms = intervalMs % 1000;
            var seconds = intervalMs / 1000 % 60;
            var minutes = intervalMs / 60000 % 60;
            var hours = intervalMs / 3600000 % 24;
            var days = intervalMs / 86400000;

            if (days > 0)
            {
                return $"{days}天{hours}小时{minutes}分{seconds}秒{ms}毫秒";
            }
            if (hours > 0)
            {
                return $"{hours}小时{minutes}分{seconds}秒{ms}毫秒";
            }
            if (minutes > 0)
            {
                return $"{minutes}分{seconds}秒{ms}毫秒";
            }
            if (seconds > 0)
            {
                return $"{seconds}秒{ms}毫秒";
            }
            return $"{ms}毫秒";
        }

        private long GetTime()
        {
            return _isNano ? System.Diagnostics.Stopwatch.GetTimestamp() / 10000 : DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}