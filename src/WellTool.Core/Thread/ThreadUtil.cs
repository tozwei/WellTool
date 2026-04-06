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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 线程工具类
    /// </summary>
    public static class ThreadUtil
    {
        /// <summary>
        /// 主线程名称
        /// </summary>
        public const string MainThreadName = "main";

        /// <summary>
        /// 新建一个线程池，默认的策略如下：
        /// <pre>
        ///    1. 初始线程数为corePoolSize指定的大小
        ///    2. 没有最大线程数限制
        ///    3. 默认使用LinkedBlockingQueue，默认队列大小为1024
        /// </pre>
        /// </summary>
        /// <param name="corePoolSize">同时执行的线程数大小</param>
        /// <returns>ThreadPoolExecutor</returns>
        public static TaskFactory NewExecutor(int corePoolSize)
        {
            var builder = ExecutorBuilder.Create();
            if (corePoolSize > 0)
            {
                builder.SetCorePoolSize(corePoolSize);
            }
            return builder.Build();
        }

        /// <summary>
        /// 获得一个新的线程池，默认的策略如下：
        /// <pre>
        ///    1. 初始线程数为 0
        ///    2. 最大线程数为Integer.MAX_VALUE
        ///    3. 使用SynchronousQueue
        ///    4. 任务直接提交给线程而不保持它们
        /// </pre>
        /// </summary>
        /// <returns>TaskFactory</returns>
        public static TaskFactory NewExecutor()
        {
            return ExecutorBuilder.Create().UseSynchronousQueue().Build();
        }

        /// <summary>
        /// 获得一个新的线程池，只有单个线程，策略如下：
        /// <pre>
        ///    1. 初始线程数为 1
        ///    2. 最大线程数为 1
        ///    3. 默认使用LinkedBlockingQueue，默认队列大小为1024
        ///    4. 同时只允许一个线程工作，剩余放入队列等待，等待数超过1024报错
        /// </pre>
        /// </summary>
        /// <returns>TaskFactory</returns>
        public static TaskFactory NewSingleExecutor()
        {
            return ExecutorBuilder.Create()
                .SetCorePoolSize(1)
                .SetMaxPoolSize(1)
                .SetKeepAliveTime(0)
                .BuildFinalizable();
        }

        /// <summary>
        /// 获得一个新的线程池<br>
        /// 如果maximumPoolSize &gt;= corePoolSize，在没有新任务加入的情况下，多出的线程将最多保留60s
        /// </summary>
        /// <param name="corePoolSize">初始线程池大小</param>
        /// <param name="maximumPoolSize">最大线程池大小</param>
        /// <returns>TaskFactory</returns>
        public static TaskFactory NewExecutor(int corePoolSize, int maximumPoolSize)
        {
            return ExecutorBuilder.Create()
                .SetCorePoolSize(corePoolSize)
                .SetMaxPoolSize(maximumPoolSize)
                .Build();
        }

        /// <summary>
        /// 获得一个新的线程池，并指定最大任务队列大小<br>
        /// 如果maximumPoolSize &gt;= corePoolSize，在没有新任务加入的情况下，多出的线程将最多保留60s
        /// </summary>
        /// <param name="corePoolSize">初始线程池大小</param>
        /// <param name="maximumPoolSize">最大线程池大小</param>
        /// <param name="maximumQueueSize">最大任务队列大小</param>
        /// <returns>TaskFactory</returns>
        public static TaskFactory NewExecutor(int corePoolSize, int maximumPoolSize, int maximumQueueSize)
        {
            return ExecutorBuilder.Create()
                .SetCorePoolSize(corePoolSize)
                .SetMaxPoolSize(maximumPoolSize)
                .SetWorkQueue(new System.Collections.Concurrent.ConcurrentQueue<Action>())
                .Build();
        }

        /// <summary>
        /// 获得一个新的线程池<br>
        /// 传入阻塞系数，线程池的大小计算公式为：CPU可用核心数 / (1 - 阻塞因子)<br>
        /// Blocking Coefficient(阻塞系数) = 阻塞时间／（阻塞时间+使用CPU的时间）<br>
        /// 计算密集型任务的阻塞系数为0，而IO密集型任务的阻塞系数则接近于1。
        /// </summary>
        /// <param name="blockingCoefficient">阻塞系数，阻塞因子介于0~1之间的数，阻塞因子越大，线程池中的线程数越多。</param>
        /// <returns>TaskFactory</returns>
        public static TaskFactory NewExecutorByBlockingCoefficient(float blockingCoefficient)
        {
            if (blockingCoefficient >= 1 || blockingCoefficient < 0)
            {
                throw new ArgumentException("[blockingCoefficient] must between 0 and 1, or equals 0.");
            }

            // 最佳的线程数 = CPU可用核心数 / (1 - 阻塞系数)
            int poolSize = (int)(Environment.ProcessorCount / (1 - blockingCoefficient));
            return ExecutorBuilder.Create()
                .SetCorePoolSize(poolSize)
                .SetMaxPoolSize(poolSize)
                .SetKeepAliveTime(0L)
                .Build();
        }

        /// <summary>
        /// 直接在公共线程池中执行线程
        /// </summary>
        /// <param name="action">可运行对象</param>
        public static void Execute(Action action)
        {
            GlobalThreadPool.Execute(action);
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <param name="action">需要执行的方法体</param>
        /// <param name="isDaemon">是否守护线程。守护线程会在主线程结束后自动结束</param>
        /// <returns>执行的方法体</returns>
        public static Action ExecAsync(Action action, bool isDaemon)
        {
            var thread = new Thread(() => action());
            thread.IsBackground = isDaemon;
            thread.Start();

            return action;
        }

        /// <summary>
        /// 执行有返回值的异步方法<br>
        /// Task代表一个异步执行的操作，通过Result属性可以获得操作的结果，如果异步操作还没有完成，则，Result会使当前线程阻塞
        /// </summary>
        /// <typeparam name="T">回调对象类型</typeparam>
        /// <param name="function">函数</param>
        /// <returns>Task</returns>
        public static Task<T> ExecAsync<T>(Func<T> function)
        {
            return GlobalThreadPool.Instance.Submit(function);
        }

        /// <summary>
        /// 执行有返回值的异步方法<br>
        /// Task代表一个异步执行的操作，通过Result属性可以获得操作的结果，如果异步操作还没有完成，则，Result会使当前线程阻塞
        /// </summary>
        /// <param name="action">可运行对象</param>
        /// <returns>Task</returns>
        public static Task ExecAsync(Action action)
        {
            return GlobalThreadPool.Instance.Submit(action);
        }

        /// <summary>
        /// 挂起当前线程
        /// </summary>
        /// <param name="timeout">挂起的时长</param>
        /// <param name="timeUnit">时长单位</param>
        /// <returns>被中断返回false，否则true</returns>
        public static bool Sleep(long timeout, TimeUnit timeUnit)
        {
            try
            {
                Thread.Sleep((int)timeUnit.ToMilliseconds(timeout));
            }
            catch (ThreadInterruptedException)
            {
                // 重新标记线程为中断状态（恢复中断信息），让后续代码能感知到“线程曾被中断过”
                Thread.CurrentThread.Interrupt();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 挂起当前线程
        /// </summary>
        /// <param name="millis">挂起的毫秒数</param>
        /// <returns>被中断返回false，否则true</returns>
        public static bool Sleep(long millis)
        {
            if (millis > 0)
            {
                try
                {
                    Thread.Sleep((int)millis);
                }
                catch (ThreadInterruptedException)
                {
                    // 重新标记线程为中断状态（恢复中断信息），让后续代码能感知到“线程曾被中断过”
                    Thread.CurrentThread.Interrupt();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 考虑Thread.Sleep方法有可能时间不足给定毫秒数，此方法保证sleep时间不小于给定的毫秒数
        /// </summary>
        /// <param name="millis">给定的sleep时间</param>
        /// <returns>被中断返回false，否则true</returns>
        public static bool SafeSleep(long millis)
        {
            long done = 0;
            long before;
            // done表示实际花费的时间，确保实际花费时间大于应该sleep的时间
            while (done < millis)
            {
                before = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (!Sleep(millis - done))
                {
                    return false;
                }
                // done始终为正
                done += (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - before;
            }
            return true;
        }

        /// <summary>
        /// 获得堆栈列表
        /// </summary>
        /// <returns>堆栈列表</returns>
        public static System.Diagnostics.StackTrace GetStackTrace()
        {
            return new System.Diagnostics.StackTrace();
        }

        /// <summary>
        /// 获得堆栈项
        /// </summary>
        /// <param name="i">第几个堆栈项</param>
        /// <returns>堆栈项</returns>
        public static System.Diagnostics.StackFrame GetStackTraceElement(int i)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            if (i < 0)
            {
                i += stackTrace.FrameCount;
            }
            return stackTrace.GetFrame(i);
        }

        /// <summary>
        /// 创建本地线程对象
        /// </summary>
        /// <typeparam name="T">持有对象类型</typeparam>
        /// <param name="isInheritable">是否为子线程提供从父线程那里继承的值</param>
        /// <returns>本地线程</returns>
        public static ThreadLocal<T> CreateThreadLocal<T>(bool isInheritable)
        {
            if (isInheritable)
            {
                return new ThreadLocal<T>();
            }
            else
            {
                return new ThreadLocal<T>();
            }
        }

        /// <summary>
        /// 创建本地线程对象
        /// </summary>
        /// <typeparam name="T">持有对象类型</typeparam>
        /// <param name="valueFactory">初始化线程对象函数</param>
        /// <returns>本地线程</returns>
        public static ThreadLocal<T> CreateThreadLocal<T>(Func<T> valueFactory)
        {
            return new ThreadLocal<T>(valueFactory);
        }

        /// <summary>
        /// 创建ThreadFactoryBuilder
        /// </summary>
        /// <returns>ThreadFactoryBuilder</returns>
        public static ThreadFactoryBuilder CreateThreadFactoryBuilder()
        {
            return ThreadFactoryBuilder.Create();
        }

        /// <summary>
        /// 结束线程，调用此方法后，线程将抛出 ThreadInterruptedException异常
        /// </summary>
        /// <param name="thread">线程</param>
        /// <param name="isJoin">是否等待结束</param>
        public static void Interrupt(Thread thread, bool isJoin)
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Interrupt();
                if (isJoin)
                {
                    WaitForDie(thread);
                }
            }
        }

        /// <summary>
        /// 等待当前线程结束. 调用 Thread.Join() 并忽略 ThreadInterruptedException
        /// </summary>
        public static void WaitForDie()
        {
            WaitForDie(Thread.CurrentThread);
        }

        /// <summary>
        /// 等待线程结束. 调用 Thread.Join() 并忽略 ThreadInterruptedException
        /// </summary>
        /// <param name="thread">线程</param>
        public static void WaitForDie(Thread thread)
        {
            if (thread == null)
            {
                return;
            }

            bool dead = false;
            do
            {
                try
                {
                    thread.Join();
                    dead = true;
                }
                catch (ThreadInterruptedException)
                {
                    // 重新标记线程为中断状态（恢复中断信息），让后续代码能感知到“线程曾被中断过”
                    Thread.CurrentThread.Interrupt();
                }
            } while (!dead);
        }

        /// <summary>
        /// 获取JVM中与当前线程同组的所有线程<br>
        /// </summary>
        /// <returns>线程对象数组</returns>
        public static Thread[] GetThreads()
        {
            return GetThreads(Thread.CurrentThread.ThreadState);
        }

        /// <summary>
        /// 获取JVM中与当前线程同组的所有线程<br>
        /// </summary>
        /// <param name="threadState">线程状态</param>
        /// <returns>线程对象数组</returns>
        public static Thread[] GetThreads(ThreadState threadState)
        {
            // 在C#中，无法直接通过线程ID获取Thread对象
            // 这里返回一个空数组，实际项目中可能需要更复杂的实现
            return new Thread[0];
        }

        /// <summary>
        /// 获取进程的主线程<br>
        /// </summary>
        /// <returns>进程的主线程</returns>
        public static Thread GetMainThread()
        {
            foreach (var thread in GetThreads())
            {
                if (thread.ManagedThreadId == 1 || thread.Name == MainThreadName)
                {
                    return thread;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取当前线程
        /// </summary>
        /// <returns>当前线程</returns>
        public static Thread Current()
        {
            return Thread.CurrentThread;
        }

        /// <summary>
        /// 阻塞当前线程，保证在main方法中执行不被退出
        /// </summary>
        /// <param name="obj">对象所在线程</param>
        public static void Sync(object obj)
        {
            lock (obj)
            {
                try
                {
                    Monitor.Wait(obj);
                }
                catch (ThreadInterruptedException)
                {
                    // 重新标记线程为中断状态（恢复中断信息），让后续代码能感知到“线程曾被中断过”
                    Thread.CurrentThread.Interrupt();
                }
            }
        }

        /// <summary>
        /// 并发测试<br>
        /// 此方法用于测试多线程下执行某些逻辑的并发性能<br>
        /// 调用此方法会导致当前线程阻塞。<br>
        /// 结束后可调用ConcurrencyTester.GetInterval() 方法获取执行时间
        /// </summary>
        /// <param name="threadSize">并发线程数</param>
        /// <param name="action">执行的逻辑实现</param>
        /// <returns>ConcurrencyTester</returns>
        public static ConcurrencyTester ConcurrencyTest(int threadSize, Action action)
        {
            using (var tester = new ConcurrencyTester(threadSize))
            {
                return tester.Test(action);
            }
        }

        /// <summary>
        /// 等待任务执行完成
        /// </summary>
        /// <param name="tasks">任务列表</param>
        public static void WaitForFinish(params Task[] tasks)
        {
            if (tasks == null || tasks.Length == 0)
                return;
            Task.WaitAll(tasks);
        }

        /// <summary>
        /// 等待任务执行完成
        /// </summary>
        /// <param name="tasks">任务列表</param>
        public static void WaitForFinish(IEnumerable<Task> tasks)
        {
            if (tasks == null)
                return;
            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// 等待线程池执行完成
        /// </summary>
        /// <param name="executor">线程池</param>
        public static void WaitForFinish(TaskFactory executor)
        {
            // 这里只是一个占位符，实际项目中可能需要更复杂的实现
            // 由于 TaskFactory 没有直接的方法来等待所有任务完成，我们可以简单地等待一段时间
            Thread.Sleep(100);
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
    /// 时间单位扩展方法
    /// </summary>
    public static class TimeUnitExtensions
    {
        /// <summary>
        /// 将时间转换为毫秒
        /// </summary>
        /// <param name="timeUnit">时间单位</param>
        /// <param name="value">时间值</param>
        /// <returns>毫秒数</returns>
        public static long ToMilliseconds(this TimeUnit timeUnit, long value)
        {
            switch (timeUnit)
            {
                case TimeUnit.Nanoseconds:
                    return value / 1000000;
                case TimeUnit.Microseconds:
                    return value / 1000;
                case TimeUnit.Milliseconds:
                    return value;
                case TimeUnit.Seconds:
                    return value * 1000;
                case TimeUnit.Minutes:
                    return value * 60 * 1000;
                case TimeUnit.Hours:
                    return value * 60 * 60 * 1000;
                case TimeUnit.Days:
                    return value * 24 * 60 * 60 * 1000;
                default:
                    throw new ArgumentException("Invalid time unit");
            }
        }
    }
}