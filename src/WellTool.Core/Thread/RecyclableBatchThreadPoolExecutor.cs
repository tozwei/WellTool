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
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 可召回批处理线程池执行器
    /// <pre>
    /// 1.数据分批并行处理
    /// 2.主线程、线程池混合执行批处理任务，主线程空闲时会尝试召回线程池队列中的任务执行
    /// 3.线程安全，可用同时执行多个任务，线程池满载时，效率与单线程模式相当，无阻塞风险，无脑提交任务即可
    /// </pre>
    /// 
    /// 适用场景：
    /// <pre>
    /// 1.批量处理数据且需要同步结束的场景，能一定程度上提高吞吐量、防止任务堆积 {@link #Process(List, int, Func)}
    /// 2.普通查询接口加速 {@link #ProcessByWarp(Warp[])}
    /// </pre>
    /// </summary>
    public class RecyclableBatchThreadPoolExecutor : IDisposable
    {
        private readonly TaskFactory _taskFactory;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="poolSize">线程池大小</param>
        public RecyclableBatchThreadPoolExecutor(int poolSize) : this(poolSize, "recyclable-batch-pool-")
        {
        }

        /// <summary>
        /// 建议的构造方法
        /// <pre>
        /// 1.使用无界队列，主线程会召回队列中的任务执行，不会有任务堆积，无需考虑拒绝策略
        /// 2.假如在web场景中请求量过大导致oom，不使用此工具也会有同样的结果，甚至更严重，应该对请求做限制或做其他优化
        /// </pre>
        /// </summary>
        /// <param name="poolSize">线程池大小</param>
        /// <param name="threadPoolPrefix">线程名前缀</param>
        public RecyclableBatchThreadPoolExecutor(int poolSize, string threadPoolPrefix)
        {
            var threadFactory = new NamedThreadFactory(threadPoolPrefix, false);
            var taskScheduler = new ConcurrentTaskScheduler(poolSize, threadFactory);
            _taskFactory = new TaskFactory(taskScheduler);
        }

        /// <summary>
        /// 自定义任务工厂，一般不需要使用
        /// </summary>
        /// <param name="taskFactory">任务工厂</param>
        public RecyclableBatchThreadPoolExecutor(TaskFactory taskFactory)
        {
            _taskFactory = taskFactory;
        }

        /// <summary>
        /// 关闭线程池
        /// </summary>
        public void Shutdown()
        {
            // 在C#中，TaskFactory不需要手动关闭
        }

        /// <summary>
        /// 获取任务工厂
        /// </summary>
        /// <returns>TaskFactory</returns>
        public TaskFactory GetTaskFactory()
        {
            return _taskFactory;
        }

        /// <summary>
        /// 分批次处理数据
        /// <pre>
        /// 1.所有批次执行完成后会过滤null并返回合并结果，保持输入数据顺序，不需要结果{@link Func}返回null即可
        /// 2.{@link Func}需自行处理异常、保证线程安全
        /// 3.原始数据在分片后可能被外部修改，导致批次数据不一致，如有必要，传参之前进行数据拷贝
        /// 4.主线程会参与处理批次数据，如果要异步执行任务请使用普通线程池
        /// </pre>
        /// </summary>
        /// <typeparam name="T">输入数据类型</typeparam>
        /// <typeparam name="R">输出数据类型</typeparam>
        /// <param name="data">待处理数据集合</param>
        /// <param name="batchSize">每批次数据量</param>
        /// <param name="processor">单条数据处理函数</param>
        /// <returns>处理结果集合</returns>
        public List<R> Process<T, R>(List<T> data, int batchSize, Func<T, R> processor)
        {
            if (batchSize < 1)
            {
                throw new ArgumentException("batchSize >= 1");
            }

            var batches = SplitData(data, batchSize);
            int batchCount = batches.Count;
            int minusOne = batchCount - 1;
            var taskQueue = new ConcurrentQueue<IdempotentTask<R>>();
            var futuresMap = new ConcurrentDictionary<int, CancellableTaskWrapper<R>>();

            // 提交前 batchCount-1 批任务
            for (int i = 0; i < minusOne; i++)
            {
                var batch = batches[i];
                var task = new IdempotentTask<R>(i, () => ProcessBatch(batch, processor));
                taskQueue.Enqueue(task);
                var cts = new CancellationTokenSource();
                var future = _taskFactory.StartNew(() => task.Call(), cts.Token);
                futuresMap.TryAdd(i, new CancellableTaskWrapper<R>(future, cts));
            }

            var resultArr = new List<R>[batchCount];
            // 处理最后一批
            resultArr[minusOne] = ProcessBatch(batches[minusOne], processor);
            // 处理剩余任务
            ProcessRemainingTasks(taskQueue, futuresMap, resultArr);

            // 排序、过滤null
            return resultArr.Where(list => list != null).SelectMany(list => list).Where(item => item != null).ToList();
        }

        /// <summary>
        /// 处理剩余任务并收集结果
        /// </summary>
        /// <typeparam name="R">结果类型</typeparam>
        /// <param name="taskQueue">任务队列</param>
        /// <param name="futuresMap">异步任务映射</param>
        /// <param name="resultArr">结果存储数组</param>
        private void ProcessRemainingTasks<R>(ConcurrentQueue<IdempotentTask<R>> taskQueue, ConcurrentDictionary<int, CancellableTaskWrapper<R>> futuresMap, List<R>[] resultArr)
        {
            // 主消费未执行任务
            while (taskQueue.TryDequeue(out var task))
            {
                try
                {
                    var call = task.Call();
                    if (call.Effective)
                    {
                        // 取消被主线程执行任务
                        if (futuresMap.TryRemove(task.Index, out var wrapper))
                        {
                            wrapper.CancellationTokenSource.Cancel();
                            wrapper.CancellationTokenSource.Dispose();
                        }
                        // 加入结果集
                        resultArr[task.Index] = call.Result;
                    }
                }
                catch (Exception e)
                {
                    // 不处理异常
                    throw new Exception(e.Message, e);
                }
            }

            foreach (var kvp in futuresMap)
            {
                try
                {
                    var taskResult = kvp.Value.Task.Result;
                    if (taskResult.Effective)
                    {
                        resultArr[kvp.Key] = taskResult.Result;
                    }
                    kvp.Value.CancellationTokenSource.Dispose();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
        }

        /// <summary>
        /// 幂等任务包装类，确保任务只执行一次
        /// </summary>
        private class IdempotentTask<R>
        {
            public int Index { get; }
            private readonly Func<List<R>> _delegate;
            private readonly AtomicBoolean _executed = new AtomicBoolean(false);

            public IdempotentTask(int index, Func<List<R>> @delegate)
            {
                Index = index;
                _delegate = @delegate;
            }

            public TaskResult<R> Call()
            {
                if (_executed.CompareAndSet(false, true))
                {
                    return new TaskResult<R>(_delegate(), true);
                }
                return new TaskResult<R>(null, false);
            }
        }

        /// <summary>
        /// 带取消令牌的任务包装类
        /// </summary>
        private class CancellableTaskWrapper<R>
        {
            public Task<TaskResult<R>> Task { get; }
            public CancellationTokenSource CancellationTokenSource { get; }

            public CancellableTaskWrapper(Task<TaskResult<R>> task, CancellationTokenSource cts)
            {
                Task = task;
                CancellationTokenSource = cts;
            }
        }

        /// <summary>
        /// 结果包装类，标记结果有效性
        /// </summary>
        private class TaskResult<R>
        {
            public List<R> Result { get; }
            public bool Effective { get; }

            public TaskResult(List<R> result, bool effective)
            {
                Result = result;
                Effective = effective;
            }
        }

        /// <summary>
        /// 数据分片方法
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">原始数据</param>
        /// <param name="batchSize">每批次数据量</param>
        /// <returns>分片后的二维集合</returns>
        private static List<List<T>> SplitData<T>(List<T> data, int batchSize)
        {
            int batchCount = (data.Count + batchSize - 1) / batchSize;
            var batches = new List<List<T>>();

            for (int i = 0; i < batchCount; i++)
            {
                int from = i * batchSize;
                int to = Math.Min((i + 1) * batchSize, data.Count);
                batches.Add(data.GetRange(from, to - from));
            }

            return batches;
        }

        /// <summary>
        /// 单批次数据处理
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <typeparam name="R">结果类型</typeparam>
        /// <param name="batch">单批次数据</param>
        /// <param name="processor">处理函数</param>
        /// <returns>处理结果</returns>
        private static List<R> ProcessBatch<T, R>(List<T> batch, Func<T, R> processor)
        {
            return batch.Select(processor).Where(item => item != null).ToList();
        }

        /// <summary>
        /// 处理Warp数组
        /// </summary>
        /// <param name="warps">Warp数组</param>
        /// <returns>Warp集合,此方法返回结果为空的不会被过滤</returns>
        public List<Warp<object>> ProcessByWarp(params Warp<object>[] warps)
        {
            return ProcessByWarp(warps.ToList());
        }

        /// <summary>
        /// 处理Warp集合
        /// </summary>
        /// <param name="warps">Warp集合</param>
        /// <returns>Warp集合,此方法返回结果为空的不会被过滤</returns>
        public List<Warp<object>> ProcessByWarp(List<Warp<object>> warps)
        {
            return Process(warps, 1, warp => warp.Execute());
        }

        /// <summary>
        /// 处理逻辑包装类
        /// </summary>
        /// <typeparam name="R">结果类型</typeparam>
        public class Warp<R>
        {
            private readonly Func<R> _supplier;
            private R _result;

            private Warp(Func<R> supplier)
            {
                _supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));
            }

            /// <summary>
            /// 创建Warp
            /// </summary>
            /// <param name="supplier">执行逻辑</param>
            /// <returns>Warp</returns>
            public static Warp<R> Of(Func<R> supplier)
            {
                return new Warp<R>(supplier);
            }

            /// <summary>
            /// 获取结果
            /// </summary>
            /// <returns>结果</returns>
            public R Get()
            {
                return _result;
            }

            /// <summary>
            /// 执行
            /// </summary>
            /// <returns>this</returns>
            public Warp<R> Execute()
            {
                _result = _supplier();
                return this;
            }
        }

        /// <summary>
        /// 原子布尔值
        /// </summary>
        private class AtomicBoolean
        {
            private int _value;

            public AtomicBoolean(bool initialValue)
            {
                _value = initialValue ? 1 : 0;
            }

            public bool CompareAndSet(bool expected, bool update)
            {
                int expectedInt = expected ? 1 : 0;
                int updateInt = update ? 1 : 0;
                return Interlocked.CompareExchange(ref _value, updateInt, expectedInt) == expectedInt;
            }
        }

        /// <summary>
        /// 并发任务调度器
        /// </summary>
        private class ConcurrentTaskScheduler : TaskScheduler
        {
            private readonly BlockingCollection<Task> _taskQueue;
            private readonly Thread[] _threads;

            public ConcurrentTaskScheduler(int threadCount, NamedThreadFactory threadFactory)
            {
                _taskQueue = new BlockingCollection<Task>();
                _threads = new Thread[threadCount];

                for (int i = 0; i < threadCount; i++)
                {
                    _threads[i] = threadFactory.NewThread(() =>
                    {
                        foreach (var task in _taskQueue.GetConsumingEnumerable())
                        {
                            TryExecuteTask(task);
                        }
                    });
                    _threads[i].Start();
                }
            }

            protected override void QueueTask(Task task)
            {
                _taskQueue.Add(task);
            }

            protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
            {
                return false;
            }

            protected override IEnumerable<Task> GetScheduledTasks()
            {
                return _taskQueue.ToArray();
            }

            public override int MaximumConcurrencyLevel => _threads.Length;

            protected void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _taskQueue.CompleteAdding();
                    foreach (var thread in _threads)
                    {
                        thread.Join();
                    }
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Shutdown();
        }
    }
}