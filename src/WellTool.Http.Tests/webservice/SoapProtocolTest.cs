using WellTool.Http.Webservice;
using Xunit;

namespace WellTool.Http.Tests.Webservice
{
    /// <summary>
    /// SOAP协议测试
    /// </summary>
    public class SoapProtocolTest
    {
        [Fact]
        public void TestGetContentType()
        {
            // 测试获取Content-Type
            var contentType11 = SoapProtocol.SOAP_1_1.GetContentType();
            Assert.Equal("text/xml;charset=", contentType11);

            var contentType12 = SoapProtocol.SOAP_1_2.GetContentType();
            Assert.Equal("application/soap+xml;charset=", contentType12);
        }

        [Fact]
        public void TestGetNamespace()
        {
            // 测试获取命名空间
            var ns11 = SoapProtocol.SOAP_1_1.GetNamespace();
            Assert.Equal("http://schemas.xmlsoap.org/soap/envelope/", ns11);

            var ns12 = SoapProtocol.SOAP_1_2.GetNamespace();
            Assert.Equal("http://www.w3.org/2003/05/soap-envelope", ns12);
        }

        [Fact]
        public void TestGetSoapActionPrefix()
        {
            // 测试获取SOAPAction头前缀
            var prefix11 = SoapProtocol.SOAP_1_1.GetSoapActionPrefix();
            Assert.Equal("SOAPAction", prefix11);

            var prefix12 = SoapProtocol.SOAP_1_2.GetSoapActionPrefix();
            Assert.Equal("action", prefix12);
        }

        [Fact]
        public void TestEnumValues()
        {
            // 测试枚举值
            Assert.Equal(0, (int)SoapProtocol.SOAP_1_1);
            Assert.Equal(1, (int)SoapProtocol.SOAP_1_2);
        }
    }
}
