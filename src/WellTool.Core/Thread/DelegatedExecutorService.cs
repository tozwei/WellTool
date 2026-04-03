using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WellTool.Core.Lang;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// ExecutorService代理
    /// </summary>
    public class DelegatedExecutorService : IExecutorService
    {
        private readonly IExecutorService _executor;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="executor"><see cref="IExecutorService"/></param>
        public DelegatedExecutorService(IExecutorService executor)
        {
            Assert.NotNull(executor, "executor must be not null!");
            _executor = executor;
        }

        /// <summary>
        /// 执行指定的任务
        /// </summary>
        /// <param name="command">要执行的任务</param>
        public void Execute(System.Action command)
        {
            _executor.Execute(command);
        }

        /// <summary>
        /// 关闭执行器
        /// </summary>
        public void Shutdown()
        {
            _executor.Shutdown();
        }

        /// <summary>
        /// 立即关闭执行器并返回未执行的任务
        /// </summary>
        /// <returns>未执行的任务列表</returns>
        public List<System.Action> ShutdownNow()
        {
            return _executor.ShutdownNow();
        }

        /// <summary>
        /// 检查执行器是否已关闭
        /// </summary>
        public bool IsShutdown => _executor.IsShutdown;

        /// <summary>
        /// 检查执行器是否已终止
        /// </summary>
        public bool IsTerminated => _executor.IsTerminated;

        /// <summary>
        /// 等待执行器终止
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <param name="unit">时间单位</param>
        /// <returns>是否在超时前终止</returns>
        public bool AwaitTermination(long timeout, TimeUnit unit)
        {
            return _executor.AwaitTermination(timeout, unit);
        }

        /// <summary>
        /// 提交一个任务并返回一个表示该任务的Future
        /// </summary>
        /// <param name="task">要执行的任务</param>
        /// <returns>表示任务结果的Future</returns>
        public Task Submit(System.Action task)
        {
            return _executor.Submit(task);
        }

        /// <summary>
        /// 提交一个任务并返回一个表示该任务的Future
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="task">要执行的任务</param>
        /// <returns>表示任务结果的Future</returns>
        public Task<T> Submit<T>(System.Func<T> task)
        {
            return _executor.Submit(task);
        }

        /// <summary>
        /// 提交一个任务并返回一个表示该任务的Future
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="task">要执行的任务</param>
        /// <param name="result">任务结果</param>
        /// <returns>表示任务结果的Future</returns>
        public Task<T> Submit<T>(System.Action task, T result)
        {
            return _executor.Submit(task, result);
        }

        /// <summary>
        /// 执行所有任务并返回任务结果
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="tasks">任务集合</param>
        /// <returns>任务结果集合</returns>
        public List<Task<T>> InvokeAll<T>(ICollection<System.Func<T>> tasks)
        {
            return _executor.InvokeAll(tasks);
        }

        /// <summary>
        /// 执行所有任务并返回任务结果
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="tasks">任务集合</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="unit">时间单位</param>
        /// <returns>任务结果集合</returns>
        public List<Task<T>> InvokeAll<T>(ICollection<System.Func<T>> tasks, long timeout, TimeUnit unit)
        {
            return _executor.InvokeAll(tasks, timeout, unit);
        }

        /// <summary>
        /// 执行所有任务并返回第一个完成的任务结果
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="tasks">任务集合</param>
        /// <returns>第一个完成的任务结果</returns>
        public T InvokeAny<T>(ICollection<System.Func<T>> tasks)
        {
            return _executor.InvokeAny(tasks);
        }

        /// <summary>
        /// 执行所有任务并返回第一个完成的任务结果
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="tasks">任务集合</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="unit">时间单位</param>
        /// <returns>第一个完成的任务结果</returns>
        public T InvokeAny<T>(ICollection<System.Func<T>> tasks, long timeout, TimeUnit unit)
        {
            return _executor.InvokeAny(tasks, timeout, unit);
        }
    }
}