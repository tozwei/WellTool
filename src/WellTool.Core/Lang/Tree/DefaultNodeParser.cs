namespace WellTool.Core.lang.tree;

/// <summary>
/// 默认节点解析器
/// </summary>
public class DefaultNodeParser<T> : INodeParser<T>
{
	/// <summary>
	/// 解析ID
	/// </summary>
	public virtual object? ParseId(T node)
	{
		var prop = typeof(T).GetProperty("Id");
		return prop?.GetValue(node);
	}

	/// <summary>
	/// 解析父ID
	/// </summary>
	public virtual object? ParseParentId(T node)
	{
		var prop = typeof(T).GetProperty("ParentId");
		return prop?.GetValue(node);
	}

	/// <summary>
	/// 解析名称
	/// </summary>
	public virtual string ParseName(T node)
	{
		var prop = typeof(T).GetProperty("Name");
		return prop?.GetValue(node)?.ToString() ?? string.Empty;
	}
}

/// <summary>
/// 节点解析器接口
/// </summary>
public interface INodeParser<T>
{
	/// <summary>
	/// 解析ID
	/// </summary>
	object? ParseId(T node);

	/// <summary>
	/// 解析父ID
	/// </summary>
	object? ParseParentId(T node);

	/// <summary>
	/// 解析名称
	/// </summary>
	string ParseName(T node);
}

/// <summary>
/// 树节点
/// </summary>
public class TreeNode<T>
{
	/// <summary>
	/// 节点ID
	/// </summary>
	public object Id { get; set; } = default!;

	/// <summary>
	/// 父节点ID
	/// </summary>
	public object? ParentId { get; set; }

	/// <summary>
	/// 节点名称
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// 节点数据
	/// </summary>
	public T Data { get; set; } = default!;

	/// <summary>
	/// 子节点列表
	/// </summary>
	public System.Collections.Generic.List<TreeNode<T>> Children { get; set; } = new();
}
