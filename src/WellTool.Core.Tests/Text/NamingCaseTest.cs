using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class NamingCaseTest
{
    [Fact]
    public void ToCamelCaseTest()
    {
        Assert.Equal("tableTestOfDay", NamingCase.ToCamelCase("Table_Test_Of_day"));
        Assert.Equal("TableTestOfDay", NamingCase.ToCamelCase("TableTestOfDay"));
        Assert.Equal("abc1d", NamingCase.ToCamelCase("abc_1d"));
    }

    [Fact]
    public void ToUnderLineCaseTest()
    {
        Assert.Equal("table_test_of_day", NamingCase.ToUnderlineCase("Table_Test_Of_day"));
        Assert.Equal("_table_test_of_day_", NamingCase.ToUnderlineCase("_Table_Test_Of_day_"));
        Assert.Equal("_table_test_of_DAY_", NamingCase.ToUnderlineCase("_Table_Test_Of_DAY_"));
        Assert.Equal("_table_test_of_DAY_today", NamingCase.ToUnderlineCase("_TableTestOfDAYToday"));
        Assert.Equal("hello_world_test", NamingCase.ToUnderlineCase("HelloWorld_test"));
        Assert.Equal("H2", NamingCase.ToUnderlineCase("H2"));
        Assert.Equal("customer_nick_v2", NamingCase.ToUnderlineCase("customerNickV2"));
    }

    [Fact]
    public void ToPascalCaseTest()
    {
        Assert.Equal("TableTestOfDay", NamingCase.ToPascalCase("table_test_of_day"));
        Assert.Equal("TableTestOfDay", NamingCase.ToPascalCase("TableTestOfDay"));
        Assert.Equal("Abc1d", NamingCase.ToPascalCase("abc_1d"));
    }
}
