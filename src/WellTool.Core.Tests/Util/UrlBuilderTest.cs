using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class UrlBuilderTest
{
    [Fact]
    public void ConstructorTest()
    {
        var url = new UrlBuilder();
        Assert.NotNull(url);
    }

    [Fact]
    public void SetSchemeTest()
    {
        var url = new UrlBuilder().SetScheme("https");
        Assert.Equal("https", url.Scheme);
    }

    [Fact]
    public void SetHostTest()
    {
        var url = new UrlBuilder().SetHost("example.com");
        Assert.Equal("example.com", url.Host);
    }

    [Fact]
    public void SetPortTest()
    {
        var url = new UrlBuilder().SetPort(8080);
        Assert.Equal(8080, url.Port);
    }

    [Fact]
    public void SetPathTest()
    {
        var url = new UrlBuilder().SetPath("/api/test");
        Assert.Equal("/api/test", url.Path);
    }

    [Fact]
    public void AddQueryTest()
    {
        var url = new UrlBuilder().AddQuery("name", "John");
        Assert.Equal("John", url.Query["name"]);
    }

    [Fact]
    public void BuildTest()
    {
        var url = new UrlBuilder()
            .SetScheme("https")
            .SetHost("example.com")
            .SetPort(8080)
            .SetPath("/api/test")
            .AddQuery("name", "John")
            .Build();
        Assert.Contains("https", url);
        Assert.Contains("example.com", url);
    }
}

public class UrlBuilder
{
    public string Scheme { get; private set; } = "http";
    public string Host { get; private set; } = "";
    public int Port { get; private set; } = -1;
    public string Path { get; private set; } = "";
    public Dictionary<string, string> Query { get; } = new Dictionary<string, string>();

    public UrlBuilder SetScheme(string scheme)
    {
        Scheme = scheme;
        return this;
    }

    public UrlBuilder SetHost(string host)
    {
        Host = host;
        return this;
    }

    public UrlBuilder SetPort(int port)
    {
        Port = port;
        return this;
    }

    public UrlBuilder SetPath(string path)
    {
        Path = path;
        return this;
    }

    public UrlBuilder AddQuery(string key, string value)
    {
        Query[key] = value;
        return this;
    }

    public override string ToString()
    {
        var url = $"{Scheme}://{Host}";
        if (Port > 0) url += $":{Port}";
        url += Path;
        if (Query.Count > 0)
            url += "?" + string.Join("&", Query.Select(kv => $"{kv.Key}={kv.Value}"));
        return url;
    }

    public string Build() => ToString();
}
