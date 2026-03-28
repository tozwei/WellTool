using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 秒表
    /// </summary>
    public class StopWatch
    {
        private DateTime _startTime;
        private DateTime _stopTime;
        private bool _running;

        /// <summary>
        /// 开始计时
        /// </summary>
        public void Start()
        {
            _startTime = DateTime.Now;
            _running = true;
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        public void Stop()
        {
            _stopTime = DateTime.Now;
            _running = false;
        }

        /// <summary>
        /// 重置秒表
        /// </summary>
        public void Reset()
        {
            _startTime = DateTime.MinValue;
            _stopTime = DateTime.MinValue;
            _running = false;
        }

        /// <summary>
        /// 重新开始计时
        /// </summary>
        public void Restart()
        {
            Reset();
            Start();
        }

        /// <summary>
        /// 获取经过的时间
        /// </summary>
        /// <returns>经过的时间</returns>
        public TimeSpan Elapsed
        {
            get
            {
                if (_running)
                {
                    return DateTime.Now - _startTime;
                }
                else
                {
                    return _stopTime - _startTime;
                }
            }
        }

        /// <summary>
        /// 获取经过的毫秒数
        /// </summary>
        /// <returns>经过的毫秒数</returns>
        public long ElapsedMilliseconds
        {
            get { return (long)Elapsed.TotalMilliseconds; }
        }

        /// <summary>
        /// 获取经过的秒数
        /// </summary>
        /// <returns>经过的秒数</returns>
        public double ElapsedSeconds
        {
            get { return Elapsed.TotalSeconds; }
        }

        /// <summary>
        /// 获取经过的分钟数
        /// </summary>
        /// <returns>经过的分钟数</returns>
        public double ElapsedMinutes
        {
            get { return Elapsed.TotalMinutes; }
        }

        /// <summary>
        /// 获取经过的小时数
        /// </summary>
        /// <returns>经过的小时数</returns>
        public double ElapsedHours
        {
            get { return Elapsed.TotalHours; }
        }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning
        {
            get { return _running; }
        }

        /// <summary>
        /// 获取经过的毫秒数
        /// </summary>
        /// <returns>经过的毫秒数</returns>
        public long Interval()
        {
            return ElapsedMilliseconds;
        }

        /// <summary>
        /// 返回花费时间，并重置开始时间
        /// </summary>
        /// <returns>经过的毫秒数</returns>
        public long IntervalRestart()
        {
            var interval = ElapsedMilliseconds;
            Restart();
            return interval;
        }
    }
}