using System;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 异步工具类
    /// </summary>
    public class AsyncUtil
    {
        /// <summary>
        /// 执行异步任务
        /// </summary>
        /// <param name="action">异步操作</param>
        /// <returns>任务</returns>
        public static Task RunAsync(Action action)
        {
            return Task.Run(action);
        }

        /// <summary>
        /// 执行异步任务
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="func">异步操作</param>
        /// <returns>任务</returns>
        public static Task<T> RunAsync<T>(Func<T> func)
        {
            return Task.Run(func);
        }

        /// <summary>
        /// 执行异步任务（带取消令牌）
        /// </summary>
        /// <param name="action">异步操作</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>任务</returns>
        public static Task RunAsync(Action action, CancellationToken cancellationToken)
        {
            return Task.Run(action, cancellationToken);
        }

        /// <summary>
        /// 执行异步任务（带取消令牌）
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="func">异步操作</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>任务</returns>
        public static Task<T> RunAsync<T>(Func<T> func, CancellationToken cancellationToken)
        {
            return Task.Run(func, cancellationToken);
        }

        /// <summary>
        /// 延迟执行
        /// </summary>
        /// <param name="millisecondsDelay">延迟时间（毫秒）</param>
        /// <returns>任务</returns>
        public static Task Delay(int millisecondsDelay)
        {
            return Task.Delay(millisecondsDelay);
        }

        /// <summary>
        /// 延迟执行
        /// </summary>
        /// <param name="delay">延迟时间</param>
        /// <returns>任务</returns>
        public static Task Delay(TimeSpan delay)
        {
            return Task.Delay(delay);
        }

        /// <summary>
        /// 延迟执行（带取消令牌）
        /// </summary>
        /// <param name="millisecondsDelay">延迟时间（毫秒）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>任务</returns>
        public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
        {
            return Task.Delay(millisecondsDelay, cancellationToken);
        }

        /// <summary>
        /// 延迟执行（带取消令牌）
        /// </summary>
        /// <param name="delay">延迟时间</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>任务</returns>
        public static Task Delay(TimeSpan delay, CancellationToken cancellationToken)
        {
            return Task.Delay(delay, cancellationToken);
        }

        /// <summary>
        /// 等待所有任务完成
        /// </summary>
        /// <param name="tasks">任务数组</param>
        /// <returns>任务</returns>
        public static Task WhenAll(params Task[] tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 等待所有任务完成
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="tasks">任务数组</param>
        /// <returns>任务</returns>
        public static Task<T[]> WhenAll<T>(params Task<T>[] tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 等待任一任务完成
        /// </summary>
        /// <param name="tasks">任务数组</param>
        /// <returns>任务</returns>
        public static Task<Task> WhenAny(params Task[] tasks)
        {
            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// 等待任一任务完成
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="tasks">任务数组</param>
        /// <returns>任务</returns>
        public static Task<Task<T>> WhenAny<T>(params Task<T>[] tasks)
        {
            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// 执行异步任务并捕获异常
        /// </summary>
        /// <param name="action">异步操作</param>
        /// <returns>任务</returns>
        public static async Task RunAsyncWithCatch(Action action)
        {
            try
            {
                await Task.Run(action);
            }
            catch (System.Exception ex)
            {
                // 处理异常
                Console.WriteLine($"Async task error: {ex.Message}");
            }
        }

        /// <summary>
        /// 执行异步任务并捕获异常
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="func">异步操作</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>任务</returns>
        public static async Task<T> RunAsyncWithCatch<T>(Func<T> func, T defaultValue = default)
        {
            try
            {
                return await Task.Run(func);
            }
            catch (System.Exception ex)
            {
                // 处理异常
                Console.WriteLine($"Async task error: {ex.Message}");
                return defaultValue;
            }
        }
    }
}