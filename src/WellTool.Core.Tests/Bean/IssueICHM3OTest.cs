using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;
using WellTool.Core.Util;
using System.Collections.Generic;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue ICHM3O 测试 - @Alias 注解在 Map 和 Bean 之间转换
/// </summary>
public class IssueICHM3OTest
{
    [Fact]
    public void TestMapToBean()
    {
        var map = new Dictionary<object, object>
        {
            { "doctor_name", "李医生" },
            { "doctor_id_card_value", "12345" },
            { "gender", "男" }
        };
        var doctor = BeanUtil.MapToBean<TestClass>(map);
        Assert.Equal("李医生", doctor.Name);
        Assert.Equal("12345", doctor.IdCardValue);

        var mapData = BeanUtil.BeanToMap(doctor, true, false);
        Assert.Equal("李医生", mapData["doctor_name"]);
        Assert.Equal("12345", mapData["doctor_id_card_value"]);
    }

    public class TestClass
    {
        [WellTool.Core.Bean.@Alias("doctor_name")]
        public string Name { get; set; }

        [WellTool.Core.Bean.@Alias("doctor_id_card_value")]
        public string IdCardValue { get; set; }

        [WellTool.Core.Bean.@Alias("doctor_name")]
        public string Gender { get; set; }
    }
}
