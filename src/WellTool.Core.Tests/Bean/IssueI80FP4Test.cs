using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;
using WellTool.Core.Bean.Copier;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue I80FP4 测试 - setIgnoreProperties 大小写敏感性
/// </summary>
public class IssueI80FP4Test
{
    [Fact]
    public void TestCopyPropertiesIgnoreCase()
    {
        var sourceDest = new Dest { CPF = 33699, EnderDest = "abc" };
        var dest = new Dest();
        var copyOptions = CopyOptions.Create()
            .SetIgnoreNullValue(true)
            .SetIgnoreCase(true)
            .SetIgnoreProperties("enderDest");
        BeanUtil.CopyProperties(sourceDest, dest, copyOptions);
        Assert.Null(dest.EnderDest);
    }

    [Fact]
    public void TestCopyPropertiesIgnoreCase2()
    {
        var sourceDest = new Dest { CPF = 33699, EnderDest = "abc" };
        var dest = new Dest();
        var copyOptions = CopyOptions.Create()
            .SetIgnoreNullValue(true)
            .SetIgnoreCase(true)
            .SetIgnoreProperties("enderdest");
        BeanUtil.CopyProperties(sourceDest, dest, copyOptions);
        Assert.Null(dest.EnderDest);
    }

    public class Dest
    {
        public int CPF { get; set; }
        public string EnderDest { get; set; }
    }
}
