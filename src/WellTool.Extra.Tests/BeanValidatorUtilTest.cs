namespace WellTool.Extra.Tests;

using Well.Extra.Validation;

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
        Assert.NotEmpty(errors);
    }

    [Fact]
    public void GetConstraintViolationsTest()
    {
        var bean = new TestBean { Name = "" };
        var violations = BeanValidatorUtil.GetConstraintViolations(bean);
        Assert.NotNull(volutions);
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
    public string Name { get; set; } = "default";
}

internal interface TestGroup
{
}
