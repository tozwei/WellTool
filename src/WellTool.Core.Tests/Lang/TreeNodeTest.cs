using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class TreeNodeTest
{
    [Fact]
    public void ConstructorTest()
    {
        var node = new TreeNode<string>("1", "Root");
        Assert.Equal("1", node.Id);
        Assert.Equal("Root", node.Name);
    }

    [Fact]
    public void ParentTest()
    {
        var parent = new TreeNode<string>("1", "Parent");
        var child = new TreeNode<string>("2", "Child");
        parent.AddChild(child);
        Assert.Same(parent, child.Parent);
    }

    [Fact]
    public void ChildrenTest()
    {
        var parent = new TreeNode<string>("1", "Parent");
        var child1 = new TreeNode<string>("2", "Child1");
        var child2 = new TreeNode<string>("3", "Child2");
        parent.AddChild(child1);
        parent.AddChild(child2);
        Assert.Equal(2, parent.Children.Count);
    }

    [Fact]
    public void DeepTest()
    {
        var node = new TreeNode<string>("1", "Root");
        var child = new TreeNode<string>("2", "Child");
        node.AddChild(child);
        Assert.Equal(1, child.Deep);
    }
}

public class TreeNode<T>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public TreeNode<T> Parent { get; set; }
    public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();

    public TreeNode(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public void AddChild(TreeNode<T> child)
    {
        child.Parent = this;
        Children.Add(child);
    }

    public int Deep
    {
        get
        {
            int depth = 0;
            var current = Parent;
            while (current != null)
            {
                depth++;
                current = current.Parent;
            }
            return depth;
        }
    }
}
