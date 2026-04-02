using System;
using System.Collections.Generic;
using System.Text;
using WellTool.Core.Collection;
using WellTool.Core.Lang;
using WellTool.Core.Util;

namespace WellTool.Core.Net.Url
{
    /// <summary>
    /// URL中Path部分的封装
    /// </summary>
    public class UrlPath
    {
        private List<string> _segments;
        private bool _withEndTag;

        /// <summary>
        /// 构建UrlPath
        /// </summary>
        /// <param name="pathStr">初始化的路径字符串</param>
        /// <param name="charset">decode用的编码，null表示不做decode</param>
        /// <returns>UrlPath</returns>
        public static UrlPath Of(CharSequence pathStr, Encoding charset)
        {
            var urlPath = new UrlPath();
            urlPath.Parse(pathStr, charset);
            return urlPath;
        }

        /// <summary>
        /// 是否path的末尾加 /
        /// </summary>
        /// <param name="withEngTag">是否path的末尾加 /</param>
        /// <returns>this</returns>
        public UrlPath SetWithEndTag(bool withEngTag)
        {
            _withEndTag = withEngTag;
            return this;
        }

        /// <summary>
        /// 获取path的节点列表
        /// </summary>
        /// <returns>节点列表</returns>
        public List<string> GetSegments()
        {
            return _segments ?? CollUtil.EmptyList<string>();
        }

        /// <summary>
        /// 获得指定节点
        /// </summary>
        /// <param name="index">节点位置</param>
        /// <returns>节点，无节点或者越界返回null</returns>
        public string GetSegment(int index)
        {
            if (_segments == null || index >= _segments.Count)
            {
                return null;
            }
            return _segments[index];
        }

        /// <summary>
        /// 添加到path最后面
        /// </summary>
        /// <param name="segment">Path节点</param>
        /// <returns>this</returns>
        public UrlPath Add(CharSequence segment)
        {
            AddInternal(FixPath(segment), false);
            return this;
        }

        /// <summary>
        /// 添加到path最前面
        /// </summary>
        /// <param name="segment">Path节点</param>
        /// <returns>this</returns>
        public UrlPath AddBefore(CharSequence segment)
        {
            AddInternal(FixPath(segment), true);
            return this;
        }

        /// <summary>
        /// 解析path
        /// </summary>
        /// <param name="path">路径，类似于aaa/bb/ccc或/aaa/bbb/ccc</param>
        /// <param name="charset">decode编码，null表示不解码</param>
        /// <returns>this</returns>
        public UrlPath Parse(CharSequence path, Encoding charset)
        {
            if (StrUtil.IsNotEmpty(path))
            {
                // 原URL中以/结尾，则这个规则需保留，issue#I1G44J@Gitee
                if (StrUtil.EndWith(path, '/'))
                {
                    _withEndTag = true;
                }

                path = FixPath(path);
                if (StrUtil.IsNotEmpty(path))
                {
                    var split = StrUtil.Split(path, '/');
                    foreach (var seg in split)
                    {
                        AddInternal(URLDecoder.DecodeForPath(seg, charset), false);
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// 构建path，前面带'/'
        /// <pre>
        ///     path = path-abempty / path-absolute / path-noscheme / path-rootless / path-empty
        /// </pre>
        /// </summary>
        /// <param name="charset">encode编码，null表示不做encode</param>
        /// <returns>如果没有任何内容，则返回空字符串""</returns>
        public string Build(Encoding charset)
        {
            return Build(charset, true);
        }

        /// <summary>
        /// 构建path，前面带'/'
        /// <pre>
        ///     path = path-abempty / path-absolute / path-noscheme / path-rootless / path-empty
        /// </pre>
        /// </summary>
        /// <param name="charset">encode编码，null表示不做encode</param>
        /// <param name="encodePercent">是否编码`%`</param>
        /// <returns>如果没有任何内容，则返回空字符串""</returns>
        public string Build(Encoding charset, bool encodePercent)
        {
            if (CollUtil.IsEmpty(_segments))
            {
                // 没有节点的path取决于是否末尾追加/，如果不追加返回空串，否则返回/
                return _withEndTag ? StrUtil.SLASH : StrUtil.EMPTY;
            }

            char[] safeChars = encodePercent ? null : new char[] { '%' };
            var builder = new StringBuilder();
            foreach (var segment in _segments)
            {
                // https://www.ietf.org/rfc/rfc3986.html#section-3.3
                // 此处Path中是允许有`:`的，之前理解有误，应该是相对URI的第一个segment中不允许有`:`
                builder.Append('/').Append(RFC3986.Segment.Encode(segment, charset, safeChars));
            }

            if (_withEndTag)
            {
                if (StrUtil.IsEmpty(builder))
                {
                    // 空白追加是保证以/开头
                    builder.Append('/');
                }
                else if (!StrUtil.EndWith(builder, '/'))
                {
                    // 尾部没有/则追加，否则不追加
                    builder.Append('/');
                }
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            return Build(null);
        }

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="segment">节点</param>
        /// <param name="before">是否在前面添加</param>
        private void AddInternal(CharSequence segment, bool before)
        {
            if (_segments == null)
            {
                _segments = new List<string>();
            }

            var seg = StrUtil.Str(segment);
            if (before)
            {
                _segments.Insert(0, seg);
            }
            else
            {
                _segments.Add(seg);
            }
        }

        /// <summary>
        /// 修正路径，包括去掉前后的/，去掉空白符
        /// </summary>
        /// <param name="path">节点或路径path</param>
        /// <returns>修正后的路径</returns>
        private static string FixPath(CharSequence path)
        {
            Assert.NotNull(path, "Path segment must be not null!");
            if ("/".Equals(path.ToString()))
            {
                return StrUtil.EMPTY;
            }

            string segmentStr = StrUtil.Trim(path);
            segmentStr = StrUtil.RemovePrefix(segmentStr, StrUtil.SLASH);
            segmentStr = StrUtil.RemoveSuffix(segmentStr, StrUtil.SLASH);
            segmentStr = StrUtil.Trim(segmentStr);
            return segmentStr;
        }
    }
}