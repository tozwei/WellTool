namespace WellTool.Core.Lang.Tree;

using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Lang.Tree
{
    /// <summary>
    /// 树节点配置
    /// </summary>
    public class TreeNodeConfig
    {
        /// <summary>
        /// 默认配置
        /// </summary>
        public static readonly TreeNodeConfig DEFAULT_CONFIG = new TreeNodeConfig();

        /// <summary>
        /// ID 字段名，默认 "id"
        /// </summary>
        public string IdKey { get; set; } = "id";

        /// <summary>
        /// 父ID字段名，默认 "parentId"
        /// </summary>
        public string ParentIdKey { get; set; } = "parentId";

        /// <summary>
        /// 权重字段名，默认 "weight"
        /// </summary>
        public string WeightKey { get; set; } = "weight";

        /// <summary>
        /// 名称字段名，默认 "name"
        /// </summary>
        public string NameKey { get; set; } = "name";

        /// <summary>
        /// 子节点列表字段名，默认 "children"
        /// </summary>
        public string ChildrenKey { get; set; } = "children";

        /// <summary>
        /// 构造
        /// </summary>
        public TreeNodeConfig()
        {
        }
    }

    /// <summary>
    /// 树节点接口
    /// </summary>
    /// <typeparam name="T">ID类型</typeparam>
    public interface Node<T>
    {
        /// <summary>
        /// 获取节点ID
        /// </summary>
        T GetId();

        /// <summary>
        /// 获取父节点ID
        /// </summary>
        T? GetParentId();

        /// <summary>
        /// 获取权重
        /// </summary>
        int GetWeight();

        /// <summary>
        /// 获取名称
        /// </summary>
        string GetName();

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        List<Tree<T>> GetChildren();
    }
}
