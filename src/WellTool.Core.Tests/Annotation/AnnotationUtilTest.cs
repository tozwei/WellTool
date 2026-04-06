using WellTool.Core.Annotation;
using Xunit;

namespace WellTool.Core.Tests;

public class AnnotationUtilTest
{
    [Fact]
    public void GetAnnotationTest()
    {
        var type = typeof(TestClass);
        var annotation = AnnotationUtil.GetAttribute<MyAnnotation>(type);
        Assert.NotNull(annotation);
    }

    [Fact]
    public void GetAnnotationsTest()
    {
        var type = typeof(TestClass);
        var annotations = AnnotationUtil.GetAnnotations(type, true);
        Assert.NotNull(annotations);
    }

    [Fact]
    public void HasAnnotationTest()
    {
        var type = typeof(TestClass);
        Assert.True(AnnotationUtil.HasAttribute<MyAnnotation>(type));
        Assert.False(AnnotationUtil.HasAttribute<NotExistAnnotation>(type));
    }

    [Fact]
    public void GetAnnotationValueTest()
    {
        var type = typeof(TestClass);
        var value = AnnotationUtil.GetAnnotationValue<MyAnnotation, string>(type, a => a.Value);
        Assert.NotNull(value);
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

    private class NotExistAnnotation : Attribute
    {
    }
}
