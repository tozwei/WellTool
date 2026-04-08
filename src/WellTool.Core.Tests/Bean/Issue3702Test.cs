using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;
using WellTool.Core.Bean.Copier;
using System.Collections.Generic;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue 3702 测试 - setFieldValueEditor 编辑后的值继续判断 ignoreNullValue
/// </summary>
public class Issue3702Test
{
    [Fact]
    public void TestMapToMap()
    {
        var map = new Dictionary<string, string>
        {
            { "a", "" },
            { "b", "b" },
            { "c", "c" },
            { "d", "d" }
        };

        var map2 = new Dictionary<string, string>
        {
            { "a", "a1" },
            { "b", "b1" },
            { "c", "c1" },
            { "d", "d1" }
        };

        var option = CopyOptions.Create()
            .SetIgnoreNullValue(true)
            .SetIgnoreError(true)
            .SetFieldValueEditor((name, value) =>
            {
                if ("".Equals(value))
                {
                    return null;
                }
                return value;
            });

        BeanUtil.CopyProperties(map, map2, option);
        Assert.Equal("a1", map2["a"]);
        Assert.Equal("b", map2["b"]);
        Assert.Equal("c", map2["c"]);
        Assert.Equal("d", map2["d"]);
    }
}
