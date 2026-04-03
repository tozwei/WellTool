namespace WellTool.Core.lang.tree;

using System;
using System.Collections.Generic;

/// <summary>
/// 树形节点接口
/// </summary>
public interface ITreeNode<T>
{
	/// <summary>
	/// 获取节点ID
	/// </summary>
	object GetId();

	/// <summary>
	/// 获取父节点ID
	/// </summary>
	object? GetParentId();

	/// <summary>
	/// 获取节点名称
	/// </summary>
	string GetName();

	/// <summary>
	/// 获取排序号
	/// </summary>
	int GetOrder();

	/// <summary>
	/// 获取子节点
	/// </summary>
	IList<ITreeNode<T>> Children { get; set; }
}

/// <summary>
/// 树形节点
/// </summary>
public class TreeNode<T> : ITreeNode<T>
{
	public object GetId() => throw new NotImplementedException();
	public object? GetParentId() => null;
	public string GetName() => string.Empty;
	public int GetOrder() => 0;
	public IList<ITreeNode<T>> Children { get; set; } = new List<ITreeNode<T>>();
}

/// <summary>
/// 树形结构构建器
/// </summary>
public class TreeBuilder<T> where T : ITreeNode<T>
{
	private readonly IList<T> _nodes;

	public TreeBuilder(IList<T> nodes)
	{
		_nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
	}

	/// <summary>
	/// 构建树形结构
	/// </summary>
	/// <returns>根节点列表</returns>
	public IList<T> Build()
	{
		var nodeMap = new Dictionary<object, T>();
		var rootNodes = new List<T>();

		foreach (var node in _nodes)
		{
			nodeMap[node.GetId()] = node;
		}

		foreach (var node in _nodes)
		{
			var parentId = node.GetParentId();
			if (parentId == null || !nodeMap.ContainsKey(parentId))
			{
				rootNodes.Add(node);
			}
			else
			{
				var parent = nodeMap[parentId];
				parent.Children.Add(node);
			}
		}

		return rootNodes;
	}
}

/// <summary>
/// 默认节点解析器
/// </summary>
public class DefaultNodeParser<T> : INodeParser<T> where T : class
{
	public object? GetId(T node, string idField)
	{
		var prop = node.GetType().GetProperty(idField);
		return prop?.GetValue(node);
	}

	public object? GetParentId(T node, string parentIdField)
	{
		var prop = node.GetType().GetProperty(parentIdField);
		return prop?.GetValue(node);
	}

	public string GetName(T node, string nameField)
	{
		var prop = node.GetType().GetProperty(nameField);
		return prop?.GetValue(node)?.ToString() ?? string.Empty;
	}

	public int GetOrder(T node, string orderField)
	{
		var prop = node.GetType().GetProperty(orderField);
		return (int)(prop?.GetValue(node) ?? 0);
	}
}

/// <summary>
/// 节点解析器接口
/// </summary>
public interface INodeParser<T>
{
	object? GetId(T node, string idField);
	object? GetParentId(T node, string parentIdField);
	string GetName(T node, string nameField);
	int GetOrder(T node, string orderField);
}
