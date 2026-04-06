namespace WellTool.Extra.Tests;

using WellTool.Extra.Spring;

public class SpringUtilTest
{
    [Fact]
    public void GetBeanTest()
    {
        var bean = SpringUtil.GetBean<string>("testBean");
        Assert.Null(bean);
    }

    [Fact]
    public void GetBeansTest()
    {
        var beans = SpringUtil.GetBeans<string>();
        Assert.NotNull(beans);
    }

    [Fact]
    public void ContainsBeanTest()
    {
        Assert.False(SpringUtil.ContainsBean("nonExistentBean"));
    }

    [Fact]
    public void GetBeanNamesTest()
    {
        var names = SpringUtil.GetBeanNames();
        Assert.NotNull(names);
    }

    [Fact]
    public void GetBeanTypeTest()
    {
        var bean = SpringUtil.GetBean(typeof(string));
        Assert.Null(bean);
    }

    [Fact]
    public void GetPropertyTest()
    {
        var prop = SpringUtil.GetProperty("test", "name");
        Assert.Null(prop);
    }

    [Fact]
    public void SetPropertyTest()
    {
        var obj = new object();
        SpringUtil.SetProperty(obj, "name", "value");
    }

    [Fact]
    public void InvokeMethodTest()
    {
        var obj = new object();
        var result = SpringUtil.InvokeMethod(obj, "ToString");
        Assert.NotNull(result);
    }

    [Fact]
    public void GetApplicationContextTest()
    {
        var ctx = SpringUtil.GetApplicationContext();
        Assert.Null(ctx);
    }

    [Fact]
    public void IsInSpringTest()
    {
        var result = SpringUtil.IsInSpring();
        Assert.False(result);
    }
}
