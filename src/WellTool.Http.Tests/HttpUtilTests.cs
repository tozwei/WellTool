using WellTool.Http;
using Xunit;
using System.Text;

namespace WellTool.Http.Tests;

/// <summary>
/// HttpUtil 工具类测试
/// </summary>
public class HttpUtilTests
{
    [Fact]
    public void IsHttpTest()
    {
        Assert.True(HttpUtil.IsHttp("Http://aaa.bbb"));
        Assert.True(HttpUtil.IsHttp("HTTP://aaa.bbb"));
        Assert.False(HttpUtil.IsHttp("FTP://aaa.bbb"));
    }

    [Fact]
    public void IsHttpsTest()
    {
        Assert.True(HttpUtil.IsHttps("Https://aaa.bbb"));
        Assert.True(HttpUtil.IsHttps("HTTPS://aaa.bbb"));
        Assert.True(HttpUtil.IsHttps("https://aaa.bbb"));
        Assert.False(HttpUtil.IsHttps("ftp://aaa.bbb"));
    }

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
        var map = HttpUtil.DECODE_PARAMS(paramsStr, Encoding.UTF8);

        // 将 List<string> 转换为 IDictionary<string, object?>
        var objMap = new Dictionary<string, object?>();
        foreach (var kvp in map)
        {
            objMap[kvp.Key] = kvp.Value[0];
        }

        var encodedParams = HttpUtil.ToParams(objMap);
        Assert.Equal(paramsStr, encodedParams);
    }

    [Fact]
    public void EncodeParamTest()
    {
        // ?单独存在去除之，&单位位于末尾去除之
        var paramsStr = "?a=b&c=d&";
        var encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=b&c=d", encode);

        // url 不参与转码
        paramsStr = "http://www.abc.dd?a=b&c=d&";
        encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("http://www.abc.dd?a=b&c=d", encode);

        // b=b 中的=被当作值的一部分，不做 encode
        paramsStr = "a=b=b&c=d&";
        encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=b%3Db&c=d", encode);

        // =d 的情况被当作 key 为空
        paramsStr = "a=bbb&c=d&=d";
        encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=d&=d", encode);

        // d=的情况被处理为 value 为空
        paramsStr = "a=bbb&c=d&d=";
        encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=d&d=", encode);

        // 多个&&被处理为单个，相当于空条件
        paramsStr = "a=bbb&c=d&&&d=";
        encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=d&d=", encode);

        // &d&相当于只有键，无值得情况
        paramsStr = "a=bbb&c=d&d&";
        encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=d&d=", encode);

        // 中文的键和值被编码
        paramsStr = "a=bbb&c=你好&哈喽&";
        encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("a=bbb&c=%E4%BD%A0%E5%A5%BD&%E5%93%88%E5%96%BD=", encode);

        // URL 原样输出
        paramsStr = "https://www.hutool.cn/";
        encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal(paramsStr, encode);

        // URL 原样输出
        paramsStr = "https://www.hutool.cn/?";
        encode = HttpUtil.ENCODE_PARAMS(paramsStr, Encoding.UTF8);
        Assert.Equal("https://www.hutool.cn/", encode);
    }

    [Fact]
    public void DecodeParamTest()
    {
        // 开头的？被去除
        var a = "?a=b&c=d&";
        var map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);

        // =e 被当作空为 key，e 为 value
        a = "?a=b&c=d&=e";
        map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
        Assert.Equal("e", map[""][0]);

        // 多余的&去除
        a = "?a=b&c=d&=e&&&&";
        map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
        Assert.Equal("e", map[""][0]);

        // 值为空
        a = "?a=b&c=d&e=";
        map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
        Assert.Equal("", map["e"][0]);

        // &=被作为键和值都为空
        a = "a=b&c=d&=";
        map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
        Assert.Equal("", map[""][0]);

        // &e&这类单独的字符串被当作 key
        a = "a=b&c=d&e&";
        map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("b", map["a"][0]);
        Assert.Equal("d", map["c"][0]);
        Assert.Equal(string.Empty, map["e"].FirstOrDefault());
        Assert.False(map.ContainsKey(""));

        // 被编码的键和值被还原
        a = "a=bbb&c=%E4%BD%A0%E5%A5%BD&%E5%93%88%E5%96%BD=";
        map = HttpUtil.DECODE_PARAMS(a, Encoding.UTF8);
        Assert.Equal("bbb", map["a"][0]);
        Assert.Equal("你好", map["c"][0]);
        Assert.Equal("", map["哈喽"][0]);
    }

    [Fact]
    public void UrlWithFormTest()
    {
        var param = new Dictionary<string, object?>
        {
            ["AccessKeyId"] = "123",
            ["Action"] = "DescribeDomainRecords",
            ["Format"] = "date",
            ["DomainName"] = "lesper.cn", // 域名地址
            ["SignatureMethod"] = "POST",
            ["SignatureNonce"] = "123",
            ["SignatureVersion"] = "4.3.1",
            ["Timestamp"] = 123432453,
            ["Version"] = "1.0"
        };

        var urlWithForm = HttpUtil.UrlWithForm("http://api.hutool.cn/login?type=aaa", param, Encoding.UTF8, false);
        Assert.Equal(
            "http://api.hutool.cn/login?type=aaa&AccessKeyId=123&Action=DescribeDomainRecords&Format=date&DomainName=lesper.cn&SignatureMethod=POST&SignatureNonce=123&SignatureVersion=4.3.1&Timestamp=123432453&Version=1.0",
            urlWithForm);

        urlWithForm = HttpUtil.UrlWithForm("http://api.hutool.cn/login?type=aaa", param, Encoding.UTF8, false);
        Assert.Equal(
            "http://api.hutool.cn/login?type=aaa&AccessKeyId=123&Action=DescribeDomainRecords&Format=date&DomainName=lesper.cn&SignatureMethod=POST&SignatureNonce=123&SignatureVersion=4.3.1&Timestamp=123432453&Version=1.0",
            urlWithForm);
    }

    [Fact]
    public void GetCharsetTest()
    {
        var charsetName = WellTool.Http.HtmlUtil.GetCharsetFromHeader("Charset=UTF-8;fq=0.9");
        Assert.Equal("UTF-8", charsetName);
    }

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

    [Fact]
    public void GetMimeTypeTest()
    {
        var mimeType = HttpUtil.GetMimeType("aaa.aaa");
        Assert.Null(mimeType);
    }
}
