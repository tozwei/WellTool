using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Lang.Tree
{
    /// <summary>
    /// 树工具类
    /// </summary>
    public class TreeUtil
    {
        /// <summary>
        /// 根据 ID 获取节点
        /// </summary>
        public static Tree<T>? GetNode<T>(Tree<T> root, T id)
        {
            if (root == null) return null;

            var queue = new Queue<Tree<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (Equals(node.GetId(), id))
                {
                    return node;
                }

                foreach (var child in node.GetChildren())
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        /// <summary>
        /// 构建树
        /// </summary>
        public static List<Tree<T>> Build<T>(List<Node<T>> list, T? rootId = default)
        {
            var nodeMap = new Dictionary<T, Tree<T>>();
            var result = new List<Tree<T>>();

            // 创建所有节点
            foreach (var item in list)
            {
                var tree = new Tree<T>(item);
                nodeMap[item.GetId()] = tree;
            }

            // 构建树关系
            foreach (var item in list)
            {
                var tree = nodeMap[item.GetId()];
                var parentId = item.GetParentId();

                if (parentId == null || Equals(parentId, default(T)) || !nodeMap.ContainsKey(parentId))
                {
                    result.Add(tree);
                }
                else
                {
                    nodeMap[parentId].AddChild(tree);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取所有叶子节点
        /// </summary>
        public static List<Tree<T>> GetLeafs<T>(Tree<T> root)
        {
            var result = new List<Tree<T>>();
            CollectLeafs(root, result);
            return result;
        }

        private static void CollectLeafs<T>(Tree<T> node, List<Tree<T>> result)
        {
            var children = node.GetChildren();
            if (children == null || children.Count == 0)
            {
                result.Add(node);
            }
            else
            {
                foreach (var child in children)
                {
                    CollectLeafs(child, result);
                }
            }
        }

        /// <summary>
        /// 获取树的深度
        /// </summary>
        public static int GetDepth<T>(Tree<T> root)
        {
            if (root == null) return 0;
            return GetDepthRecursive(root, 1);
        }

        private static int GetDepthRecursive<T>(Tree<T> node, int currentDepth)
        {
            var children = node.GetChildren();
            if (children == null || children.Count == 0)
            {
                return currentDepth;
            }

            int maxDepth = currentDepth;
            foreach (var child in children)
            {
                int childDepth = GetDepthRecursive(child, currentDepth + 1);
                if (childDepth > maxDepth)
                {
                    maxDepth = childDepth;
                }
            }

            return maxDepth;
        }

        /// <summary>
        /// 递归遍历树
        /// </summary>
        public static void Walk<T>(Tree<T> root, System.Action<Tree<T>> visitor)
        {
            if (root == null) return;
            visitor(root);
            foreach (var child in root.GetChildren())
            {
                Walk(child, visitor);
            }
        }
    }
}
