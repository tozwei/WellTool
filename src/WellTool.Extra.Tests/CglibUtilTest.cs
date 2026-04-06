namespace WellTool.Extra.Tests;

using WellTool.Extra.Spring;

public class EnableSpringUtilTest
{
    [Fact]
    public void IsInSpringTest()
    {
        var result = EnableSpringUtil.IsInSpring();
        Assert.False(result);
    }

    [Fact]
    public void GetBeansTest()
    {
        var beans = EnableSpringUtil.GetBeans<string>();
        Assert.NotNull(beans);
    }

    [Fact]
    public void GetBeanTest()
    {
        var bean = EnableSpringUtil.GetBean<string>("testBean");
        Assert.Null(bean);
    }

    [Fact]
    public void GetBeanTypeTest()
    {
        var bean = EnableSpringUtil.GetBean(typeof(string));
        Assert.Null(bean);
    }
}
