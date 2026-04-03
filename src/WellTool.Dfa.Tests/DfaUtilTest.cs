namespace WellTool.Dfa.Tests;

using Well.Dfa;

public class DfaUtilTest
{
    [Fact]
    public void BuildTreeTest()
    {
        var words = new[] { "test", "testing", "tested" };
        var tree = DfaUtil.BuildTree(words);
        Assert.NotNull(tree);
    }

    [Fact]
    public void ContainsTest()
    {
        var words = new[] { "bad", "evil" };
        var tree = DfaUtil.BuildTree(words);
        Assert.True(DfaUtil.Contains(tree, "This is bad"));
        Assert.False(DfaUtil.Contains(tree, "This is good"));
    }

    [Fact]
    public void FindAllTest()
    {
        var words = new[] { "bad", "evil" };
        var tree = DfaUtil.BuildTree(words);
        var found = DfaUtil.FindAll(tree, "This is bad and evil");
        Assert.Equal(2, found.Count);
    }

    [Fact]
    public void ReplaceTest()
    {
        var words = new[] { "bad" };
        var tree = DfaUtil.BuildTree(words);
        var result = DfaUtil.Replace(tree, "This is bad word", '*');
        Assert.Equal("This is *** word", result);
    }

    [Fact]
    public void IsEmptyTreeTest()
    {
        var tree = DfaUtil.BuildTree(Array.Empty<string>());
        Assert.Null(tree);
    }

    [Fact]
    public void BuildWithNullTest()
    {
        var tree = DfaUtil.BuildTree(null);
        Assert.Null(tree);
    }

    [Fact]
    public void ContainsWithNullTreeTest()
    {
        Assert.False(DfaUtil.Contains(null, "test"));
    }

    [Fact]
    public void FindAllWithOverlapTest()
    {
        var words = new[] { "test", "testing" };
        var tree = DfaUtil.BuildTree(words);
        var found = DfaUtil.FindAll(tree, "testing");
        Assert.True(found.Count > 0);
    }
}
