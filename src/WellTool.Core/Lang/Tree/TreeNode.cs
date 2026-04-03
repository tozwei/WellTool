namespace WellTool.Core.lang.tree;

using System;
using System.Collections.Generic;

/// <summary>
/// 树节点接口
/// </summary>
public interface INode<T>
{
	/// <summary>
	/// 获取ID
	/// </summary>
	object GetId();

	/// <summary>
	/// 获取父ID
	/// </summary>
	object? GetParentId();

	/// <summary>
	/// 获取名称
	/// </summary>
	string GetName();

	/// <summary>
	/// 获取数据
	/// </summary>
	T GetData();
}

/// <summary>
/// 树节点实现
/// </summary>
public class Node<T> : INode<T>
{
	public object Id { get; set; } = default!;
	public object? ParentId { get; set; }
	public string Name { get; set; } = string.Empty;
	public T Data { get; set; } = default!;
	public List<Node<T>> Children { get; set; } = new();

	public object GetId() => Id;
	public object? GetParentId() => ParentId;
	public string GetName() => Name;
	public T GetData() => Data;
}

/// <summary>
/// 树节点解析器
/// </summary>
public class NodeParser<T>
{
	/// <summary>
	/// 解析节点ID
	/// </summary>
	/// <param name="node">节点</param>
	/// <returns>ID</returns>
	public virtual object? ParseId(T node)
	{
		var prop = typeof(T).GetProperty("Id");
		return prop?.GetValue(node);
	}

	/// <summary>
	/// 解析父节点ID
	/// </summary>
	/// <param name="node">节点</param>
	/// <returns>父ID</returns>
	public virtual object? ParseParentId(T node)
	{
		var prop = typeof(T).GetProperty("ParentId");
		return prop?.GetValue(node);
	}

	/// <summary>
	/// 解析名称
	/// </summary>
	/// <param name="node">节点</param>
	/// <returns>名称</returns>
	public virtual string ParseName(T node)
	{
		var prop = typeof(T).GetProperty("Name");
		return prop?.GetValue(node)?.ToString() ?? string.Empty;
	}
}
