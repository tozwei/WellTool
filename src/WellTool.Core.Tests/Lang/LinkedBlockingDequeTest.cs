using System.Collections.Concurrent;
using Xunit;

public class LinkedBlockingDequeTest
{
    [Fact]
    public void AddFirstTest()
    {
        var deque = new ConcurrentQueue<string>();
        deque.Enqueue("first");
        deque.Enqueue("second");
        Assert.Equal(2, deque.Count);
    }

    [Fact]
    public void AddLastTest()
    {
        var deque = new ConcurrentQueue<string>();
        deque.Enqueue("first");
        deque.Enqueue("last");
        Assert.Equal(2, deque.Count);
    }

    [Fact]
    public void TakeFirstTest()
    {
        var deque = new ConcurrentQueue<string>();
        deque.Enqueue("test");
        deque.TryDequeue(out var result);
        Assert.Equal("test", result);
        Assert.Empty(deque);
    }
}
