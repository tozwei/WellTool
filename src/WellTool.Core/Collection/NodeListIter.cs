using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 节点列表迭代器
    /// </summary>
    public class NodeListIter<T> : IEnumerable<T>, IEnumerator<T>
    {
        private readonly List<T> _nodes;
        private int _index = -1;

        /// <summary>
        /// 构造函数
        /// </summary>
        public NodeListIter(params T[] nodes)
        {
            _nodes = new List<T>(nodes);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NodeListIter(IEnumerable<T> nodes)
        {
            _nodes = new List<T>(nodes);
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        public T Current => _index >= 0 && _index < _nodes.Count ? _nodes[_index] : default;

        object IEnumerator.Current => Current;

        /// <summary>
        /// 获取枚举器
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 移动到下一个元素
        /// </summary>
        public bool MoveNext()
        {
            if (_index < _nodes.Count)
            {
                _index++;
            }
            return _index < _nodes.Count;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _index = -1;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _nodes.Clear();
        }

        /// <summary>
        /// 获取节点数量
        /// </summary>
        public int Count => _nodes.Count;

        /// <summary>
        /// 获取指定索引的节点
        /// </summary>
        public T this[int index] => _nodes[index];
    }

    /// <summary>
    /// 节点列表迭代器扩展
    /// </summary>
    public static class NodeListIterExtensions
    {
        /// <summary>
        /// 创建节点列表迭代器
        /// </summary>
        public static NodeListIter<T> ToNodeList<T>(params T[] nodes)
        {
            return new NodeListIter<T>(nodes);
        }

        /// <summary>
        /// 创建节点列表迭代器
        /// </summary>
        public static NodeListIter<T> ToNodeList<T>(this IEnumerable<T> source)
        {
            return new NodeListIter<T>(source);
        }
    }
}
