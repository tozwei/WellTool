using System;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// 森林 Map
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class ForestMap<K, V>
    {
        private readonly Dictionary<K, Node> _nodes;

        /// <summary>
        /// 节点类
        /// </summary>
        public class Node
        {
            /// <summary>
            /// 键
            /// </summary>
            public K Key { get; }

            /// <summary>
            /// 值
            /// </summary>
            public V Value { get; set; }

            /// <summary>
            /// 父节点
            /// </summary>
            public Node Parent { get; set; }

            /// <summary>
            /// 子节点
            /// </summary>
            public Dictionary<K, Node> Children { get; }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            public Node(K key, V value)
            {
                Key = key;
                Value = value;
                Children = new Dictionary<K, Node>();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ForestMap()
        {
            _nodes = new Dictionary<K, Node>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comparer">键比较器</param>
        public ForestMap(IEqualityComparer<K> comparer)
        {
            _nodes = new Dictionary<K, Node>(comparer);
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="parentKey">父键</param>
        /// <returns>是否添加成功</returns>
        public bool Add(K key, V value, K parentKey = default)
        {
            if (_nodes.ContainsKey(key))
            {
                return false;
            }

            var node = new Node(key, value);
            _nodes[key] = node;

            if (parentKey != null && !EqualityComparer<K>.Default.Equals(parentKey, default))
            {
                if (_nodes.TryGetValue(parentKey, out var parentNode))
                {
                    node.Parent = parentNode;
                    parentNode.Children[key] = node;
                }
            }

            return true;
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>节点</returns>
        public Node Get(K key)
        {
            _nodes.TryGetValue(key, out var node);
            return node;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public V GetValue(K key)
        {
            if (_nodes.TryGetValue(key, out var node))
            {
                return node.Value;
            }
            return default;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否设置成功</returns>
        public bool SetValue(K key, V value)
        {
            if (_nodes.TryGetValue(key, out var node))
            {
                node.Value = value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否移除成功</returns>
        public bool Remove(K key)
        {
            if (!_nodes.TryGetValue(key, out var node))
            {
                return false;
            }

            // 移除子节点
            RemoveChildren(node);

            // 从父节点中移除
            if (node.Parent != null)
            {
                node.Parent.Children.Remove(key);
            }

            // 从根节点中移除
            return _nodes.Remove(key);
        }

        /// <summary>
        /// 移除子节点
        /// </summary>
        /// <param name="node">节点</param>
        private void RemoveChildren(Node node)
        {
            foreach (var childKey in node.Children.Keys)
            {
                _nodes.Remove(childKey);
            }
            node.Children.Clear();
        }

        /// <summary>
        /// 检查是否包含键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否包含</returns>
        public bool ContainsKey(K key)
        {
            return _nodes.ContainsKey(key);
        }

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns>根节点列表</returns>
        public List<Node> GetRoots()
        {
            var roots = new List<Node>();
            foreach (var node in _nodes.Values)
            {
                if (node.Parent == null)
                {
                    roots.Add(node);
                }
            }
            return roots;
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>子节点列表</returns>
        public List<Node> GetChildren(K key)
        {
            if (_nodes.TryGetValue(key, out var node))
            {
                return new List<Node>(node.Children.Values);
            }
            return new List<Node>();
        }

        /// <summary>
        /// 获取父节点
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>父节点</returns>
        public Node GetParent(K key)
        {
            if (_nodes.TryGetValue(key, out var node))
            {
                return node.Parent;
            }
            return null;
        }

        /// <summary>
        /// 获取所有节点
        /// </summary>
        /// <returns>所有节点列表</returns>
        public List<Node> GetAllNodes()
        {
            return new List<Node>(_nodes.Values);
        }

        /// <summary>
        /// 获取节点数量
        /// </summary>
        public int Count => _nodes.Count;

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _nodes.Clear();
        }

        /// <summary>
        /// 遍历所有节点
        /// </summary>
        /// <param name="action">操作</param>
        public void ForEach(Action<Node> action)
        {
            foreach (var node in _nodes.Values)
            {
                action(node);
            }
        }

        /// <summary>
        /// 递归遍历节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="action">操作</param>
        public void Traverse(Node node, Action<Node> action)
        {
            if (node == null)
            {
                return;
            }

            action(node);
            foreach (var child in node.Children.Values)
            {
                Traverse(child, action);
            }
        }

        /// <summary>
        /// 递归遍历所有根节点
        /// </summary>
        /// <param name="action">操作</param>
        public void TraverseAll(Action<Node> action)
        {
            foreach (var root in GetRoots())
            {
                Traverse(root, action);
            }
        }
    }
}