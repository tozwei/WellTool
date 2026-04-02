using WellTool.Http.Webservice;
using Xunit;

namespace WellTool.Http.Tests.Webservice;

/// <summary>
/// SOAP 客户端测试类
/// </summary>
public class SoapClientTest
{
    [Fact]
    public void CreateSoapClientTest()
    {
        // 测试创建 SOAP 客户端
        var client = new SoapClient("http://example.com/soap", "http://example.com/test");
        Assert.NotNull(client);
    }
}

