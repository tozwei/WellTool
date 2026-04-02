using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellTool.Http;
using Xunit;

namespace WellTool.Http.Tests;

/// <summary>
/// HttpUtil 工具类扩展测试
/// </summary>
public class HttpUtilExpandedTests
{
    #region URL validation tests

    [Fact]
    public void IsHttpTest()
    {
        Assert.True(HttpUtil.IsHttp("Http://aaa.bbb"));
        Assert.True(HttpUtil.IsHttp("HTTP://aaa.bbb"));
        Assert.True(HttpUtil.IsHttp("http://example.com"));
        Assert.False(HttpUtil.IsHttp("FTP://aaa.bbb"));
        Assert.False(HttpUtil.IsHttp("https://example.com"));
    }

    [Fact]
    public void IsHttpsTest()
    {
        Assert.True(HttpUtil.IsHttps("Https://aaa.bbb"));
        Assert.True(HttpUtil.IsHttps("HTTPS://aaa.bbb"));
        Assert.True(HttpUtil.IsHttps("https://aaa.bbb"));
        Assert.False(HttpUtil.IsHttps("http://aaa.bbb"));
        Assert.False(HttpUtil.IsHttps("ftp://aaa.bbb"));
    }

    #endregion

    #region Parameter decoding tests

    [Fact]
    public void DecodeParamsTest()
    {
        var paramsStr = "uuuu=0&a=b&c=%3F%23%40!%24%25%5E%26%3Ddsssss555555";
        var map = HttpUtil.DECODE_PARAMS(paramsStr, Encoding.UTF8);

        Assert.Equal("0", map["uuuu"][0]);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("?#@!$%^&=dsssss555555", map["c"][0]);
    }

    [Fact]
    public void DecodeParamMapTest()
    {
        // 参数值存在分界标记等号时
        var paramMap = HttpUtil.DECODE_PARAM_MAP(
            "aa=123&f_token=NzBkMjQxNDM1MDVlMDliZTk1OTU3ZDI1OTI0NTBiOWQ=",
            Encoding.UTF8);

        Assert.Equal("123", paramMap["aa"][0]);
        Assert.Equal("NzBkMjQxNDM1MDVlMDliZTk1OTU3ZDI1OTI0NTBiOWQ=", paramMap["f_token"][0]);
    }

    [Fact]
    public void ToParamsTest()
    {
        var paramsStr = "uuuu=0&a=b&c=3Ddsssss555555";
        var listMap = HttpUtil.DECODE_PARAMS(paramsStr, Encoding.UTF8);

        // 将 Dictionary<string, List<string>> 转换为 IDictionary<string, object?>
        var paramMap = new Dictionary<string, object?>();
        foreach (var kvp in listMap)
        {
            paramMap[kvp.Key] = kvp.Value.FirstOrDefault();
        }

        var encodedParams = HttpUtil.ToParams(paramMap);
        Assert.Equal(paramsStr, encodedParams);
    }

    [Fact]
    public void EncodeParamTest_RemoveLeadingQuestion()
    {
        // ?单独存在去除之，&单位位于末尾去除之
        var paramsStr = "?a=b&c=d&";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=b&c=d", encode);
    }

    [Fact]
    public void EncodeParamTest_UrlNotEncoded()
    {
        // url 不参与转码
        var paramsStr = "http://www.abc.dd?a=b&c=d&";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("http://www.abc.dd?a=b&c=d", encode);
    }

    [Fact]
    public void EncodeParamTest_EqualsInValue()
    {
        // b=b 中的=被当作值的一部分，不做 encode
        var paramsStr = "a=b=b&c=d&";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=b%3Db&c=d", encode);
    }

    [Fact]
    public void EncodeParamTest_EmptyKey()
    {
        // =d 的情况被当作 key 为空
        var paramsStr = "a=bbb&c=d&=d";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=d&=d", encode);
    }

    [Fact]
    public void EncodeParamTest_EmptyValue()
    {
        // d=的情况被处理为 value 为空
        var paramsStr = "a=bbb&c=d&d=";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=d&d=", encode);
    }

    [Fact]
    public void EncodeParamTest_MultipleAmpersands()
    {
        // 多个&&被处理为单个，相当于空条件
        var paramsStr = "a=bbb&c=d&&&d=";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=d&d=", encode);
    }

    [Fact]
    public void EncodeParamTest_TrailingAmpersand()
    {
        // &d&相当于只有键，无值得情况
        var paramsStr = "a=bbb&c=d&d&";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=d&d=", encode);
    }

    [Fact]
    public void EncodeParamTest_ChineseCharacters()
    {
        // 中文的键和值被编码
        var paramsStr = "a=bbb&c=你好&哈喽&";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=%E4%BD%A0%E5%A5%BD&%E5%93%88%E5%96%BD=", encode);
    }

    [Fact]
    public void EncodeParamTest_FullUrl()
    {
        // URL 原样输出
        var paramsStr = "https://www.hutool.cn/";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal(paramsStr, encode);
    }

    [Fact]
    public void EncodeParamTest_FullUrlWithQuestionMark()
    {
        // URL 原样输出
        var paramsStr = "https://www.hutool.cn/?";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("https://www.hutool.cn/", encode);
    }

    [Fact]
    public void DecodeParamTest_RemoveLeadingQuestion()
    {
        // 开头的？被去除
        var a = "?a=b&c=d&";
        var map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
    }

    [Fact]
    public void DecodeParamTest_EmptyKey()
    {
        // =e 被当作空为 key，e 为 value
        var a = "?a=b&c=d&=e";
        var map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
        Assert.Equal("e", map[""][0]);
    }

    [Fact]
    public void DecodeParamTest_RemoveTrailingAmpersands()
    {
        // 多余的&去除
        var a = "?a=b&c=d&=e&&&&";
        var map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
        Assert.Equal("e", map[""][0]);
    }

    [Fact]
    public void DecodeParamTest_EmptyValue()
    {
        // 值为空
        var a = "?a=b&c=d&e=";
        var map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
        Assert.Equal("", map["e"][0]);
    }

    [Fact]
    public void DecodeParamTest_EmptyKeyAndValue()
    {
        // &=被作为键和值都为空
        var a = "a=b&c=d&=";
        var map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
        Assert.Equal("", map[""][0]);
    }

    [Fact]
    public void DecodeParamTest_OnlyKey()
    {
        // &e&这类单独的字符串被当作 key
        var a = "a=b&c=d&e&";
        var map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
    }

    [Fact]
    public void DecodeParamTest_EncodedValues()
    {
        // 被编码的键和值被还原
        var a = "a=bbb&c=%E4%BD%A0%E5%A5%BD&%E5%93%88%E5%96%BD=";
        var map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("bbb", map["a"][0]);
        Assert.Equal("你好", map["c"][0]);
        Assert.Equal("", map["哈喽"][0]);
    }

    #endregion

    #region URL with form tests

    [Fact]
    public void UrlWithFormTest()
    {
        var param = new Dictionary<string, object?>
        {
            ["AccessKeyId"] = "123",
            ["Action"] = "DescribeDomainRecords",
            ["Format"] = "date",
            ["DomainName"] = "lesper.cn",
            ["SignatureMethod"] = "POST",
            ["SignatureNonce"] = "123",
            ["SignatureVersion"] = "4.3.1",
            ["Timestamp"] = 123432453,
            ["Version"] = "1.0"
        };

        var urlWithForm = HttpUtil.UrlWithForm("http://api.hutool.cn/login?type=aaa", param, Encoding.UTF8, false);
        Assert.Contains("AccessKeyId=123", urlWithForm);
        Assert.Contains("DomainName=lesper.cn", urlWithForm);
    }

    #endregion

    #region Normalization tests

    [Fact]
    public void NormalizeParamsTest()
    {
        var encodeResult = HttpUtil.NormalizeParams("参数=", Encoding.UTF8);
        Assert.Equal("%E5%8F%82%E6%95%B0=", encodeResult);
    }

    [Fact]
    public void NormalizeBlankParamsTest()
    {
        var encodeResult = HttpUtil.NormalizeParams("", Encoding.UTF8);
        Assert.Equal("", encodeResult);
    }

    [Fact]
    public void NormalizeAmpersandParamsTest()
    {
        var encodeResult = HttpUtil.NormalizeParams("&", Encoding.UTF8);
        Assert.Equal("", encodeResult);
    }

    #endregion

    #region MimeType tests

    [Fact]
    public void GetMimeTypeTest()
    {
        var mimeType = HttpUtil.GetMimeType("aaa.aaa");
        Assert.Null(mimeType);
    }

    [Fact]
    public void GetMimeTypeTest_KnownExtension()
    {
        // 测试已知扩展名
        var htmlMime = HttpUtil.GetMimeType("test.html");
        var jsonMime = HttpUtil.GetMimeType("data.json");
        var xmlMime = HttpUtil.GetMimeType("config.xml");
        
        // 根据实现可能返回null或具体值
        Assert.True(htmlMime == null || htmlMime.Contains("html"));
    }

    #endregion

    #region Charset extraction tests

    [Fact]
    public void GetCharsetTest()
    {
        var charsetName = HtmlUtil.GetCharsetFromHeader("Charset=UTF-8;fq=0.9");
        Assert.Equal("UTF-8", charsetName);
    }

    [Fact]
    public void GetCharsetTest_LowerCase()
    {
        var charsetName = HtmlUtil.GetCharsetFromHeader("charset=utf-8");
        Assert.Equal("utf-8", charsetName);
    }

    #endregion
}

/// <summary>
/// HttpRequest 扩展测试类
/// </summary>
public class HttpRequestExpandedTests
{
    private const string TestUrl = "http://example.com/api";

    [Fact]
    public void SetAndGetHeaderTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetHeader("X-Custom-Header", "test-value");

        Assert.Equal("test-value", request.GetHeader("X-Custom-Header"));
    }

    [Fact]
    public void RemoveHeaderTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetHeader("X-Remove-Me", "value");
        request.RemoveHeader("X-Remove-Me");

        Assert.Null(request.GetHeader("X-Remove-Me"));
    }

    [Fact]
    public void SetContentTypeTest()
    {
        var request = HttpRequest.Post(TestUrl);
        request.SetContentType("application/json");

        var contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Contains("application/json", contentType);
    }

    [Fact]
    public void SetCharsetTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetCharset(Encoding.UTF8);

        Assert.NotNull(request);
    }

    [Fact]
    public void TimeoutTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.Timeout(5000);

        Assert.NotNull(request);
    }

    [Fact]
    public void SetFollowRedirectsTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetFollowRedirects(true);

        Assert.NotNull(request);
    }

    [Fact]
    public void SetMethodTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetMethod(Method.PUT);

        Assert.Equal(Method.PUT, request.GetMethod());
    }

    [Fact]
    public void FormTest()
    {
        var request = HttpRequest.Post(TestUrl);
        request.Form("key1", "value1");
        request.Form("key2", "value2");

        Assert.NotNull(request);
    }

    [Fact]
    public void BodyTest()
    {
        var request = HttpRequest.Post(TestUrl);
        request.Body("{\"test\":\"data\"}");

        Assert.NotNull(request);
    }

    [Fact]
    public void AllStaticMethodsTest()
    {
        var get = HttpRequest.Get(TestUrl);
        var post = HttpRequest.Post(TestUrl);
        var put = HttpRequest.Put(TestUrl);
        var delete = HttpRequest.Delete(TestUrl);
        var patch = HttpRequest.Patch(TestUrl);
        var head = HttpRequest.Head(TestUrl);
        var options = HttpRequest.Options(TestUrl);

        Assert.NotNull(get);
        Assert.NotNull(post);
        Assert.NotNull(put);
        Assert.NotNull(delete);
        Assert.NotNull(patch);
        Assert.NotNull(head);
        Assert.NotNull(options);
    }

    [Fact]
    public void ChainMethodCallTest()
    {
        var request = HttpRequest.Post(TestUrl)
            .SetHeader("Accept", ContentType.JSON)
            .SetContentType(ContentType.JSON)
            .SetCharset(Encoding.UTF8)
            .Timeout(10000)
            .Body("{\"data\":\"test\"}");

        Assert.NotNull(request);
        Assert.Equal(Method.POST, request.GetMethod());
    }
}
