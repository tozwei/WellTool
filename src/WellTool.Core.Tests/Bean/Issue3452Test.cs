using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;
using WellTool.Core.Bean.Copier;
using System.Collections.Generic;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue 3452 测试 - fillBeanWithMap 方法
/// </summary>
public class Issue3452Test
{
    [Fact]
    public void TestFillBeanWithMap()
    {
        var properties = new Dictionary<string, object>
        {
            { "name", "JohnDoe" },
            { "user_age", 25 }
        };
        var user = BeanUtil.MapToBean<User>(properties, CopyOptions.Create());
        Assert.Equal("JohnDoe", user.Name);
        Assert.Equal(25, user.UserAge);
    }

    public class User
    {
        public string Name { get; set; }
        public int UserAge { get; set; }
    }
}
