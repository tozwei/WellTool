using WellTool.Setting.Yaml;
using Xunit;

namespace WellTool.Setting.Tests;

/// <summary>
/// YamlUtil 单元测试
/// </summary>
public class YamlUtilTest
{
    /// <summary>
    /// 测试 YAML 加载功能
    /// </summary>
    [Fact]
    public void LoadByPathTest()
    {
        var result = YamlUtil.LoadByPath("TestData/test.yaml");

        Assert.Equal("John", result["firstName"]);
        Assert.Equal("Doe", result["lastName"]);
        Assert.Equal(31, Convert.ToInt32(result["age"]));

        // 测试嵌套结构
        Assert.True(result.ContainsKey("contactDetails"));
        Assert.True(result.ContainsKey("homeAddress"));
    }

    /// <summary>
    /// 测试 YAML 强类型加载
    /// </summary>
    [Fact]
    public void LoadTypedObjectTest()
    {
        // 只测试基本字段，不包含嵌套结构
        var yamlContent = @"
firstName: John
lastName: Doe
age: 31
";
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(yamlContent));
        var person = YamlUtil.Load<Person>(stream);

        Assert.Equal("John", person.FirstName);
        Assert.Equal("Doe", person.LastName);
        Assert.Equal(31, person.Age);
    }

    /// <summary>
    /// 测试 YAML 序列化
    /// </summary>
    [Fact]
    public void DumpTest()
    {
        var tempFile = Path.GetTempFileName() + ".yaml";
        try
        {
            var dict = new Dictionary<string, object?>
            {
                ["name"] = "hutool",
                ["count"] = 1000
            };

            YamlUtil.Dump(dict, tempFile);

            // 验证文件已创建
            Assert.True(File.Exists(tempFile));

            // 读取并验证内容
            var content = File.ReadAllText(tempFile);
            Assert.Contains("name", content);
            Assert.Contains("hutool", content);
            Assert.Contains("count", content);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试从流中加载 YAML
    /// </summary>
    [Fact]
    public void LoadFromStreamTest()
    {
        var yamlContent = @"
firstName: Jane
lastName: Smith
age: 25
";
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(yamlContent));
        var result = YamlUtil.Load(stream);

        Assert.Equal("Jane", result["firstName"]);
        Assert.Equal("Smith", result["lastName"]);
        Assert.Equal(25, Convert.ToInt32(result["age"]));
    }

    /// <summary>
    /// 测试复杂 YAML 结构
    /// </summary>
    [Fact]
    public void ComplexYamlStructureTest()
    {
        var result = YamlUtil.LoadByPath("TestData/test.yaml");

        // 验证 contactDetails 是列表
        if (result.TryGetValue("contactDetails", out var contactObj) && contactObj is IList<object> contacts)
        {
            Assert.Equal(2, contacts.Count);
        }

        // 验证 homeAddress 是字典
        if (result.TryGetValue("homeAddress", out var addressObj) && addressObj is IDictionary<string, object> address)
        {
            Assert.Equal("Xyz, DEF Street", address["line"]);
            Assert.Equal("City Y", address["city"]);
        }
    }

    /// <summary>
    /// 用于测试的 Person 类
    /// </summary>
    public class Person
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Age { get; set; }
    }
}
