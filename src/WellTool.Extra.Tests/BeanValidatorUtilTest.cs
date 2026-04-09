namespace WellTool.Extra.Tests;

using System.ComponentModel.DataAnnotations;
using WellTool.Extra.Validation;

public class BeanValidatorUtilTest
{
    [Fact]
    public void ValidateTest()
    {
        var result = BeanValidatorUtil.Validate(new TestBean { Name = "test" });
        Assert.True(result);
    }

    [Fact]
    public void ValidateEntityTest()
    {
        var bean = new TestBean { Name = "valid" };
        var result = BeanValidatorUtil.ValidateEntity(bean);
        Assert.Empty(result);
    }

    [Fact]
    public void ValidateNullTest()
    {
        var result = BeanValidatorUtil.Validate(null);
        Assert.False(result);
    }

    [Fact]
    public void ValidateInvalidBeanTest()
    {
        var bean = new TestBean { Name = "" };
        var errors = BeanValidatorUtil.ValidateEntity(bean);
        // 验证失败，应该返回错误信息
        Assert.NotEmpty(errors);
        Assert.Contains("Name is required", errors);
    }

    [Fact]
    public void ValidateGroupTest()
    {
        var bean = new TestBean { Name = "test" };
        var result = BeanValidatorUtil.Validate(bean, typeof(TestGroup));
        Assert.True(result);
    }
}

internal class TestBean
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    public string Name { get; set; } = "default";
}

internal interface TestGroup
{
}