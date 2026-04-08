using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue 3091 测试 - copyToList 方法的泛型转换
/// </summary>
public class Issue3091Test
{
    [Fact]
    public void TestCopyToList()
    {
        var list = new List<long> { 1, 2 };
        var result = BeanUtil.CopyToList<long, int>(list);
        Assert.Equal("[1, 2]", string.Join(", ", result));
    }
}
