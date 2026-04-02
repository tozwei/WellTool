using System;
using System.Text;
using System.Xml.Linq;
using WellTool.Http.Webservice;
using Xunit;

namespace WellTool.Http.Tests.Webservice
{
    /// <summary>
    /// SOAP相关单元测试
    /// </summary>
    public class SoapClientTest
    {
        [Fact]
        public void TestCreateClient()
        {
            // 测试创建SOAP客户端
            var client = SoapClient.Create("http://example.com/service");
            Assert.NotNull(client);
        }

        [Fact]
        public void TestCreateClientWithProtocol()
        {
            // 测试使用指定协议创建客户端
            var client = SoapClient.Create("http://example.com/service", SoapProtocol.SOAP_1_1);
            Assert.NotNull(client);
        }

        [Fact]
        public void TestCreateClientWithProtocolAndNamespace()
        {
            // 测试使用协议和命名空间创建客户端
            var client = SoapClient.Create("http://example.com/service", SoapProtocol.SOAP_1_2, "http://example.com/ns");
            Assert.NotNull(client);
        }

        [Fact]
        public void TestSetMethod()
        {
            // 测试设置方法
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser", "http://example.com/ns");

            var msgStr = client.GetMsgStr(false);
            Assert.Contains("getUser", msgStr);
        }

        [Fact]
        public void TestSetMethodWithPrefix()
        {
            // 测试设置带前缀的方法
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("ns:getUser", "http://example.com/ns");

            var msgStr = client.GetMsgStr(false);
            Assert.Contains("getUser", msgStr);
            Assert.Contains("xmlns:ns", msgStr);
        }

        [Fact]
        public void TestSetParam()
        {
            // 测试设置参数
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser", "http://example.com/ns");
            client.SetParam("id", "123");

            var msgStr = client.GetMsgStr(false);
            Assert.Contains("id", msgStr);
            Assert.Contains("123", msgStr);
        }

        [Fact]
        public void TestSetParams()
        {
            // 测试批量设置参数
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser", "http://example.com/ns");
            client.SetParams(new System.Collections.Generic.Dictionary<string, object>
            {
                { "id", "123" },
                { "name", "test" }
            });

            var msgStr = client.GetMsgStr(false);
            Assert.Contains("id", msgStr);
            Assert.Contains("123", msgStr);
            Assert.Contains("name", msgStr);
            Assert.Contains("test", msgStr);
        }

        [Fact]
        public void TestSetCharset()
        {
            // 测试设置编码
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser");
            client.SetCharset(Encoding.UTF8);
            client.SetParam("name", "测试");

            var msgStr = client.GetMsgStr(false);
            // 验证消息包含正确的中文内容
            Assert.Contains("测试", msgStr);
        }

        [Fact]
        public void TestGetMsgStr()
        {
            // 测试获取SOAP消息字符串
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser");
            client.SetParam("id", "123");

            var msgStr = client.GetMsgStr(true);
            Assert.NotNull(msgStr);
            Assert.Contains("Envelope", msgStr);
            Assert.Contains("Body", msgStr);
        }

        [Fact]
        public void TestGetMsgStrPretty()
        {
            // 测试格式化输出
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser");
            client.SetParam("id", "123");

            var msgStr = client.GetMsgStr(true);
            // 格式化后应包含换行和缩进
            Assert.Contains("\n", msgStr);
        }

        [Fact]
        public void TestReset()
        {
            // 测试重置客户端
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser");
            client.SetParam("id", "123");

            client.Reset();
            client.SetMethod("getOrder");

            var msgStr = client.GetMsgStr(false);
            Assert.Contains("getOrder", msgStr);
            Assert.DoesNotContain("getUser", msgStr);
            Assert.DoesNotContain("id", msgStr);
        }

        [Fact]
        public void TestTimeout()
        {
            // 测试超时设置
            var client = SoapClient.Create("http://example.com/service");
            client.Timeout(5000);

            client.SetConnectionTimeout(3000);
            client.SetReadTimeout(5000);

            // 验证客户端可以正常创建
            Assert.NotNull(client);
        }

        [Fact]
        public void TestSetUrl()
        {
            // 测试设置URL
            var client = SoapClient.Create("http://example.com/service1");
            client.SetUrl("http://example.com/service2");

            client.SetMethod("test");
            var msgStr = client.GetMsgStr(false);
            Assert.NotNull(msgStr);
        }

        [Fact]
        public void TestAddSoapHeader()
        {
            // 测试添加SOAP头
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser");
            client.AddSoapHeader("AuthToken", "abc123");

            var msgStr = client.GetMsgStr(false);
            Assert.NotNull(msgStr);
        }

        [Fact]
        public void TestSoap11Envelope()
        {
            // 测试SOAP 1.1信封结构
            var client = SoapClient.Create("http://example.com/service", SoapProtocol.SOAP_1_1);
            client.SetMethod("test");

            var msgStr = client.GetMsgStr(false);
            Assert.Contains("Envelope", msgStr);
            Assert.Contains("Body", msgStr);
            // SOAP 1.1 命名空间
            Assert.Contains("http://schemas.xmlsoap.org/soap/envelope/", msgStr);
        }

        [Fact]
        public void TestSoap12Envelope()
        {
            // 测试SOAP 1.2信封结构
            var client = SoapClient.Create("http://example.com/service", SoapProtocol.SOAP_1_2);
            client.SetMethod("test");

            var msgStr = client.GetMsgStr(false);
            Assert.Contains("Envelope", msgStr);
            Assert.Contains("Body", msgStr);
            // SOAP 1.2 命名空间
            Assert.Contains("http://www.w3.org/2003/05/soap-envelope", msgStr);
        }

        [Fact]
        public void TestGetMethodElement()
        {
            // 测试获取方法元素
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser");
            client.SetParam("id", "123");

            var methodElement = client.GetMethodElement();
            Assert.NotNull(methodElement);
            Assert.Equal("getUser", methodElement.Name.LocalName);
        }

        [Fact]
        public void TestNestedParams()
        {
            // 测试嵌套参数
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser");
            client.SetParam("user", new System.Collections.Generic.Dictionary<string, object>
            {
                { "name", "test" },
                { "age", 25 }
            });

            var msgStr = client.GetMsgStr(false);
            Assert.Contains("user", msgStr);
            Assert.Contains("name", msgStr);
            Assert.Contains("test", msgStr);
        }

        [Fact]
        public void TestWriteToStream()
        {
            // 测试写入流
            var client = SoapClient.Create("http://example.com/service");
            client.SetMethod("getUser");
            client.SetParam("id", "123");

            using var stream = new System.IO.MemoryStream();
            client.Write(stream);

            stream.Position = 0;
            using var reader = new System.IO.StreamReader(stream);
            var content = reader.ReadToEnd();

            Assert.NotEmpty(content);
            Assert.Contains("getUser", content);
        }
    }
}
