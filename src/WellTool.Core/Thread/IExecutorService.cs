using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Thread
{
    /// <summary>
    /// 执行器服务接口
    /// </summary>
    public interface IExecutorService
    {
        /// <summary>
        /// 执行指定的任务
        /// </summary>
        /// <param name="command">要执行的任务</param>
        void Execute(Action command);

        /// <summary>
        /// 关闭执行器
        /// </summary>
        void Shutdown();

        /// <summary>
        /// 立即关闭执行器并返回未执行的任务
        /// </summary>
        /// <returns>未执行的任务列表</returns>
        List<Action> ShutdownNow();

        /// <summary>
        /// 检查执行器是否已关闭
        /// </summary>
        bool IsShutdown { get; }

        /// <summary>
        /// 检查执行器是否已终止
        /// </summary>
        bool IsTerminated { get; }

        /// <summary>
        /// 等待执行器终止
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <param name="unit">时间单位</param>
        /// <returns>是否在超时前终止</returns>
        bool AwaitTermination(long timeout, TimeUnit unit);

        /// <summary>
        /// 提交一个任务并返回一个表示该任务的Future
        /// </summary>
        /// <param name="task">要执行的任务</param>
        /// <returns>表示任务结果的Future</returns>
        Task Submit(Action task);

        /// <summary>
        /// 提交一个任务并返回一个表示该任务的Future
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="task">要执行的任务</param>
        /// <returns>表示任务结果的Future</returns>
        Task<T> Submit<T>(Func<T> task);

        /// <summary>
        /// 提交一个任务并返回一个表示该任务的Future
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="task">要执行的任务</param>
        /// <param name="result">任务结果</param>
        /// <returns>表示任务结果的Future</returns>
        Task<T> Submit<T>(Action task, T result);

        /// <summary>
        /// 执行所有任务并返回任务结果
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="tasks">任务集合</param>
        /// <returns>任务结果集合</returns>
        List<Task<T>> InvokeAll<T>(ICollection<Func<T>> tasks);

        /// <summary>
        /// 执行所有任务并返回任务结果
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="tasks">任务集合</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="unit">时间单位</param>
        /// <returns>任务结果集合</returns>
        List<Task<T>> InvokeAll<T>(ICollection<Func<T>> tasks, long timeout, TimeUnit unit);

        /// <summary>
        /// 执行所有任务并返回第一个完成的任务结果
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="tasks">任务集合</param>
        /// <returns>第一个完成的任务结果</returns>
        T InvokeAny<T>(ICollection<Func<T>> tasks);

        /// <summary>
        /// 执行所有任务并返回第一个完成的任务结果
        /// </summary>
        /// <typeparam name="T">任务结果类型</typeparam>
        /// <param name="tasks">任务集合</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="unit">时间单位</param>
        /// <returns>第一个完成的任务结果</returns>
        T InvokeAny<T>(ICollection<Func<T>> tasks, long timeout, TimeUnit unit);
    }
}