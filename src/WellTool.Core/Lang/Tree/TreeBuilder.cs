namespace WellTool.Core.Lang.Tree;

using System;
using System.Collections.Generic;

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
