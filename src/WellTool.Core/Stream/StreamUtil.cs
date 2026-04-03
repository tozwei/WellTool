using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WellTool.Core.Streams
{
    /// <summary>
    /// Stream 工具类
    /// </summary>
    public static class StreamUtil
    {
        /// <summary>
        /// 将数组转换为 IEnumerable
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> Of<T>(params T[] array)
        {
            return array ?? Array.Empty<T>();
        }

        /// <summary>
        /// 将 IEnumerable 转换为 IEnumerable
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="iterable">集合</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> Of<T>(IEnumerable<T> iterable)
        {
            return iterable ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// 将 IEnumerable 转换为 List
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="iterable">集合</param>
        /// <returns>List</returns>
        public static List<T> OfList<T>(IEnumerable<T> iterable)
        {
            return iterable?.ToList() ?? new List<T>();
        }

        /// <summary>
        /// 将 IEnumerable 转换为 Array
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="iterable">集合</param>
        /// <returns>Array</returns>
        public static T[] OfArray<T>(IEnumerable<T> iterable)
        {
            return iterable?.ToArray() ?? Array.Empty<T>();
        }

        /// <summary>
        /// 将迭代器转换为列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="iterator">迭代器</param>
        /// <returns>列表</returns>
        public static List<T> ToList<T>(IEnumerator<T> iterator)
        {
            var list = new List<T>();
            while (iterator.MoveNext())
            {
                list.Add(iterator.Current);
            }
            return list;
        }

        /// <summary>
        /// 按行读取文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>行集合</returns>
        public static IEnumerable<string> Of(FileInfo file)
        {
            return Of(file, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 按行读取文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="charset">编码</param>
        /// <returns>行集合</returns>
        public static IEnumerable<string> Of(FileInfo file, System.Text.Encoding charset)
        {
            return File.ReadLines(file.FullName, charset);
        }

        /// <summary>
        /// 按行读取文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>行集合</returns>
        public static IEnumerable<string> OfPath(string path)
        {
            return OfPath(path, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 按行读取文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="charset">编码</param>
        /// <returns>行集合</returns>
        public static IEnumerable<string> OfPath(string path, System.Text.Encoding charset)
        {
            return File.ReadLines(path, charset);
        }

        /// <summary>
        /// 将 Stream 中所有元素以指定分隔符连接成一个字符串
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="stream">集合</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>字符串</returns>
        public static string Join<T>(IEnumerable<T> stream, string delimiter)
        {
            return string.Join(delimiter, stream);
        }

        /// <summary>
        /// 将 Stream 中所有元素以指定分隔符连接成一个字符串
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="stream">集合</param>
        /// <param name="delimiter">分隔符</param>
        /// <param name="toStringFunc">转换为字符串的函数</param>
        /// <returns>字符串</returns>
        public static string Join<T>(IEnumerable<T> stream, string delimiter, Func<T, string> toStringFunc)
        {
            return string.Join(delimiter, stream.Select(toStringFunc));
        }
    }
}
