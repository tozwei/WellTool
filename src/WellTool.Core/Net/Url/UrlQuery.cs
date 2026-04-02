using System;
using System.Collections.Generic;
using System.Text;
using WellTool.Core.Codec;
using WellTool.Core.Util;

namespace WellTool.Core.Net.Url
{
    /// <summary>
    /// URL中查询字符串部分的封装，类似于：
    /// <pre>
    ///   key1=v1&amp;key2=&amp;key3=v3
    /// </pre>
    /// 查询封装分为解析查询字符串和构建查询字符串，解析可通过charset为null来自定义是否decode编码后的内容，<br>
    /// 构建则通过charset是否为null是否encode参数键值对
    /// </summary>
    public class UrlQuery
    {
        private readonly Dictionary<string, string> _query;
        /// <summary>
        /// 是否为x-www-form-urlencoded模式，此模式下空格会编码为'+'
        /// </summary>
        private readonly bool _isFormUrlEncoded;
        /// <summary>
        /// 是否严格模式，严格模式下，query的name和value中均不允许有分隔符。
        /// </summary>
        private bool _isStrict;

        /// <summary>
        /// 构建UrlQuery
        /// </summary>
        /// <param name="queryMap">初始化的查询键值对</param>
        /// <returns>UrlQuery</returns>
        public static UrlQuery Of(Dictionary<string, object> queryMap)
        {
            return new UrlQuery(queryMap);
        }

        /// <summary>
        /// 构建UrlQuery
        /// </summary>
        /// <param name="queryMap">初始化的查询键值对</param>
        /// <param name="isFormUrlEncoded">是否为x-www-form-urlencoded模式，此模式下空格会编码为'+'</param>
        /// <returns>UrlQuery</returns>
        public static UrlQuery Of(Dictionary<string, object> queryMap, bool isFormUrlEncoded)
        {
            return new UrlQuery(queryMap, isFormUrlEncoded);
        }

        /// <summary>
        /// 构建UrlQuery
        /// </summary>
        /// <param name="queryStr">初始化的查询字符串</param>
        /// <param name="charset">decode用的编码，null表示不做decode</param>
        /// <returns>UrlQuery</returns>
        public static UrlQuery Of(string queryStr, Encoding charset)
        {
            return Of(queryStr, charset, true);
        }

        /// <summary>
        /// 构建UrlQuery
        /// </summary>
        /// <param name="queryStr">初始化的查询字符串</param>
        /// <param name="charset">decode用的编码，null表示不做decode</param>
        /// <param name="autoRemovePath">是否自动去除path部分，{@code true}则自动去除第一个?前的内容</param>
        /// <returns>UrlQuery</returns>
        public static UrlQuery Of(string queryStr, Encoding charset, bool autoRemovePath)
        {
            return Of(queryStr, charset, autoRemovePath, false);
        }

        /// <summary>
        /// 构建UrlQuery
        /// </summary>
        /// <param name="queryStr">初始化的查询字符串</param>
        /// <param name="charset">decode用的编码，null表示不做decode</param>
        /// <param name="autoRemovePath">是否自动去除path部分，{@code true}则自动去除第一个?前的内容</param>
        /// <param name="isFormUrlEncoded">是否为x-www-form-urlencoded模式，此模式下空格会编码为'+'</param>
        /// <returns>UrlQuery</returns>
        public static UrlQuery Of(string queryStr, Encoding charset, bool autoRemovePath, bool isFormUrlEncoded)
        {
            return new UrlQuery(isFormUrlEncoded).Parse(queryStr, charset, autoRemovePath);
        }

        /// <summary>
        /// 构造
        /// </summary>
        public UrlQuery() : this(null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="isFormUrlEncoded">是否为x-www-form-urlencoded模式，此模式下空格会编码为'+'</param>
        public UrlQuery(bool isFormUrlEncoded) : this(null, isFormUrlEncoded)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="queryMap">初始化的查询键值对</param>
        public UrlQuery(Dictionary<string, object> queryMap) : this(queryMap, false)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="queryMap">初始化的查询键值对</param>
        /// <param name="isFormUrlEncoded">是否为x-www-form-urlencoded模式，此模式下空格会编码为'+'</param>
        public UrlQuery(Dictionary<string, object> queryMap, bool isFormUrlEncoded)
        {
            _query = new Dictionary<string, string>();
            _isFormUrlEncoded = isFormUrlEncoded;
            if (queryMap != null && queryMap.Count > 0)
            {
                AddAll(queryMap);
            }
        }

        /// <summary>
        /// 设置是否严格模式
        /// </summary>
        /// <param name="strict">是否严格模式</param>
        /// <returns>this</returns>
        public UrlQuery SetStrict(bool strict)
        {
            _isStrict = strict;
            return this;
        }

        /// <summary>
        /// 增加键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值，集合和数组转换为逗号分隔形式</param>
        /// <returns>this</returns>
        public UrlQuery Add(string key, object value)
        {
            _query[key] = ToStr(value);
            return this;
        }

        /// <summary>
        /// 批量增加键值对
        /// </summary>
        /// <param name="queryMap">query中的键值对</param>
        /// <returns>this</returns>
        public UrlQuery AddAll(Dictionary<string, object> queryMap)
        {
            if (queryMap != null && queryMap.Count > 0)
            {
                foreach (var entry in queryMap)
                {
                    Add(entry.Key, entry.Value);
                }
            }
            return this;
        }

        /// <summary>
        /// 移除键及对应所有的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>this</returns>
        public UrlQuery Remove(string key)
        {
            _query.Remove(key);
            return this;
        }

        /// <summary>
        /// 解析URL中的查询字符串
        /// </summary>
        /// <param name="queryStr">查询字符串，类似于key1=v1&amp;key2=&amp;key3=v3</param>
        /// <param name="charset">decode编码，null表示不做decode</param>
        /// <returns>this</returns>
        public UrlQuery Parse(string queryStr, Encoding charset)
        {
            return Parse(queryStr, charset, true);
        }

        /// <summary>
        /// 解析URL中的查询字符串
        /// </summary>
        /// <param name="queryStr">查询字符串，类似于key1=v1&amp;key2=&amp;key3=v3</param>
        /// <param name="charset">decode编码，null表示不做decode</param>
        /// <param name="autoRemovePath">是否自动去除path部分，{@code true}则自动去除第一个?前的内容</param>
        /// <returns>this</returns>
        public UrlQuery Parse(string queryStr, Encoding charset, bool autoRemovePath)
        {
            if (StrUtil.IsBlank(queryStr))
            {
                return this;
            }

            if (autoRemovePath)
            {
                // 去掉Path部分
                int pathEndPos = queryStr.IndexOf('?');
                if (pathEndPos > -1)
                {
                    queryStr = queryStr.Substring(pathEndPos + 1);
                    if (StrUtil.IsBlank(queryStr))
                    {
                        return this;
                    }
                }
                else if (queryStr.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || queryStr.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    // 用户传入只有URL，没有param部分，返回空
                    return this;
                }
            }

            return DoParse(queryStr, charset);
        }

        /// <summary>
        /// 获得查询的Map
        /// </summary>
        /// <returns>查询的Map</returns>
        public Dictionary<string, string> GetQueryMap()
        {
            return new Dictionary<string, string>(_query);
        }

        /// <summary>
        /// 获取查询值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public string Get(string key)
        {
            if (_query.Count == 0)
            {
                return null;
            }
            _query.TryGetValue(key, out var value);
            return value;
        }

        /// <summary>
        /// 构建URL查询字符串，即将key-value键值对转换为{@code key1=v1&key2=v2&key3=v3}形式。<br>
        /// 对于{@code null}处理规则如下：
        /// <ul>
        ///     <li>如果key为{@code null}，则这个键值对忽略</li>
        ///     <li>如果value为{@code null}，只保留key，如key1对应value为{@code null}生成类似于{@code key1&key2=v2}形式</li>
        /// </ul>
        /// </summary>
        /// <param name="charset">encode编码，null表示不做encode编码</param>
        /// <returns>URL查询字符串</returns>
        public string Build(Encoding charset)
        {
            return Build(charset, true);
        }

        /// <summary>
        /// 构建URL查询字符串，即将key-value键值对转换为{@code key1=v1&key2=v2&key3=v3}形式。<br>
        /// 对于{@code null}处理规则如下：
        /// <ul>
        ///     <li>如果key为{@code null}，则这个键值对忽略</li>
        ///     <li>如果value为{@code null}，只保留key，如key1对应value为{@code null}生成类似于{@code key1&key2=v2}形式</li>
        /// </ul>
        /// </summary>
        /// <param name="charset">encode编码，null表示不做encode编码</param>
        /// <param name="encodePercent">是否编码`%`</param>
        /// <returns>URL查询字符串</returns>
        public string Build(Encoding charset, bool encodePercent)
        {
            if (_isFormUrlEncoded)
            {
                return Build(FormUrlencoded.All, FormUrlencoded.All, charset, encodePercent);
            }

            if (_isStrict)
            {
                return Build(RFC3986.QUERY_PARAM_NAME_STRICT, RFC3986.QUERY_PARAM_VALUE_STRICT, charset, encodePercent);
            }
            return Build(RFC3986.QUERY_PARAM_NAME, RFC3986.QUERY_PARAM_VALUE, charset, encodePercent);
        }

        /// <summary>
        /// 构建URL查询字符串，即将key-value键值对转换为{@code key1=v1&key2=v2&key3=v3}形式。<br>
        /// 对于{@code null}处理规则如下：
        /// <ul>
        ///     <li>如果key为{@code null}，则这个键值对忽略</li>
        ///     <li>如果value为{@code null}，只保留key，如key1对应value为{@code null}生成类似于{@code key1&key2=v2}形式</li>
        /// </ul>
        /// </summary>
        /// <param name="keyCoder">键值对中键的编码器</param>
        /// <param name="valueCoder">键值对中值的编码器</param>
        /// <param name="charset">encode编码，null表示不做encode编码</param>
        /// <returns>URL查询字符串</returns>
        public string Build(PercentCodec keyCoder, PercentCodec valueCoder, Encoding charset)
        {
            return Build(keyCoder, valueCoder, charset, true);
        }

        /// <summary>
        /// 构建URL查询字符串，即将key-value键值对转换为{@code key1=v1&key2=v2&key3=v3}形式。<br>
        /// 对于{@code null}处理规则如下：
        /// <ul>
        ///     <li>如果key为{@code null}，则这个键值对忽略</li>
        ///     <li>如果value为{@code null}，只保留key，如key1对应value为{@code null}生成类似于{@code key1&key2=v2}形式</li>
        /// </ul>
        /// </summary>
        /// <param name="keyCoder">键值对中键的编码器</param>
        /// <param name="valueCoder">键值对中值的编码器</param>
        /// <param name="charset">encode编码，null表示不做encode编码</param>
        /// <param name="encodePercent">是否编码`%`</param>
        /// <returns>URL查询字符串</returns>
        public string Build(PercentCodec keyCoder, PercentCodec valueCoder, Encoding charset, bool encodePercent)
        {
            if (_query.Count == 0)
            {
                return StrUtil.Empty;
            }

            char[] safeChars = encodePercent ? null : new char[] { '%' };
            StringBuilder sb = new StringBuilder();
            foreach (var entry in _query)
            {
                string name = entry.Key;
                if (name != null)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("&");
                    }
                    sb.Append(keyCoder.Encode(name, charset, safeChars));
                    string value = entry.Value;
                    if (value != null)
                    {
                        sb.Append("=").Append(valueCoder.Encode(value, charset, safeChars));
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成查询字符串，类似于aaa=111&amp;bbb=222<br>
        /// 此方法不对任何特殊字符编码，仅用于输出显示
        /// </summary>
        /// <returns>查询字符串</returns>
        public override string ToString()
        {
            return Build(null);
        }

        /// <summary>
        /// 解析URL中的查询字符串<br>
        /// 规则见：https://url.spec.whatwg.org/#urlencoded-parsing
        /// </summary>
        /// <param name="queryStr">查询字符串，类似于key1=v1&amp;key2=&amp;key3=v3</param>
        /// <param name="charset">decode编码，null表示不做decode</param>
        /// <returns>this</returns>
        private UrlQuery DoParse(string queryStr, Encoding charset)
        {
            int len = queryStr.Length;
            string name = null;
            int pos = 0; // 未处理字符开始位置
            for (int i = 0; i < len; i++)
            {
                char c = queryStr[i];
                switch (c)
                {
                    case '=': // 键和值的分界符
                        if (name == null)
                        {
                            // name可以是""
                            name = queryStr.Substring(pos, i - pos);
                            // 开始位置从分节符后开始
                            pos = i + 1;
                        }
                        // 当=不作为分界符时，按照普通字符对待
                        break;
                    case '&': // 键值对之间的分界符
                        AddParam(name, queryStr.Substring(pos, i - pos), charset);
                        name = null;
                        if (i + 4 < len && queryStr.Substring(i + 1, 4) == "amp;")
                        {
                            // "&amp;"转义为"&"
                            i += 4;
                        }
                        // 开始位置从分节符后开始
                        pos = i + 1;
                        break;
                }
            }

            // 处理结尾
            AddParam(name, queryStr.Substring(pos), charset);

            return this;
        }

        /// <summary>
        /// 对象转换为字符串，用于URL的Query中
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字符串</returns>
        private static string ToStr(object value)
        {
            if (value == null)
            {
                return null;
            }
            if (value is System.Collections.IEnumerable enumerable && !(value is string))
            {
                return StrUtil.Join(",", enumerable);
            }
            return value.ToString();
        }

        /// <summary>
        /// 将键值对加入到Map中,，情况如下：
        /// <pre>
        ///     1、key和value都不为null，类似于 "a=1"或者"=1"，直接put
        ///     2、key不为null，value为null，类似于 "a="，值传""
        ///     3、key为null，value不为null，类似于 "1"
        ///     4、key和value都为null，忽略之，比如&&
        /// </pre>
        /// </summary>
        /// <param name="key">key，为null则value作为key</param>
        /// <param name="value">value，为null且key不为null时传入""
        /// <param name="charset">编码</param>
        private void AddParam(string key, string value, Encoding charset)
        {
            if (key != null)
            {
                string actualKey = URLDecoder.Decode(key, charset, _isFormUrlEncoded);
                _query[actualKey] = StrUtil.NullToEmpty(URLDecoder.Decode(value, charset, _isFormUrlEncoded));
            }
            else if (value != null)
            {
                // name为空，value作为name，value赋值null
                _query[URLDecoder.Decode(value, charset, _isFormUrlEncoded)] = null;
            }
        }
    }
}