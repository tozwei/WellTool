using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 集合流工具类
    /// </summary>
    public class CollStreamUtil
    {
        /// <summary>
        /// 并行处理集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="action">处理操作</param>
        public static void ParallelForEach<T>(IEnumerable<T> collection, Action<T> action)
        {
            Parallel.ForEach(collection, action);
        }

        /// <summary>
        /// 并行处理集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="action">处理操作</param>
        /// <returns>任务</returns>
        public static Task ParallelForEachAsync<T>(IEnumerable<T> collection, Func<T, Task> action)
        {
            return Task.WhenAll(collection.Select(action));
        }

        /// <summary>
        /// 流式处理集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="func">处理函数</param>
        /// <returns>处理后的集合</returns>
        public static IEnumerable<R> Stream<T, R>(IEnumerable<T> collection, Func<T, R> func)
        {
            return collection.Select(func);
        }

        /// <summary>
        /// 流式处理集合（异步）
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="func">处理函数</param>
        /// <returns>处理后的集合</returns>
        public static async Task<IEnumerable<R>> StreamAsync<T, R>(IEnumerable<T> collection, Func<T, Task<R>> func)
        {
            var tasks = collection.Select(func);
            return await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 过滤集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">过滤条件</param>
        /// <returns>过滤后的集合</returns>
        public static IEnumerable<T> Filter<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Where(predicate);
        }

        /// <summary>
        /// 过滤集合（异步）
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">过滤条件</param>
        /// <returns>过滤后的集合</returns>
        public static async Task<IEnumerable<T>> FilterAsync<T>(IEnumerable<T> collection, Func<T, Task<bool>> predicate)
        {
            var tasks = collection.Select(async item => (item, await predicate(item)));
            var results = await Task.WhenAll(tasks);
            return results.Where(result => result.Item2).Select(result => result.Item1);
        }

        /// <summary>
        /// 聚合集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="seed">初始值</param>
        /// <param name="func">聚合函数</param>
        /// <returns>聚合结果</returns>
        public static R Reduce<T, R>(IEnumerable<T> collection, R seed, Func<R, T, R> func)
        {
            return collection.Aggregate(seed, func);
        }

        /// <summary>
        /// 聚合集合（异步）
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="seed">初始值</param>
        /// <param name="func">聚合函数</param>
        /// <returns>聚合结果</returns>
        public static async Task<R> ReduceAsync<T, R>(IEnumerable<T> collection, R seed, Func<R, T, Task<R>> func)
        {
            var result = seed;
            foreach (var item in collection)
            {
                result = await func(result, item);
            }
            return result;
        }

        /// <summary>
        /// 批量处理集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="batchSize">批次大小</param>
        /// <param name="action">处理操作</param>
        public static void BatchForEach<T>(IEnumerable<T> collection, int batchSize, Action<IEnumerable<T>> action)
        {
            var batch = new List<T>(batchSize);
            foreach (var item in collection)
            {
                batch.Add(item);
                if (batch.Count >= batchSize)
                {
                    action(batch);
                    batch.Clear();
                }
            }
            if (batch.Count > 0)
            {
                action(batch);
            }
        }

        /// <summary>
        /// 批量处理集合（异步）
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="batchSize">批次大小</param>
        /// <param name="action">处理操作</param>
        /// <returns>任务</returns>
        public static async Task BatchForEachAsync<T>(IEnumerable<T> collection, int batchSize, Func<IEnumerable<T>, Task> action)
        {
            var batch = new List<T>(batchSize);
            foreach (var item in collection)
            {
                batch.Add(item);
                if (batch.Count >= batchSize)
                {
                    await action(batch);
                    batch.Clear();
                }
            }
            if (batch.Count > 0)
            {
                await action(batch);
            }
        }
    }
}