using System;
using WellTool.Http;
using Xunit;

namespace WellTool.Http.Tests;

/// <summary>
/// HttpGlobalConfig HTTP全局配置测试
/// </summary>
public class HttpGlobalConfigTest
{
    [Fact]
    public void Timeout_DefaultValue()
    {
        Assert.Equal(30000, HttpGlobalConfig.Timeout);
    }

    [Fact]
    public void MaxRedirectCount_DefaultValue()
    {
        Assert.Equal(2, HttpGlobalConfig.MaxRedirectCount);
    }

    [Fact]
    public void IgnoreEOFError_DefaultValue()
    {
        Assert.False(HttpGlobalConfig.IgnoreEOFError);
    }

    [Fact]
    public void IsDecodeUrl_DefaultValue()
    {
        Assert.True(HttpGlobalConfig.IsDecodeUrl);
    }

    [Fact]
    public void TrustAnyHost_DefaultValue()
    {
        Assert.False(HttpGlobalConfig.TrustAnyHost);
    }
}

/// <summary>
/// HttpException HTTP异常测试
/// </summary>
public class HttpExceptionTest
{
    [Fact]
    public void Constructor_Message()
    {
        var ex = new HttpException("HTTP error");
        Assert.Equal("HTTP error", ex.Message);
    }

    [Fact]
    public void Constructor_MessageAndInner()
    {
        var innerEx = new InvalidOperationException("Inner error");
        var ex = new HttpException("HTTP error", innerEx);
        Assert.Equal("HTTP error", ex.Message);
        Assert.Same(innerEx, ex.InnerException);
    }
}

/// <summary>
/// HttpStatus HTTP状态码测试
/// </summary>
public class HttpStatusTest
{
    [Fact]
    public void IsRedirected_Test()
    {
        Assert.True(HttpStatus.IsRedirected(301));
        Assert.True(HttpStatus.IsRedirected(302));
        Assert.False(HttpStatus.IsRedirected(200));
    }

    [Fact]
    public void RedirectStatusCodes_Exist()
    {
        Assert.Equal(307, HttpStatus.HTTP_TEMP_REDIRECT);
        Assert.Equal(308, HttpStatus.HTTP_PERMANENT_REDIRECT);
    }
}

/// <summary>
/// Method HTTP方法测试
/// </summary>
public class MethodTest
{
    [Fact]
    public void HttpMethods_Exist()
    {
        Assert.NotNull(Method.GET);
        Assert.NotNull(Method.POST);
        Assert.NotNull(Method.PUT);
        Assert.NotNull(Method.DELETE);
        Assert.NotNull(Method.PATCH);
        Assert.NotNull(Method.HEAD);
        Assert.NotNull(Method.OPTIONS);
    }
}
