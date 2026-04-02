using System;
using System.Text;
using WellTool.Core.Lang;
using WellTool.Core.Util;

namespace WellTool.Core.Net.Url
{
    /// <summary>
    /// URL 生成器，格式形如：
    /// <pre>
    /// [scheme:]scheme-specific-part[#fragment]
    /// [scheme:][//authority][path][?query][#fragment]
    /// [scheme:][//host:port][path][?query][#fragment]
    /// </pre>
    /// </summary>
    public class UrlBuilder : IBuilder<string>
    {
        private const string DEFAULT_SCHEME = "http";

        /// <summary>
        /// 协议，例如http
        /// </summary>
        private string _scheme;

        /// <summary>
        /// 主机，例如127.0.0.1
        /// </summary>
        private string _host;

        /// <summary>
        /// 端口，默认-1
        /// </summary>
        private int _port = -1;

        /// <summary>
        /// 路径，例如/aa/bb/cc
        /// </summary>
        private UrlPath _path;

        /// <summary>
        /// 查询语句，例如a=1&b=2
        /// </summary>
        private UrlQuery _query;

        /// <summary>
        /// 标识符，例如#后边的部分
        /// </summary>
        private string _fragment;

        /// <summary>
        /// 编码，用于URLEncode和URLDecode
        /// </summary>
        private Encoding _charset;

        /// <summary>
        /// 是否需要编码`%`
        /// 区别对待，如果是，则生成URL时需要重新全部编码，否则跳过所有`%`
        /// </summary>
        private bool _needEncodePercent;

        /// <summary>
        /// 使用URI构建UrlBuilder
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="charset">编码，用于URLEncode和URLDecode</param>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder Of(Uri uri, Encoding charset)
        {
            return Of(uri.Scheme, uri.Host, uri.Port, uri.AbsolutePath, uri.Query.TrimStart('?'), uri.Fragment.TrimStart('#'), charset);
        }

        /// <summary>
        /// 使用URL字符串构建UrlBuilder，当传入的URL没有协议时，按照http协议对待
        /// 此方法不对URL编码
        /// </summary>
        /// <param name="httpUrl">URL字符串</param>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder OfHttpWithoutEncode(string httpUrl)
        {
            return OfHttp(httpUrl, null);
        }

        /// <summary>
        /// 使用URL字符串构建UrlBuilder，当传入的URL没有协议时，按照http协议对待，编码默认使用UTF-8
        /// </summary>
        /// <param name="httpUrl">URL字符串</param>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder OfHttp(string httpUrl)
        {
            return OfHttp(httpUrl, Encoding.UTF8);
        }

        /// <summary>
        /// 使用URL字符串构建UrlBuilder，当传入的URL没有协议时，按照http协议对待。
        /// </summary>
        /// <param name="httpUrl">URL字符串</param>
        /// <param name="charset">编码，用于URLEncode和URLDecode</param>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder OfHttp(string httpUrl, Encoding charset)
        {
            Assert.NotBlank(httpUrl, "Http url must be not blank!");
            httpUrl = StrUtil.TrimStart(httpUrl);
            // issue#I66CIR
            if (!StrUtil.StartsWithAny(httpUrl, true, "http://", "https://"))
            {
                httpUrl = "http://" + httpUrl;
            }
            return Of(new Uri(httpUrl), charset);
        }

        /// <summary>
        /// 使用URL字符串构建UrlBuilder，默认使用UTF-8编码
        /// </summary>
        /// <param name="url">URL字符串</param>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder Of(string url)
        {
            return Of(url, Encoding.UTF8);
        }

        /// <summary>
        /// 使用URL字符串构建UrlBuilder
        /// </summary>
        /// <param name="url">URL字符串</param>
        /// <param name="charset">编码，用于URLEncode和URLDecode</param>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder Of(string url, Encoding charset)
        {
            Assert.NotBlank(url, "Url must be not blank!");
            return Of(new Uri(StrUtil.Trim(url)), charset);
        }

        /// <summary>
        /// 使用URL构建UrlBuilder
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="charset">编码，用于URLEncode和URLDecode</param>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder Of(Uri url, Encoding charset)
        {
            return Of(url.Scheme, url.Host, url.Port, url.AbsolutePath, url.Query.TrimStart('?'), url.Fragment.TrimStart('#'), charset);
        }

        /// <summary>
        /// 构建UrlBuilder
        /// </summary>
        /// <param name="scheme">协议，默认http</param>
        /// <param name="host">主机，例如127.0.0.1</param>
        /// <param name="port">端口，-1表示默认端口</param>
        /// <param name="path">路径，例如/aa/bb/cc</param>
        /// <param name="query">查询，例如a=1&b=2</param>
        /// <param name="fragment">标识符例如#后边的部分</param>
        /// <param name="charset">编码，用于URLEncode和URLDecode</param>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder Of(string scheme, string host, int port, string path, string query, string fragment, Encoding charset)
        {
            return Of(scheme, host, port,
                    UrlPath.Of(path, charset),
                    UrlQuery.Of(query, charset, false), fragment, charset);
        }

        /// <summary>
        /// 构建UrlBuilder
        /// </summary>
        /// <param name="scheme">协议，默认http</param>
        /// <param name="host">主机，例如127.0.0.1</param>
        /// <param name="port">端口，-1表示默认端口</param>
        /// <param name="path">路径，例如/aa/bb/cc</param>
        /// <param name="query">查询，例如a=1&b=2</param>
        /// <param name="fragment">标识符例如#后边的部分</param>
        /// <param name="charset">编码，用于URLEncode和URLDecode</param>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder Of(string scheme, string host, int port, UrlPath path, UrlQuery query, string fragment, Encoding charset)
        {
            return new UrlBuilder(scheme, host, port, path, query, fragment, charset);
        }

        /// <summary>
        /// 创建空的UrlBuilder
        /// </summary>
        /// <returns>UrlBuilder</returns>
        public static UrlBuilder Of()
        {
            return new UrlBuilder();
        }

        /// <summary>
        /// 构造
        /// </summary>
        public UrlBuilder()
        {
            _charset = Encoding.UTF8;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="scheme">协议，默认http</param>
        /// <param name="host">主机，例如127.0.0.1</param>
        /// <param name="port">端口，-1表示默认端口</param>
        /// <param name="path">路径，例如/aa/bb/cc</param>
        /// <param name="query">查询，例如a=1&b=2</param>
        /// <param name="fragment">标识符例如#后边的部分</param>
        /// <param name="charset">编码，用于URLEncode和URLDecode，{@code null}表示不编码</param>
        public UrlBuilder(string scheme, string host, int port, UrlPath path, UrlQuery query, string fragment, Encoding charset)
        {
            _charset = charset;
            _scheme = scheme;
            _host = host;
            _port = port;
            _path = path;
            _query = query;
            SetFragment(fragment);
            // 编码非空情况下做解码
            _needEncodePercent = charset != null;
        }

        /// <summary>
        /// 获取协议，例如http
        /// </summary>
        /// <returns>协议，例如http</returns>
        public string GetScheme()
        {
            return _scheme;
        }

        /// <summary>
        /// 获取协议，例如http，如果用户未定义协议，使用默认的http协议
        /// </summary>
        /// <returns>协议，例如http</returns>
        public string GetSchemeWithDefault()
        {
            return StrUtil.EmptyToDefault(_scheme, DEFAULT_SCHEME);
        }

        /// <summary>
        /// 设置协议，例如http
        /// </summary>
        /// <param name="scheme">协议，例如http</param>
        /// <returns>this</returns>
        public UrlBuilder SetScheme(string scheme)
        {
            _scheme = scheme;
            return this;
        }

        /// <summary>
        /// 获取 主机，例如127.0.0.1
        /// </summary>
        /// <returns>主机，例如127.0.0.1</returns>
        public string GetHost()
        {
            return _host;
        }

        /// <summary>
        /// 设置主机，例如127.0.0.1
        /// </summary>
        /// <param name="host">主机，例如127.0.0.1</param>
        /// <returns>this</returns>
        public UrlBuilder SetHost(string host)
        {
            _host = host;
            return this;
        }

        /// <summary>
        /// 获取端口，默认-1
        /// </summary>
        /// <returns>端口，默认-1</returns>
        public int GetPort()
        {
            return _port;
        }

        /// <summary>
        /// 获取端口，如果未自定义返回协议默认端口
        /// </summary>
        /// <returns>端口</returns>
        public int GetPortWithDefault()
        {
            int port = GetPort();
            if (port > 0)
            {
                return port;
            }
            var url = ToUri();
            return url.IsDefaultPort ? url.Port : url.Port;
        }

        /// <summary>
        /// 设置端口，默认-1
        /// </summary>
        /// <param name="port">端口，默认-1</param>
        /// <returns>this</returns>
        public UrlBuilder SetPort(int port)
        {
            _port = port;
            return this;
        }

        /// <summary>
        /// 获得authority部分
        /// </summary>
        /// <returns>authority部分</returns>
        public string GetAuthority()
        {
            return (_port < 0) ? _host : $"{_host}:{_port}";
        }

        /// <summary>
        /// 获取路径，例如/aa/bb/cc
        /// </summary>
        /// <returns>路径，例如/aa/bb/cc</returns>
        public UrlPath GetPath()
        {
            return _path;
        }

        /// <summary>
        /// 获得路径，例如/aa/bb/cc
        /// </summary>
        /// <returns>路径，例如/aa/bb/cc</returns>
        public string GetPathStr()
        {
            return _path == null ? StrUtil.SLASH : _path.Build(_charset, _needEncodePercent);
        }

        /// <summary>
        /// 是否path的末尾加 /
        /// </summary>
        /// <param name="withEngTag">是否path的末尾加 /</param>
        /// <returns>this</returns>
        public UrlBuilder SetWithEndTag(bool withEngTag)
        {
            if (_path == null)
            {
                _path = new UrlPath();
            }

            _path.SetWithEndTag(withEngTag);
            return this;
        }

        /// <summary>
        /// 设置路径，例如/aa/bb/cc，将覆盖之前所有的path相关设置
        /// </summary>
        /// <param name="path">路径，例如/aa/bb/cc</param>
        /// <returns>this</returns>
        public UrlBuilder SetPath(UrlPath path)
        {
            _path = path;
            return this;
        }

        /// <summary>
        /// 增加路径，在现有路径基础上追加路径
        /// </summary>
        /// <param name="path">路径，例如aaa/bbb/ccc</param>
        /// <returns>this</returns>
        public UrlBuilder AddPath(CharSequence path)
        {
            UrlPath.Of(path, _charset).GetSegments().ForEach(AddPathSegment);
            return this;
        }

        /// <summary>
        /// 增加路径节点，路径节点中的"/"会被转义为"%2F"
        /// </summary>
        /// <param name="segment">路径节点</param>
        /// <returns>this</returns>
        public UrlBuilder AddPathSegment(CharSequence segment)
        {
            if (StrUtil.IsEmpty(segment))
            {
                return this;
            }
            if (_path == null)
            {
                _path = new UrlPath();
            }
            _path.Add(segment);
            return this;
        }

        /// <summary>
        /// 追加path节点
        /// </summary>
        /// <param name="path">path节点</param>
        /// <returns>this</returns>
        [Obsolete("方法重复，请使用AddPath")]
        public UrlBuilder AppendPath(CharSequence path)
        {
            return AddPath(path);
        }

        /// <summary>
        /// 获取查询语句，例如a=1&b=2
        /// 可能为{@code null}
        /// </summary>
        /// <returns>查询语句，例如a=1&b=2，可能为{@code null}</returns>
        public UrlQuery GetQuery()
        {
            return _query;
        }

        /// <summary>
        /// 获取查询语句，例如a=1&b=2
        /// </summary>
        /// <returns>查询语句，例如a=1&b=2</returns>
        public string GetQueryStr()
        {
            return _query == null ? null : _query.Build(_charset, _needEncodePercent);
        }

        /// <summary>
        /// 设置查询语句，例如a=1&b=2，将覆盖之前所有的query相关设置
        /// </summary>
        /// <param name="query">查询语句，例如a=1&b=2</param>
        /// <returns>this</returns>
        public UrlBuilder SetQuery(UrlQuery query)
        {
            _query = query;
            return this;
        }

        /// <summary>
        /// 添加查询项，支持重复键
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public UrlBuilder AddQuery(string key, object value)
        {
            if (StrUtil.IsEmpty(key))
            {
                return this;
            }

            if (_query == null)
            {
                _query = new UrlQuery();
            }
            _query.Add(key, value);
            return this;
        }

        /// <summary>
        /// 获取标识符，#后边的部分
        /// </summary>
        /// <returns>标识符，例如#后边的部分</returns>
        public string GetFragment()
        {
            return _fragment;
        }

        /// <summary>
        /// 获取标识符，#后边的部分
        /// </summary>
        /// <returns>标识符，例如#后边的部分</returns>
        public string GetFragmentEncoded()
        {
            char[] safeChars = _needEncodePercent ? null : new char[] { '%' };
            return RFC3986.Fragment.Encode(_fragment, _charset, safeChars);
        }

        /// <summary>
        /// 设置标识符，例如#后边的部分
        /// </summary>
        /// <param name="fragment">标识符，例如#后边的部分</param>
        /// <returns>this</returns>
        public UrlBuilder SetFragment(string fragment)
        {
            if (StrUtil.IsEmpty(fragment))
            {
                _fragment = null;
            }
            _fragment = StrUtil.RemovePrefix(fragment, "#");
            return this;
        }

        /// <summary>
        /// 获取编码，用于URLEncode和URLDecode
        /// </summary>
        /// <returns>编码</returns>
        public Encoding GetCharset()
        {
            return _charset;
        }

        /// <summary>
        /// 设置编码，用于URLEncode和URLDecode
        /// </summary>
        /// <param name="charset">编码</param>
        /// <returns>this</returns>
        public UrlBuilder SetCharset(Encoding charset)
        {
            _charset = charset;
            return this;
        }

        /// <summary>
        /// 创建URL字符串
        /// </summary>
        /// <returns>URL字符串</returns>
        public string Build()
        {
            return ToUri().ToString();
        }

        /// <summary>
        /// 转换为{@link Uri} 对象
        /// </summary>
        /// <returns>{@link Uri}</returns>
        public Uri ToUri()
        {
            var fileBuilder = new StringBuilder();

            // path
            fileBuilder.Append(GetPathStr());

            // query
            var query = GetQueryStr();
            if (StrUtil.IsNotBlank(query))
            {
                fileBuilder.Append('?').Append(query);
            }

            // fragment
            if (StrUtil.IsNotBlank(_fragment))
            {
                fileBuilder.Append('#').Append(GetFragmentEncoded());
            }

            try
            {
                return new Uri(new Uri($"{GetSchemeWithDefault()}://{GetAuthority()}"), fileBuilder.ToString());
            }
            catch (UriFormatException e)
            {
                return null;
            }
        }

        public override string ToString()
        {
            return Build();
        }
    }
}