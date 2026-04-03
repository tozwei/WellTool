namespace WellTool.Core.Lang.Tree;

/// <summary>
/// 树节点接口
/// </summary>
/// <typeparam name="T">节点数据ID类型</typeparam>
/// <typeparam name="V">节点值类型</typeparam>
public interface ITreeNode<T, V>
{
	/// <summary>
	/// 获取节点ID
	/// </summary>
	T Id { get; }

	/// <summary>
	/// 获取父节点ID
	/// </summary>
	T? ParentId { get; }

	/// <summary>
	/// 获取节点值
	/// </summary>
	V Value { get; }

	/// <summary>
	/// 获取子节点列表
	/// </summary>
	IList<ITreeNode<T, V>> Children { get; }
}

/// <summary>
/// 树节点实现
/// </summary>
/// <typeparam name="T">节点数据ID类型</typeparam>
/// <typeparam name="V">节点值类型</typeparam>
public class TreeNode<T, V> : ITreeNode<T, V>
{
	/// <inheritdoc />
	public T Id { get; set; } = default!;

	/// <inheritdoc />
	public T? ParentId { get; set; }

	/// <inheritdoc />
	public V Value { get; set; } = default!;

	/// <inheritdoc />
	public IList<ITreeNode<T, V>> Children { get; set; } = new List<ITreeNode<T, V>>();

	/// <summary>
	/// 构造
	/// </summary>
	public TreeNode() { }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id">节点ID</param>
	/// <param name="parentId">父节点ID</param>
	/// <param name="value">节点值</param>
	public TreeNode(T id, T? parentId, V value)
	{
		Id = id;
		ParentId = parentId;
		Value = value;
	}
}

/// <summary>
/// 树结构工具类
/// </summary>
public static class TreeUtil
{
	/// <summary>
	/// 将扁平结构转换为树结构
	/// </summary>
	/// <typeparam name="T">节点ID类型</typeparam>
	/// <typeparam name="V">节点值类型</typeparam>
	/// <param name="nodes">扁平节点列表</param>
	/// <param name="rootId">根节点ID（null表示没有父节点的为根节点）</param>
	/// <returns>树结构的根节点列表</returns>
	public static IList<ITreeNode<T, V>> BuildTree<T, V>(IEnumerable<ITreeNode<T, V>> nodes, T? rootId = default)
	{
		var nodeList = nodes.ToList();
		var lookup = nodeList.ToLookup(n => n.ParentId);

		IList<ITreeNode<T, V>> BuildChildren(T? parentId)
		{
			return lookup[parentId]
				.Select(n =>
				{
					if (n is TreeNode<T, V> treeNode)
						treeNode.Children = BuildChildren(n.Id);
					return n;
				})
				.ToList();
		}

		return BuildChildren(rootId);
	}

	/// <summary>
	/// 获取所有叶子节点
	/// </summary>
	/// <typeparam name="T">节点ID类型</typeparam>
	/// <typeparam name="V">节点值类型</typeparam>
	/// <param name="roots">根节点列表</param>
	/// <returns>叶子节点列表</returns>
	public static IList<ITreeNode<T, V>> GetLeaves<T, V>(IEnumerable<ITreeNode<T, V>> roots)
	{
		var leaves = new List<ITreeNode<T, V>>();
		void CollectLeaves(ITreeNode<T, V> node)
		{
			if (node.Children.Count == 0)
				leaves.Add(node);
			else
				foreach (var child in node.Children)
					CollectLeaves(child);
		}

		foreach (var root in roots)
			CollectLeaves(root);

		return leaves;
	}

	/// <summary>
	/// 获取树的深度
	/// </summary>
	/// <typeparam name="T">节点ID类型</typeparam>
	/// <typeparam name="V">节点值类型</typeparam>
	/// <param name="root">根节点</param>
	/// <returns>深度</returns>
	public static int GetDepth<T, V>(ITreeNode<T, V> root)
	{
		int GetDepthInternal(ITreeNode<T, V> node, int currentDepth)
		{
			if (node.Children.Count == 0)
				return currentDepth;

			return node.Children.Max(c => GetDepthInternal(c, currentDepth + 1));
		}

		return GetDepthInternal(root, 1);
	}

	/// <summary>
	/// 遍历树（前序遍历）
	/// </summary>
	/// <typeparam name="T">节点ID类型</typeparam>
	/// <typeparam name="V">节点值类型</typeparam>
	/// <param name="root">根节点</param>
	/// <param name="action">遍历动作</param>
	public static void Traverse<T, V>(ITreeNode<T, V> root, Action<ITreeNode<T, V>, int> action)
	{
		void TraverseInternal(ITreeNode<T, V> node, int depth)
		{
			action(node, depth);
			foreach (var child in node.Children)
				TraverseInternal(child, depth + 1);
		}

		TraverseInternal(root, 0);
	}
}
