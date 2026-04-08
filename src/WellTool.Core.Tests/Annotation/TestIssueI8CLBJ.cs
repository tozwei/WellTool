using Xunit;
using System.Reflection;
using WellTool.Core.Annotation;

namespace WellTool.Core.Tests.Annotation;

/// <summary>
/// Issue I8CLBJ 测试
/// </summary>
public class TestIssueI8CLBJ
{
    [Fact]
    public void TestAnnotation()
    {
        // 需要使用 BindingFlags.NonPublic | BindingFlags.Instance 获取私有字段
        var field = typeof(Foo).GetField("name", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(field);
        
        var annotations = field.GetCustomAttributes(true);
        Assert.NotNull(annotations);
    }

    public class Foo
    {
        private int id;
        [TestAnnotation("name")]
        private string name = "";
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class TestAnnotationAttribute : Attribute
    {
        public string Value { get; }

        public TestAnnotationAttribute(string value)
        {
            Value = value;
        }
    }
}
