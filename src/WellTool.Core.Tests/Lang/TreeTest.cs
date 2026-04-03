using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class TreeTest
{
    [Fact]
    public void ConstructorTest()
    {
        var tree = new Tree<string>();
        tree.Id = "1";
        tree.Name = "Root";
        Assert.Equal("1", tree.Id);
        Assert.Equal("Root", tree.Name);
    }

    [Fact]
    public void AddChildrenTest()
    {
        var root = new Tree<string> { Id = "1", Name = "Root" };
        var child = new Tree<string> { Id = "2", Name = "Child" };
        root.AddChildren(child);
        Assert.Single(root.Children);
        Assert.Equal("2", root.Children[0].Id);
    }

    [Fact]
    public void GetPathTest()
    {
        var root = new Tree<string> { Id = "1", Name = "Root" };
        var child = new Tree<string> { Id = "2", Name = "Child" };
        root.AddChildren(child);
        var path = child.GetPath();
        Assert.Equal(2, path.Count);
    }

    [Fact]
    public void IsLeafTest()
    {
        var leaf = new Tree<string> { Id = "2" };
        Assert.True(leaf.IsLeaf);

        var parent = new Tree<string> { Id = "1" };
        parent.AddChildren(new Tree<string> { Id = "2" });
        Assert.False(parent.IsLeaf);
    }

    [Fact]
    public void IsRootTest()
    {
        var root = new Tree<string> { Id = "1" };
        Assert.True(root.IsRoot);
        Assert.False(root.Children.Count == 0 || root.Parent != null);
    }

    [Fact]
    public void GetDepthTest()
    {
        var root = new Tree<string> { Id = "1" };
        var child = new Tree<string> { Id = "2" };
        var grandchild = new Tree<string> { Id = "3" };
        root.AddChildren(child);
        child.AddChildren(grandchild);
        Assert.Equal(3, root.GetDepth());
    }

    [Fact]
    public void GetSiblingCountTest()
    {
        var parent = new Tree<string> { Id = "0" };
        var child1 = new Tree<string> { Id = "1" };
        var child2 = new Tree<string> { Id = "2" };
        parent.AddChildren(child1);
        parent.AddChildren(child2);
        Assert.Equal(2, child1.GetSiblingCount());
    }

    [Fact]
    public void GetSiblingsTest()
    {
        var parent = new Tree<string> { Id = "0" };
        var child1 = new Tree<string> { Id = "1" };
        var child2 = new Tree<string> { Id = "2" };
        parent.AddChildren(child1);
        parent.AddChildren(child2);
        var siblings = child1.GetSiblings();
        Assert.Equal(2, siblings.Count);
    }
}

public class Tree<T>
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public Tree<T> Parent { get; set; }
    public List<Tree<T>> Children { get; private set; } = new List<Tree<T>>();

    public void AddChildren(Tree<T> child)
    {
        child.Parent = this;
        Children.Add(child);
    }

    public List<Tree<T>> GetPath()
    {
        var path = new List<Tree<T>>();
        var current = this;
        while (current != null)
        {
            path.Insert(0, current);
            current = current.Parent;
        }
        return path;
    }

    public bool IsLeaf => Children.Count == 0;
    public bool IsRoot => Parent == null;

    public int GetDepth()
    {
        int depth = 1;
        var current = this;
        while (current.Parent != null)
        {
            depth++;
            current = current.Parent;
        }
        return depth;
    }

    public int GetSiblingCount()
    {
        if (Parent == null) return Children.Count;
        return Parent.Children.Count;
    }

    public List<Tree<T>> GetSiblings()
    {
        if (Parent == null) return Children;
        return Parent.Children;
    }
}
