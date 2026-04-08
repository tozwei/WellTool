using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;
using System.Collections.Generic;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue 3645 测试 - Map 到 Map 的属性复制
/// </summary>
public class Issue3645Test
{
    [Fact]
    public void TestCopyProperties()
    {
        var p = new User { UserId = 123L };
        var map = new Dictionary<long, User>
        {
            { 123L, p }
        };

        var m = new Dictionary<long, User>();
        BeanUtil.CopyProperties(map, m);
        var u = m[123L];
        Assert.NotNull(u);
    }

    public class User
    {
        public long UserId { get; set; }
    }
}
