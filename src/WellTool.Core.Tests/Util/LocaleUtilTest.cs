using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class LocaleUtilTest
{
    [Fact]
    public void GetDefaultTest()
    {
        var locale = LocaleUtil.GetDefault();
        Assert.NotNull(locale);
    }

    [Fact]
    public void GetAvailableLocalesTest()
    {
        var locales = LocaleUtil.GetAvailableLocales();
        Assert.NotNull(locales);
        Assert.NotEmpty(locales);
    }

    [Fact]
    public void ParseTest()
    {
        var locale = LocaleUtil.Parse("zh-CN");
        Assert.NotNull(locale);
    }

    [Fact]
    public void GetLanguageTest()
    {
        var locale = LocaleUtil.GetLanguage("zh-CN");
        Assert.Equal("zh", locale);
    }

    [Fact]
    public void GetCountryTest()
    {
        var country = LocaleUtil.GetCountry("zh-CN");
        Assert.Equal("CN", country);
    }

    [Fact]
    public void ToStringTest()
    {
        var locale = LocaleUtil.Parse("en-US");
        Assert.Equal("en-US", locale.ToString());
    }
}
