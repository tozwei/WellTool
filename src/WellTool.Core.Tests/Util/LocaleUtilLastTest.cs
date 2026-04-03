using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class LocaleUtilLastTest
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
        var language = LocaleUtil.GetLanguage("zh-CN");
        Assert.Equal("zh", language);
    }

    [Fact]
    public void GetCountryTest()
    {
        var country = LocaleUtil.GetCountry("zh-CN");
        Assert.Equal("CN", country);
    }
}
