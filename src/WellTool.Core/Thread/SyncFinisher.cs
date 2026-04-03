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
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 线程同步结束器<br>
    /// 在完成一组正在其他线程中执行的操作之前，它允许一个或多个线程一直等待。
    /// </summary>
    public class SyncFinisher : IDisposable
    {
        private readonly HashSet<Worker> _workers;
        private readonly int _threadSize;
        private TaskFactory _taskFactory;

        private bool _isBeginAtSameTime;
        /// <summary>
        /// 启动同步器，用于保证所有worker线程同时开始
        /// </summary>
        private Barrier _beginBarrier;
        /// <summary>
        /// 结束同步器，用于等待所有worker线程同时结束
        /// </summary>
        private CountdownEvent _endEvent;
        /// <summary>
        /// 异常处理
        /// </summary>
        private UnhandledExceptionEventHandler _exceptionHandler;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="threadSize">线程数</param>
        public SyncFinisher(int threadSize)
        {
            _threadSize = threadSize;
            _workers = new HashSet<Worker>();
        }

        /// <summary>
        /// 设置自定义任务工厂，默认为{@link ExecutorBuilder}创建的任务工厂
        /// </summary>
        /// <param name="taskFactory">任务工厂</param>
        /// <returns>this</returns>
        public SyncFinisher SetTaskFactory(TaskFactory taskFactory)
        {
            _taskFactory = taskFactory;
            return this;
        }

        /// <summary>
        /// 设置是否所有worker线程同时开始
        /// </summary>
        /// <param name="isBeginAtSameTime">是否所有worker线程同时开始</param>
        /// <returns>this</returns>
        public SyncFinisher SetBeginAtSameTime(bool isBeginAtSameTime)
        {
            _isBeginAtSameTime = isBeginAtSameTime;
            return this;
        }

        /// <summary>
        /// 设置异常处理
        /// </summary>
        /// <param name="exceptionHandler">异常处理器</param>
        /// <returns>this</returns>
        public SyncFinisher SetExceptionHandler(UnhandledExceptionEventHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
            return this;
        }

        /// <summary>
        /// 增加定义的线程数同等数量的worker
        /// </summary>
        /// <param name="action">工作线程</param>
        /// <returns>this</returns>
        public SyncFinisher AddRepeatWorker(Action action)
        {
            for (int i = 0; i < _threadSize; i++)
            {
                AddWorker(new Worker(() => action()));
            }
            return this;
        }

        /// <summary>
        /// 增加工作线程
        /// </summary>
        /// <param name="action">工作线程</param>
        /// <returns>this</returns>
        public SyncFinisher AddWorker(Action action)
        {
            return AddWorker(new Worker(action));
        }

        /// <summary>
        /// 增加工作线程
        /// </summary>
        /// <param name="worker">工作线程</param>
        /// <returns>this</returns>
        public SyncFinisher AddWorker(Worker worker)
        {
            lock (_workers)
            {
                _workers.Add(worker);
            }
            return this;
        }

        /// <summary>
        /// 开始工作<br>
        /// 执行此方法后如果不再重复使用此对象，需调用{@link #Stop()}关闭回收资源。
        /// </summary>
        public void Start()
        {
            Start(true);
        }

        /// <summary>
        /// 开始工作<br>
        /// 执行此方法后如果不再重复使用此对象，需调用{@link #Stop()}关闭回收资源。
        /// </summary>
        /// <param name="sync">是否阻塞等待</param>
        public void Start(bool sync)
        {
            int workerCount = _workers.Count;
            _endEvent = new CountdownEvent(workerCount);

            if (_isBeginAtSameTime)
            {
                _beginBarrier = new Barrier(workerCount);
            }

            if (_taskFactory == null)
            {
                _taskFactory = BuildTaskFactory();
            }

            var tasks = new List<Task>();
            foreach (var worker in _workers)
            {
                tasks.Add(_taskFactory.StartNew(() => {
                    try
                    {
                        if (_isBeginAtSameTime)
                        {
                            _beginBarrier.SignalAndWait();
                        }
                        worker.Work();
                    }
                    catch (System.Exception ex)
                    {
                        _exceptionHandler?.Invoke(this, new UnhandledExceptionEventArgs(ex, false));
                    }
                    finally
                    {
                        _endEvent.Signal();
                    }
                }));
            }

            if (sync)
            {
                _endEvent.Wait();
            }
        }

        /// <summary>
        /// 结束任务工厂。此方法执行两种情况：
        /// <ol>
        ///     <li>执行Start(true)后，调用此方法结束任务工厂回收资源</li>
        ///     <li>执行Start(false)后，用户自行判断结束点执行此方法</li>
        /// </ol>
        /// </summary>
        public void Stop()
        {
            // 在C#中，TaskFactory不需要手动关闭
            _taskFactory = null;
            ClearWorker();
        }

        /// <summary>
        /// 清空工作线程对象
        /// </summary>
        public void ClearWorker()
        {
            lock (_workers)
            {
                _workers.Clear();
            }
        }

        /// <summary>
        /// 剩余任务数
        /// </summary>
        /// <returns>剩余任务数</returns>
        public long Count()
        {
            return _endEvent?.CurrentCount ?? 0;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        /// <summary>
        /// 工作者，为一个线程
        /// </summary>
        public class Worker
        {
            private readonly Action _action;

            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="action">任务内容</param>
            public Worker(Action action)
            {
                _action = action;
            }

            /// <summary>
            /// 任务内容
            /// </summary>
            public void Work()
            {
                _action();
            }
        }

        /// <summary>
        /// 构建任务工厂，加入了自定义的异常处理
        /// </summary>
        /// <returns>TaskFactory</returns>
        private TaskFactory BuildTaskFactory()
        {
            return ExecutorBuilder.Create()
                .SetCorePoolSize(_threadSize)
                .Build();
        }
    }
}