using Xunit;
using WellTool.Core.Bean;
using WellTool.Core.Bean.Copier;

namespace WellTool.Core.Tests.Util;

/// <summary>
/// BeanCopyMapping 测试
/// </summary>
public class BeanCopyMappingLastTest
{
    [Fact]
    public void CopyPropertiesTest()
    {
        var copyOptions = CopyOptions.Create()
            .SetFieldMapping(new System.Collections.Generic.Dictionary<string, string>
            {
                { "car", "carNo" }
            });

        var b = new B { Car = "12312312" };
        var a = new A();
        var c = new C();
        BeanUtil.CopyProperties(b, a, copyOptions);
        BeanUtil.CopyProperties(a, c);

        Assert.Equal("12312312", c.CarNo);
    }

    public class A
    {
        public string CarNo { get; set; } = "";
    }

    public class B
    {
        public string Car { get; set; } = "";
    }

    public class C
    {
        public string CarNo { get; set; } = "";
    }
}
