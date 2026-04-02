// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Threading;

namespace WellTool.Core.Threading.Lock
{
    /// <summary>
    /// 无锁实现
    /// </summary>
    public class NoLock
    {
        /// <summary>
        /// 单例实例
        /// </summary>
        public static readonly NoLock Instance = new NoLock();

        /// <summary>
        /// 加锁
        /// </summary>
        public void Lock()
        {
        }

        /// <summary>
        /// 加锁（可中断）
        /// </summary>
        public void LockInterruptibly()
        {
        }

        /// <summary>
        /// 尝试加锁
        /// </summary>
        /// <returns>总是返回 true</returns>
        public bool TryLock()
        {
            return true;
        }

        /// <summary>
        /// 尝试加锁（带超时）
        /// </summary>
        /// <param name="time">超时时间</param>
        /// <param name="unit">时间单位</param>
        /// <returns>总是返回 true</returns>
        public bool TryLock(long time, TimeUnit unit)
        {
            return true;
        }

        /// <summary>
        /// 解锁
        /// </summary>
        public void Unlock()
        {
        }

        /// <summary>
        /// 创建条件变量
        /// </summary>
        /// <returns>不支持，抛出异常</returns>
        public Condition NewCondition()
        {
            throw new System.NotSupportedException("NoLock's NewCondition method is unsupported");
        }
    }

    /// <summary>
    /// 时间单位枚举
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        /// 纳秒
        /// </summary>
        Nanoseconds,
        /// <summary>
        /// 微秒
        /// </summary>
        Microseconds,
        /// <summary>
        /// 毫秒
        /// </summary>
        Milliseconds,
        /// <summary>
        /// 秒
        /// </summary>
        Seconds,
        /// <summary>
        /// 分钟
        /// </summary>
        Minutes,
        /// <summary>
        /// 小时
        /// </summary>
        Hours,
        /// <summary>
        /// 天
        /// </summary>
        Days
    }

    /// <summary>
    /// 条件变量接口
    /// </summary>
    public interface Condition
    {
        /// <summary>
        /// 等待
        /// </summary>
        void Await();

        /// <summary>
        /// 等待（带超时）
        /// </summary>
        /// <param name="time">超时时间</param>
        /// <param name="unit">时间单位</param>
        /// <returns>是否在超时前被唤醒</returns>
        bool Await(long time, TimeUnit unit);

        /// <summary>
        /// 唤醒一个等待线程
        /// </summary>
        void Signal();

        /// <summary>
        /// 唤醒所有等待线程
        /// </summary>
        void SignalAll();
    }
}