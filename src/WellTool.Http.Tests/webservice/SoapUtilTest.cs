using System.IO;
using System.Text;
using WellTool.Http.Webservice;
using Xunit;

namespace WellTool.Http.Tests.Webservice
{
    /// <summary>
    /// SOAP工具类测试
    /// </summary>
    public class SoapUtilTest
    {
        [Fact]
        public void TestCreateClient()
        {
            // 测试创建客户端
            var client = SoapUtil.CreateClient("http://example.com/service");
            Assert.NotNull(client);
        }

        [Fact]
        public void TestCreateClientWithProtocol()
        {
            // 测试使用协议创建客户端
            var client = SoapUtil.CreateClient("http://example.com/service", SoapProtocol.SOAP_1_1);
            Assert.NotNull(client);
        }

        [Fact]
        public void TestCreateSoap11Envelope()
        {
            // 测试创建SOAP 1.1信封
            var envelope = SoapUtil.CreateSoap11Envelope("<test>content</test>");
            Assert.NotNull(envelope);
            Assert.Contains("Envelope", envelope);
            Assert.Contains("Body", envelope);
            Assert.Contains("test", envelope);
            Assert.Contains("content", envelope);
        }

        [Fact]
        public void TestCreateSoap12Envelope()
        {
            // 测试创建SOAP 1.2信封
            var envelope = SoapUtil.CreateSoap12Envelope("<test>content</test>");
            Assert.NotNull(envelope);
            Assert.Contains("soap12", envelope);
            Assert.Contains("Body", envelope);
        }

        [Fact]
        public void TestCreateSoapEnvelope()
        {
            // 测试创建指定协议信封
            var envelope11 = SoapUtil.CreateSoapEnvelope("<test/>", SoapProtocol.SOAP_1_1);
            Assert.Contains("soap:", envelope11);

            var envelope12 = SoapUtil.CreateSoapEnvelope("<test/>", SoapProtocol.SOAP_1_2);
            Assert.Contains("soap12:", envelope12);
        }

        [Fact]
        public void TestParseSoapResponse()
        {
            // 测试解析SOAP响应
            var response = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <result>test result</result>
    </soap:Body>
</soap:Envelope>";

            var body = SoapUtil.ParseSoapResponse(response);
            Assert.NotNull(body);
            Assert.Contains("result", body);
            Assert.Contains("test result", body);
        }

        [Fact]
        public void TestParseSoap12Response()
        {
            // 测试解析SOAP 1.2响应
            var response = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap12:Envelope xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
    <soap12:Body>
        <result>test result</result>
    </soap12:Body>
</soap12:Envelope>";

            var body = SoapUtil.ParseSoapResponse(response);
            Assert.NotNull(body);
            Assert.Contains("result", body);
        }

        [Fact]
        public void TestParseSoapResponseAsElement()
        {
            // 测试解析为XElement
            var response = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <result>test result</result>
    </soap:Body>
</soap:Envelope>";

            var element = SoapUtil.ParseSoapResponseAsElement(response);
            Assert.NotNull(element);
        }

        [Fact]
        public void TestFormat()
        {
            // 测试格式化XML
            var xml = "<root><child>value</child></root>";
            var formatted = SoapUtil.Format(xml, true);
            
            Assert.NotNull(formatted);
            Assert.Contains("\n", formatted);
            Assert.Contains("  ", formatted);
        }

        [Fact]
        public void TestFormatNoIndent()
        {
            // 测试不缩进格式化
            var xml = "<root><child>value</child></root>";
            var formatted = SoapUtil.Format(xml, false);
            
            Assert.NotNull(formatted);
            Assert.DoesNotContain("\n", formatted);
        }

        [Fact]
        public void TestToString()
        {
            // 测试转换为字符串
            var xml = "<test>content</test>";
            var result = SoapUtil.ToString(xml, true);
            
            Assert.NotNull(result);
        }

        [Fact]
        public void TestToStringWithCharset()
        {
            // 测试带编码转换
            var xml = "<test>中文内容</test>";
            var result = SoapUtil.ToString(xml, true, Encoding.UTF8);
            
            Assert.NotNull(result);
        }

        [Fact]
        public void TestExtractFault()
        {
            // 测试提取错误信息
            var response = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <soap:Fault>
            <faultstring>Server error</faultstring>
        </soap:Fault>
    </soap:Body>
</soap:Envelope>";

            var fault = SoapUtil.ExtractFault(response);
            Assert.NotNull(fault);
            Assert.Contains("Server error", fault);
        }

        [Fact]
        public void TestExtractFaultSoap12()
        {
            // 测试提取SOAP 1.2错误
            var response = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap12:Envelope xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
    <soap12:Body>
        <soap12:Fault>
            <soap12:Reason>
                <soap12:Text xml:lang=""en"">Server error</soap12:Text>
            </soap12:Reason>
        </soap12:Fault>
    </soap12:Body>
</soap12:Envelope>";

            var fault = SoapUtil.ExtractFault(response);
            Assert.NotNull(fault);
            Assert.Contains("Server error", fault);
        }

        [Fact]
        public void TestHasFault()
        {
            // 测试检查错误
            var responseWithFault = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <soap:Fault>
            <faultstring>Error</faultstring>
        </soap:Fault>
    </soap:Body>
</soap:Envelope>";

            Assert.True(SoapUtil.HasFault(responseWithFault));

            var responseWithoutFault = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <result>success</result>
    </soap:Body>
</soap:Envelope>";

            Assert.False(SoapUtil.HasFault(responseWithoutFault));
        }

        [Fact]
        public void TestCreateSoap11Method()
        {
            // 测试创建SOAP 1.1方法
            var method = SoapUtil.CreateSoap11Method("getUser", "http://example.com/ns");
            Assert.NotNull(method);
            Assert.Equal("Envelope", method.Name.LocalName);
        }

        [Fact]
        public void TestCreateSoap12Method()
        {
            // 测试创建SOAP 1.2方法
            var method = SoapUtil.CreateSoap12Method("getUser", "http://example.com/ns");
            Assert.NotNull(method);
            Assert.Equal("Envelope", method.Name.LocalName);
        }

        [Fact]
        public void TestNamespaceConstants()
        {
            // 测试命名空间常量
            Assert.Equal("http://schemas.xmlsoap.org/soap/envelope/", SoapUtil.Soap11Namespace);
            Assert.Equal("http://www.w3.org/2003/05/soap-envelope", SoapUtil.Soap12Namespace);
        }
    }
}
