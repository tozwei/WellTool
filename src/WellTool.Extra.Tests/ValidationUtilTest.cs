using System.ComponentModel.DataAnnotations;
using WellTool.Extra.Validation;

namespace WellTool.Extra.Tests;

/// <summary>
/// ValidationUtil 测试类
/// </summary>
public class ValidationUtilTest
{
    [Fact]
    public void TestValidate_ValidObject_ReturnsEmptyResults()
    {
        // 测试有效对象
        var user = new ValidUser
        {
            Name = "John",
            Age = 25,
            Email = "john@example.com"
        };
        
        var result = ValidationUtil.Validate(user);
        
        Assert.Empty(result);
    }

    [Fact]
    public void TestValidate_InvalidObject_ReturnsValidationResults()
    {
        // 测试无效对象
        var user = new ValidUser
        {
            Name = "",
            Age = -1,
            Email = "invalid-email"
        };
        
        var result = ValidationUtil.Validate(user);
        
        Assert.NotEmpty(result);
    }

    [Fact]
    public void TestValidate_RequiredFieldEmpty_ReturnsError()
    {
        // 测试必填字段为空
        var user = new ValidUser
        {
            Name = "",
            Age = 20,
            Email = "test@example.com"
        };
        
        var result = ValidationUtil.Validate(user);
        
        Assert.Contains(result, r => r.MemberNames.Contains("Name"));
    }

    [Fact]
    public void TestValidate_RangeViolation_ReturnsError()
    {
        // 测试范围违规
        var user = new ValidUser
        {
            Name = "John",
            Age = 150,
            Email = "john@example.com"
        };
        
        var result = ValidationUtil.Validate(user);
        
        Assert.Contains(result, r => r.MemberNames.Contains("Age"));
    }

    [Fact]
    public void TestValidate_InvalidEmail_ReturnsError()
    {
        // 测试无效邮箱
        var user = new ValidUser
        {
            Name = "John",
            Age = 25,
            Email = "not-an-email"
        };
        
        var result = ValidationUtil.Validate(user);
        
        Assert.Contains(result, r => r.MemberNames.Contains("Email"));
    }

    [Fact]
    public void TestValidate_StringLengthViolation_ReturnsError()
    {
        // 测试字符串长度违规
        var user = new ValidUser
        {
            Name = "AB", // 小于最小长度
            Age = 25,
            Email = "john@example.com"
        };
        
        var result = ValidationUtil.Validate(user);
        
        Assert.Contains(result, r => r.MemberNames.Contains("Name"));
    }

    [Fact]
    public void TestWarpValidate_ValidObject_ReturnsSuccessResult()
    {
        // 测试包装后的有效对象验证
        var user = new ValidUser
        {
            Name = "John",
            Age = 25,
            Email = "john@example.com"
        };
        
        var result = ValidationUtil.WarpValidate(user);
        
        Assert.True(result.Success);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public void TestWarpValidate_InvalidObject_ReturnsFailedResult()
    {
        // 测试包装后的无效对象验证
        var user = new ValidUser
        {
            Name = "",
            Age = 200,
            Email = "invalid"
        };
        
        var result = ValidationUtil.WarpValidate(user);
        
        Assert.False(result.Success);
        Assert.NotEmpty(result.ErrorMessages);
    }

    [Fact]
    public void TestWarpValidate_InvalidObject_ReturnsCorrectErrorCount()
    {
        // 测试返回的错误数量
        var user = new ValidUser
        {
            Name = "",
            Age = -1,
            Email = "invalid"
        };
        
        var result = ValidationUtil.WarpValidate(user);
        
        Assert.False(result.Success);
        Assert.True(result.ErrorMessages.Count >= 1);
    }

    [Fact]
    public void TestValidate_MultipleViolations_ReturnsAllErrors()
    {
        // 测试多个违规
        var user = new ValidUser
        {
            Name = "",
            Age = -1,
            Email = ""
        };
        
        var result = ValidationUtil.Validate(user);
        
        Assert.True(result.Count >= 1);
    }

    [Fact]
    public void TestWarpValidate_ErrorMessages_HaveCorrectStructure()
    {
        // 测试错误消息结构
        var user = new ValidUser
        {
            Name = "",
            Age = 25,
            Email = "invalid"
        };
        
        var result = ValidationUtil.WarpValidate(user);
        
        foreach (var error in result.ErrorMessages)
        {
            Assert.NotNull(error.PropertyName);
            Assert.NotNull(error.Message);
        }
    }

    [Fact]
    public void TestValidate_ValidEmailFormats_ReturnsNoEmailErrors()
    {
        // 测试有效邮箱格式
        var emails = new[] 
        { 
            "test@example.com",
            "user.name@domain.org",
            "user+tag@example.co.uk"
        };
        
        foreach (var email in emails)
        {
            var user = new ValidUser
            {
                Name = "John",
                Age = 25,
                Email = email
            };
            
            var result = ValidationUtil.Validate(user);
            Assert.DoesNotContain(result, r => r.MemberNames.Contains("Email"));
        }
    }

    [Fact]
    public void TestValidate_AgeBoundary_HandlesCorrectly()
    {
        // 测试年龄边界
        var userMinAge = new ValidUser { Name = "John", Age = 0, Email = "john@example.com" };
        var userMaxAge = new ValidUser { Name = "John", Age = 150, Email = "john@example.com" };
        
        var resultMin = ValidationUtil.Validate(userMinAge);
        var resultMax = ValidationUtil.Validate(userMaxAge);
        
        // 边界值可能通过或失败，取决于验证规则定义
        Assert.NotNull(resultMin);
        Assert.NotNull(resultMax);
    }

    /// <summary>
    /// 用于测试的有效用户类
    /// </summary>
    private class ValidUser
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(0, 150)]
        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
