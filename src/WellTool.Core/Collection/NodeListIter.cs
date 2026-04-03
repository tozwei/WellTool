using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// XML 节点列表迭代器
    /// </summary>
    public class NodeListIter : IEnumerable<XmlNode>, IDisposable
    {
        private readonly XmlNodeList _nodeList;
        private int _index = -1;
        private bool _disposed = false;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="nodeList">XML 节点列表</param>
        public NodeListIter(XmlNodeList nodeList)
        {
            _nodeList = nodeList ?? throw new ArgumentNullException(nameof(nodeList));
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<XmlNode> GetEnumerator()
        {
            foreach (XmlNode node in _nodeList)
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 获取当前索引
        /// </summary>
        public int CurrentIndex => _index;

        /// <summary>
        /// 获取节点数量
        /// </summary>
        public int Count => _nodeList.Count;

        /// <summary>
        /// 获取指定索引的节点
        /// </summary>
        public XmlNode this[int index] => _nodeList[index];

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}
