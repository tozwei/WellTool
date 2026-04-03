using System;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// 树形结构的Map，使用森林（多棵树）结构存储数据
    /// </summary>
    public class LinkedForestMap<V> : Dictionary<LinkedForestMap<V>.TreeNode, LinkedForestMap<V>.TreeNode>
    {
        private readonly TreeNode _root;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LinkedForestMap()
        {
            _root = new TreeNode(null, "__ROOT__");
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        public void Add(string path, V value)
        {
            var parts = path.Split('.');
            TreeNode current = _root;

            for (int i = 0; i < parts.Length; i++)
            {
                var key = parts[i];
                var isLeaf = i == parts.Length - 1;
                var child = current.GetOrCreateChild(key, isLeaf ? value : default);
                
                if (isLeaf)
                {
                    child.Value = value;
                }
                current = child;
            }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public V Get(string path)
        {
            var node = FindNode(path);
            return node?.Value ?? default;
        }

        /// <summary>
        /// 是否包含路径
        /// </summary>
        public bool ContainsPath(string path)
        {
            return FindNode(path) != null;
        }

        /// <summary>
        /// 获取根节点
        /// </summary>
        public TreeNode GetRoot()
        {
            return _root;
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        private TreeNode FindNode(string path)
        {
            var parts = path.Split('.');
            TreeNode current = _root;

            foreach (var part in parts)
            {
                if (current == null || !current.Children.TryGetValue(part, out current))
                {
                    return null;
                }
            }

            return current;
        }

        /// <summary>
        /// 树节点
        /// </summary>
        public class TreeNode
        {
            public string Key { get; }
            public V Value { get; set; }
            public TreeNode Parent { get; }
            public Dictionary<string, TreeNode> Children { get; } = new Dictionary<string, TreeNode>();
            public bool IsLeaf => Children.Count == 0;

            public TreeNode(TreeNode parent, string key, V value = default)
            {
                Parent = parent;
                Key = key;
                Value = value;
            }

            public TreeNode GetOrCreateChild(string key, V value = default)
            {
                if (!Children.TryGetValue(key, out var child))
                {
                    child = new TreeNode(this, key, value);
                    Children[key] = child;
                }
                return child;
            }

            public string GetPath()
            {
                if (Parent == null)
                {
                    return Key;
                }
                return Parent.GetPath() + "." + Key;
            }
        }
    }
}
