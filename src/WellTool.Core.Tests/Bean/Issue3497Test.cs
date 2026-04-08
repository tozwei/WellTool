using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;
using WellTool.Core.Bean.Copier;
using WellTool.Core.Text;
using System.Collections.Generic;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue 3497 测试 - Map 到 Map 转换时使用字段名编辑器
/// </summary>
public class Issue3497Test
{
    [Fact]
    public void TestMapToMap()
    {
        var aB = new Dictionary<string, string>
        {
            { "a_b", "1" }
        };
        var bean = BeanUtil.MapToBean<Dictionary<string, object>>(aB, CopyOptions.Create()
            .SetFieldNameEditor(NamingCase.ToUnderlineCase));
        Assert.Equal("1", bean["aB"]?.ToString());
    }
}
