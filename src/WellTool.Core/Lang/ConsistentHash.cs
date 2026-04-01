using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 一致性哈希
    /// </summary>
    /// <typeparam name="T">节点类型</typeparam>
    public class ConsistentHash<T>
    {
        private readonly SortedDictionary<long, T> _hashRing = new SortedDictionary<long, T>();
        private readonly int _virtualNodes;
        private readonly Func<string, long> _hashFunction;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="virtualNodes">虚拟节点数</param>
        public ConsistentHash(int virtualNodes = 100)
            : this(virtualNodes, DefaultHashFunction)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="virtualNodes">虚拟节点数</param>
        /// <param name="hashFunction">哈希函数</param>
        public ConsistentHash(int virtualNodes, Func<string, long> hashFunction)
        {
            _virtualNodes = virtualNodes;
            _hashFunction = hashFunction;
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">节点</param>
        public void Add(T node)
        {
            for (int i = 0; i < _virtualNodes; i++)
            {
                var key = _hashFunction($"{node.ToString()}_{i}");
                _hashRing[key] = node;
            }
        }

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="node">节点</param>
        public void Remove(T node)
        {
            for (int i = 0; i < _virtualNodes; i++)
            {
                var key = _hashFunction($"{node.ToString()}_{i}");
                _hashRing.Remove(key);
            }
        }

        /// <summary>
        /// 获取对应键的节点
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>节点</returns>
        public T Get(string key)
        {
            if (_hashRing.Count == 0)
            {
                throw new InvalidOperationException("No nodes added to consistent hash");
            }

            var hash = _hashFunction(key);
            var keys = _hashRing.Keys;

            foreach (var ringKey in keys)
            {
                if (ringKey >= hash)
                {
                    return _hashRing[ringKey];
                }
            }

            // 如果没有找到比当前哈希值大的节点，返回第一个节点
            return _hashRing[keys.First()];
        }

        /// <summary>
        /// 默认哈希函数
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>哈希值</returns>
        private static long DefaultHashFunction(string input)
        {
            using var md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToInt64(bytes, 0);
        }
    }
}