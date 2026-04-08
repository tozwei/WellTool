using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue I8JASO 测试 - @Alias 注解时 Bean 属性复制
/// </summary>
public class IssueI8JASOTest
{
    [Fact]
    public void TestCopy()
    {
        var userOne = new UserOne { Email = "123@qq.com" };
        var userTwo = new UserTwo();
        BeanUtil.CopyProperties(userOne, userTwo);
        Assert.Equal(userOne.Email, userTwo.Email);
    }

    public class UserOne
    {
        public long Id { get; set; }

        [WellTool.Core.Bean.@Alias("邮箱")]
        public string Email { get; set; }
    }

    public class UserTwo
    {
        public long Id { get; set; }

        [WellTool.Core.Bean.@Alias("邮箱")]
        public string Email { get; set; }
    }
}
