using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class TrieTest
{
    [Fact]
    public void InsertTest()
    {
        var trie = new Trie();
        trie.Insert("hello");
        Xunit.Assert.True(trie.Contains("hello"));
    }

    [Fact]
    public void ContainsTest()
    {
        var trie = new Trie();
        trie.Insert("hello");
        trie.Insert("world");
        Xunit.Assert.True(trie.Contains("hello"));
        Xunit.Assert.True(trie.Contains("world"));
        Xunit.Assert.False(trie.Contains("hell"));
    }

    [Fact]
    public void StartsWithTest()
    {
        var trie = new Trie();
        trie.Insert("hello");
        trie.Insert("world");
        var prefixes = trie.StartsWith("hel");
        Xunit.Assert.Contains("hello", prefixes);
    }

    [Fact]
    public void RemoveTest()
    {
        var trie = new Trie();
        trie.Insert("hello");
        trie.Remove("hello");
        Xunit.Assert.False(trie.Contains("hello"));
    }

    [Fact]
    public void SizeTest()
    {
        var trie = new Trie();
        trie.Insert("hello");
        trie.Insert("world");
        trie.Insert("help");
        Xunit.Assert.Equal(3, trie.Size);
    }

    [Fact]
    public void IsEmptyTest()
    {
        var trie = new Trie();
        Xunit.Assert.True(trie.IsEmpty);
        trie.Insert("hello");
        Xunit.Assert.False(trie.IsEmpty);
    }
}

public class Trie
{
    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
        public bool IsEndOfWord { get; set; }
    }

    private readonly TrieNode _root = new TrieNode();
    private int _size;

    public void Insert(string word)
    {
        var node = _root;
        foreach (var c in word)
        {
            if (!node.Children.ContainsKey(c))
                node.Children[c] = new TrieNode();
            node = node.Children[c];
        }
        if (!node.IsEndOfWord)
        {
            node.IsEndOfWord = true;
            _size++;
        }
    }

    public bool Contains(string word)
    {
        var node = SearchNode(word);
        return node != null && node.IsEndOfWord;
    }

    public List<string> StartsWith(string prefix)
    {
        var result = new List<string>();
        var node = SearchNode(prefix);
        if (node != null)
            CollectWords(node, prefix, result);
        return result;
    }

    private void CollectWords(TrieNode node, string prefix, List<string> result)
    {
        if (node.IsEndOfWord)
            result.Add(prefix);
        foreach (var child in node.Children)
            CollectWords(child.Value, prefix + child.Key, result);
    }

    public bool Remove(string word)
    {
        return Remove(_root, word, 0);
    }

    private bool Remove(TrieNode node, string word, int depth)
    {
        if (node == null) return false;

        if (depth == word.Length)
        {
            if (!node.IsEndOfWord) return false;
            node.IsEndOfWord = false;
            _size--;
            return node.Children.Count == 0;
        }

        var c = word[depth];
        if (!node.Children.ContainsKey(c)) return false;

        var childNode = node.Children[c];
        var shouldDeleteChild = Remove(childNode, word, depth + 1);

        if (shouldDeleteChild)
        {
            node.Children.Remove(c);
            return node.Children.Count == 0 && !node.IsEndOfWord;
        }
        return false;
    }

    private TrieNode SearchNode(string prefix)
    {
        var node = _root;
        foreach (var c in prefix)
        {
            if (!node.Children.ContainsKey(c))
                return null;
            node = node.Children[c];
        }
        return node;
    }

    public int Size => _size;
    public bool IsEmpty => _size == 0;
}
