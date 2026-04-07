using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class UniqueKeySetTest
{
    [Fact]
    public void AddTest()
    {
        var set = new UniqueKeySet<string, UniqueTestBean>(x => x.Id);
        set.Add(new UniqueTestBean("id1", "张三", "地球"));
        set.Add(new UniqueTestBean("id2", "李四", "火星"));
        // id重复，替换之前的元素
        set.Add(new UniqueTestBean("id2", "王五", "木星"));

        // 后两个ID重复
        Assert.Equal(2, set.Count);
    }

    [Fact]
    public void ContainsTest()
    {
        var set = new UniqueKeySet<string, UniqueTestBean>(x => x.Id);
        var bean1 = new UniqueTestBean("id1", "张三", "地球");
        set.Add(bean1);

        Assert.True(set.Contains(bean1));

        var bean2 = new UniqueTestBean("id1", "李四", "火星");
        Assert.True(set.Contains(bean2)); // 因为 ID 相同，所以被认为存在
    }

    private class UniqueTestBean
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public UniqueTestBean(string id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
