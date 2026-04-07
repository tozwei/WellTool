using Xunit;
using System.Reflection;

namespace WellTool.Core.Tests;

public class AnnotationUtilLastTest
{
    [Fact]
    public void GetAnnotationTest()
    {
        var type = typeof(TestClass);
        var annotation = type.GetCustomAttribute<MyAnnotation>();
        Assert.NotNull(annotation);
    }

    [Fact]
    public void GetAnnotationsTest()
    {
        var type = typeof(TestClass);
        var annotations = type.GetCustomAttributes();
        Assert.NotNull(annotations);
    }

    [Fact]
    public void HasAnnotationTest()
    {
        var type = typeof(TestClass);
        var hasAnnotation = type.IsDefined(typeof(MyAnnotation), false);
        Assert.True(hasAnnotation);
    }

    [MyAnnotation(Value = "test")]
    private class TestClass
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    private class MyAnnotation : Attribute
    {
        public string Value { get; set; } = "";
    }
}
