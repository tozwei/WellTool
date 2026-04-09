namespace WellTool.Extra.Tests;

using Xunit;
using WellTool.Extra.Template;

public class Issue3488Test
{
    [Fact]
    public void TemplateTest()
    {
        // 测试模板功能
        var template = TemplateUtil.GetByPath("test");
        Assert.NotNull(template);
    }
}
