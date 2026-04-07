using WellTool.Core.Bean;
using WellTool.Core.Bean.Copier;
using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanCopyMappingTest
{
    [Fact]
    public void CopyPropertiesTest()
    {
        var copyOptions = CopyOptions.Create()
            .SetFieldMapping(MapUtil.Of("car", "carNo"));

        var b = new B { Car = "12312312" };
        var a = new A();
        var c = new C();

        BeanUtil.CopyProperties(b, a, copyOptions);
        BeanUtil.CopyProperties(a, c);

        Assert.Equal("12312312", c.CarNo);
    }

    private class A
    {
        public string? CarNo { get; set; }
    }

    private class B
    {
        public string? Car { get; set; }
    }

    private class C
    {
        public string? CarNo { get; set; }
    }
}
