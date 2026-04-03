using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class LinkedBlockingDequeTest
{
    [Fact]
    public void AddFirstTest()
    {
        var deque = new LinkedBlockingDeque<int>(10);
        deque.AddFirst(1);
        deque.AddFirst(2);
        Assert.Equal(2, deque.Count);
    }

    [Fact]
    public void AddLastTest()
    {
        var deque = new LinkedBlockingDeque<int>(10);
        deque.AddLast(1);
        deque.AddLast(2);
        Assert.Equal(2, deque.Count);
    }

    [Fact]
    public void TakeFirstTest()
    {
        var deque = new LinkedBlockingDeque<int>(10);
        deque.AddLast(1);
        deque.AddLast(2);
        Assert.Equal(1, deque.TakeFirst());
        Assert.Equal(1, deque.Count);
    }

    [Fact]
    public void TakeLastTest()
    {
        var deque = new LinkedBlockingDeque<int>(10);
        deque.AddLast(1);
        deque.AddLast(2);
        Assert.Equal(2, deque.TakeLast());
        Assert.Equal(1, deque.Count);
    }

    [Fact]
    public void OfferFirstTest()
    {
        var deque = new LinkedBlockingDeque<int>(2);
        Assert.True(deque.OfferFirst(1));
        Assert.True(deque.OfferFirst(2));
        Assert.False(deque.OfferFirst(3));
    }

    [Fact]
    public void OfferLastTest()
    {
        var deque = new LinkedBlockingDeque<int>(2);
        Assert.True(deque.OfferLast(1));
        Assert.True(deque.OfferLast(2));
        Assert.False(deque.OfferLast(3));
    }
}
