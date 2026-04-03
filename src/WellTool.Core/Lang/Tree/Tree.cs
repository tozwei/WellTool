using System;
using System.Collections.Generic;

namespace WellTool.Core.Lang.Tree
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode<T>
    {
        /// <summary>
        /// 节点数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public TreeNode<T> Parent { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeNode(T data)
        {
            Data = data;
        }

        /// <summary>
        /// 是否为根节点
        /// </summary>
        public bool IsRoot => Parent == null;

        /// <summary>
        /// 是否为叶子节点
        /// </summary>
        public bool IsLeaf => Children.Count == 0;

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth
        {
            get
            {
                int depth = 0;
                var node = Parent;
                while (node != null)
                {
                    depth++;
                    node = node.Parent;
                }
                return depth;
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        public TreeNode<T> AddChild(T data)
        {
            var child = new TreeNode<T>(data) { Parent = this };
            Children.Add(child);
            return child;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        public void AddChild(TreeNode<T> child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        /// <summary>
        /// 移除子节点
        /// </summary>
        public bool RemoveChild(TreeNode<T> child)
        {
            child.Parent = null;
            return Children.Remove(child);
        }

        /// <summary>
        /// 获取所有祖先
        /// </summary>
        public IEnumerable<TreeNode<T>> GetAncestors()
        {
            var node = Parent;
            while (node != null)
            {
                yield return node;
                node = node.Parent;
            }
        }

        /// <summary>
        /// 获取所有后代
        /// </summary>
        public IEnumerable<TreeNode<T>> GetDescendants()
        {
            var stack = new Stack<TreeNode<T>>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                foreach (var child in node.Children)
                {
                    yield return child;
                    stack.Push(child);
                }
            }
        }

        /// <summary>
        /// 遍历所有节点
        /// </summary>
        public IEnumerable<TreeNode<T>> Traverse()
        {
            yield return this;
            foreach (var child in Children)
            {
                foreach (var descendant in child.Traverse())
                {
                    yield return descendant;
                }
            }
        }
    }

    /// <summary>
    /// 树
    /// </summary>
    public class Tree<T>
    {
        /// <summary>
        /// 根节点
        /// </summary>
        public TreeNode<T> Root { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Tree(T rootData)
        {
            Root = new TreeNode<T>(rootData);
        }

        /// <summary>
        /// 获取所有节点
        /// </summary>
        public IEnumerable<TreeNode<T>> GetAllNodes()
        {
            return Root.Traverse();
        }

        /// <summary>
        /// 获取节点数量
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;
                foreach (var _ in GetAllNodes())
                {
                    count++;
                }
                return count;
            }
        }
    }
}
