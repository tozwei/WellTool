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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 高并发测试工具类
    /// </summary>
    public class ConcurrencyTester : IDisposable
    {
        private readonly int _threadSize;
        private readonly Stopwatch _stopwatch;
        private long _interval;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="threadSize">线程数</param>
        public ConcurrencyTester(int threadSize)
        {
            _threadSize = threadSize;
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        /// 执行测试<br>
        /// 执行测试后不会关闭线程池，可以调用{@link #Dispose()}释放资源
        /// </summary>
        /// <param name="action">要测试的内容</param>
        /// <returns>this</returns>
        public ConcurrencyTester Test(Action action)
        {
            _stopwatch.Restart();

            var tasks = new List<Task>();
            var barrier = new Barrier(_threadSize);

            for (int i = 0; i < _threadSize; i++)
            {
                tasks.Add(Task.Run(() => {
                    try
                    {
                        // 等待所有线程都准备好
                        barrier.SignalAndWait();
                        action();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            _stopwatch.Stop();
            _interval = _stopwatch.ElapsedMilliseconds;

            return this;
        }

        /// <summary>
        /// 重置测试器，重置包括：
        /// <ul>
        ///     <li>重置计时器</li>
        /// </ul>
        /// </summary>
        /// <returns>this</returns>
        public ConcurrencyTester Reset()
        {
            _stopwatch.Reset();
            return this;
        }

        /// <summary>
        /// 获取执行时间
        /// </summary>
        /// <returns>执行时间，单位毫秒</returns>
        public long GetInterval()
        {
            return _interval;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 不需要释放任何资源
        }
    }
}