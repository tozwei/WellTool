using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 迭代器工具类
    /// </summary>
    public static class IterUtil
    {
        /// <summary>
        /// 获取一个空的迭代器
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <returns>空迭代器</returns>
        public static IEnumerator<T> Empty<T>()
        {
            yield break;
        }

        /// <summary>
        /// 将迭代器转换为列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <returns>列表</returns>
        public static List<T> ToList<T>(IEnumerator<T> enumerator)
        {
            var list = new List<T>();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Current);
            }
            return list;
        }

        /// <summary>
        /// 将迭代器转换为数组
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <returns>数组</returns>
        public static T[] ToArray<T>(IEnumerator<T> enumerator)
        {
            return ToList(enumerator).ToArray();
        }

        /// <summary>
        /// 遍历迭代器
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <param name="action">操作</param>
        public static void ForEach<T>(IEnumerator<T> enumerator, Action<T> action)
        {
            while (enumerator.MoveNext())
            {
                action(enumerator.Current);
            }
        }

        /// <summary>
        /// 创建一个迭代器链
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="iterators">迭代器数组</param>
        /// <returns>迭代器链</returns>
        public static IEnumerator<T> Chain<T>(params IEnumerator<T>[] iterators)
        {
            foreach (var iterator in iterators)
            {
                while (iterator.MoveNext())
                {
                    yield return iterator.Current;
                }
            }
        }

        /// <summary>
        /// 过滤迭代器
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <param name="predicate">过滤条件</param>
        /// <returns>过滤后的迭代器</returns>
        public static IEnumerator<T> Filter<T>(IEnumerator<T> enumerator, Func<T, bool> predicate)
        {
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    yield return current;
                }
            }
        }

        /// <summary>
        /// 转换迭代器
        /// </summary>
        /// <typeparam name="T">输入类型</typeparam>
        /// <typeparam name="R">输出类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <param name="func">转换函数</param>
        /// <returns>转换后的迭代器</returns>
        public static IEnumerator<R> Map<T, R>(IEnumerator<T> enumerator, Func<T, R> func)
        {
            while (enumerator.MoveNext())
            {
                yield return func(enumerator.Current);
            }
        }

        /// <summary>
        /// 限制迭代器长度
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <param name="limit">限制数量</param>
        /// <returns>限制后的迭代器</returns>
        public static IEnumerator<T> Limit<T>(IEnumerator<T> enumerator, int limit)
        {
            int count = 0;
            while (enumerator.MoveNext() && count < limit)
            {
                yield return enumerator.Current;
                count++;
            }
        }

        /// <summary>
        /// 跳过指定数量的元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <param name="skip">跳过数量</param>
        /// <returns>跳过指定数量后的迭代器</returns>
        public static IEnumerator<T> Skip<T>(IEnumerator<T> enumerator, int skip)
        {
            int count = 0;
            while (enumerator.MoveNext() && count < skip)
            {
                count++;
            }
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// 查找第一个满足条件的元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <param name="predicate">条件</param>
        /// <returns>第一个满足条件的元素，不存在则返回默认值</returns>
        public static T FindFirst<T>(IEnumerator<T> enumerator, Func<T, bool> predicate)
        {
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    return current;
                }
            }
            return default;
        }

        /// <summary>
        /// 检查是否包含满足条件的元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <param name="predicate">条件</param>
        /// <returns>是否包含</returns>
        public static bool Any<T>(IEnumerator<T> enumerator, Func<T, bool> predicate)
        {
            return FindFirst(enumerator, predicate) != null;
        }

        /// <summary>
        /// 计算满足条件的元素数量
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerator">迭代器</param>
        /// <param name="predicate">条件</param>
        /// <returns>数量</returns>
        public static int Count<T>(IEnumerator<T> enumerator, Func<T, bool> predicate)
        {
            int count = 0;
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    count++;
                }
            }
            return count;
        }
    }
}