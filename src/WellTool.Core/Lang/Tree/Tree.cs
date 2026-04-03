namespace WellTool.Core.Lang.Tree;

using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Lang.Tree
{
    /// <summary>
    /// 树节点
    /// </summary>
    /// <typeparam name="T">ID类型</typeparam>
    public class Tree<T> : Dictionary<string, object>, Node<T>
    {
        private static readonly long SerialVersionUID = 1L;

        private readonly TreeNodeConfig _config;
        private Tree<T>? _parent;
        private readonly List<Tree<T>> _children = new List<Tree<T>>();

        /// <summary>
        /// 构造
        /// </summary>
        public Tree() : this(null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">节点配置</param>
        public Tree(TreeNodeConfig? config)
        {
            _config = config ?? TreeNodeConfig.DEFAULT_CONFIG;
        }

        /// <summary>
        /// 获取节点配置
        /// </summary>
        public TreeNodeConfig GetConfig() => _config;

        /// <summary>
        /// 获取父节点
        /// </summary>
        public Tree<T>? GetParent() => _parent;

        /// <summary>
        /// 设置父节点
        /// </summary>
        public void SetParent(Tree<T>? parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// 获取ID对应的节点
        /// </summary>
        public Tree<T>? GetNode(T id)
        {
            return TreeUtil.GetNode(this, id);
        }

        /// <summary>
        /// 获取所有父节点名称列表
        /// </summary>
        public List<string> GetParentNames()
        {
            var result = new List<string>();
            var parent = _parent;
            while (parent != null)
            {
                result.Insert(0, parent.GetName());
                parent = parent.GetParent();
            }
            return result;
        }

        /// <summary>
        /// 获取所有父节点ID列表
        /// </summary>
        public List<T> GetParentIds()
        {
            var result = new List<T>();
            var parent = _parent;
            while (parent != null)
            {
                result.Insert(0, parent.GetId());
                parent = parent.GetParent();
            }
            return result;
        }

        /// <summary>
        /// 获取节点ID
        /// </summary>
        public T GetId()
        {
            if (TryGetValue(_config.IdKey, out var value) && value is T id)
            {
                return id;
            }
            return default!;
        }

        /// <summary>
        /// 设置节点ID
        /// </summary>
        public void SetId(T id)
        {
            this[_config.IdKey] = id!;
        }

        /// <summary>
        /// 获取父节点ID
        /// </summary>
        public T? GetParentId()
        {
            if (TryGetValue(_config.ParentIdKey, out var value) && value is T parentId)
            {
                return parentId;
            }
            return default;
        }

        /// <summary>
        /// 设置父节点ID
        /// </summary>
        public void SetParentId(T parentId)
        {
            this[_config.ParentIdKey] = parentId!;
        }

        /// <summary>
        /// 获取权重
        /// </summary>
        public int GetWeight()
        {
            if (TryGetValue(_config.WeightKey, out var value))
            {
                if (value is int i) return i;
                if (value is long l) return (int)l;
                if (value is string s && int.TryParse(s, out int parsed)) return parsed;
            }
            return 0;
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        public string GetName()
        {
            if (TryGetValue(_config.NameKey, out var value) && value != null)
            {
                return value.ToString() ?? string.Empty;
            }
            return string.Empty;
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        public void SetName(string name)
        {
            this[_config.NameKey] = name;
        }

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        public List<Tree<T>> GetChildren()
        {
            return _children;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        public void AddChild(Tree<T> child)
        {
            child.SetParent(this);
            _children.Add(child);
        }

        /// <summary>
        /// 是否为根节点
        /// </summary>
        public bool IsRoot()
        {
            return _parent == null;
        }

        /// <summary>
        /// 是否为叶子节点
        /// </summary>
        public bool IsLeaf()
        {
            return _children.Count == 0;
        }

        /// <summary>
        /// 获取节点层级（根节点为0）
        /// </summary>
        public int GetLevel()
        {
            int level = 0;
            var parent = _parent;
            while (parent != null)
            {
                level++;
                parent = parent.GetParent();
            }
            return level;
        }

        /// <summary>
        /// 复制节点
        /// </summary>
        public Tree<T> Clone()
        {
            var clone = new Tree<T>(_config);
            foreach (var kvp in this)
            {
                clone[kvp.Key] = kvp.Value;
            }
            clone._children.AddRange(_children.Select(c => c.Clone()));
            return clone;
        }
    }
}
