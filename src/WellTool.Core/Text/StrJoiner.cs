using System.Text;
using WellTool.Core.Collection;
using WellTool.Core.Io;

namespace WellTool.Core.Text;

/// <summary>
/// 字符串连接器（拼接器），通过给定的字符串和多个元素，拼接为一个字符串
/// 相较于StringBuilder提供更加灵活的配置
/// </summary>
public class StrJoiner
{
    private StringBuilder? _sb;
    private string? _delimiter;
    private string? _prefix;
    private string? _suffix;
    private bool _wrapElement;
    private NullMode _nullMode = NullMode.NULL_STRING;
    private string _emptyResult = "";
    private bool _hasContent;

    /// <summary>
    /// 使用指定分隔符创建StrJoiner
    /// </summary>
    /// <param name="delimiter">分隔符</param>
    /// <returns>StrJoiner</returns>
    public static StrJoiner Of(string delimiter) => new StrJoiner(delimiter);

    /// <summary>
    /// 使用指定分隔符创建StrJoiner
    /// </summary>
    /// <param name="delimiter">分隔符</param>
    /// <param name="prefix">前缀</param>
    /// <param name="suffix">后缀</param>
    /// <returns>StrJoiner</returns>
    public static StrJoiner Of(string delimiter, string prefix, string suffix) => new StrJoiner(delimiter, prefix, suffix);

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="delimiter">分隔符，null表示无连接符，直接拼接</param>
    public StrJoiner(string? delimiter)
    {
        _delimiter = delimiter;
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="delimiter">分隔符，null表示无连接符，直接拼接</param>
    /// <param name="prefix">前缀</param>
    /// <param name="suffix">后缀</param>
    public StrJoiner(string? delimiter, string? prefix, string? suffix)
    {
        _delimiter = delimiter;
        _prefix = prefix;
        _suffix = suffix;
    }

    /// <summary>
    /// 设置分隔符
    /// </summary>
    /// <param name="delimiter">分隔符</param>
    /// <returns>this</returns>
    public StrJoiner SetDelimiter(string? delimiter)
    {
        _delimiter = delimiter;
        return this;
    }

    /// <summary>
    /// 设置前缀
    /// </summary>
    /// <param name="prefix">前缀</param>
    /// <returns>this</returns>
    public StrJoiner SetPrefix(string? prefix)
    {
        _prefix = prefix;
        return this;
    }

    /// <summary>
    /// 设置后缀
    /// </summary>
    /// <param name="suffix">后缀</param>
    /// <returns>this</returns>
    public StrJoiner SetSuffix(string? suffix)
    {
        _suffix = suffix;
        return this;
    }

    /// <summary>
    /// 设置前缀和后缀是否包装每个元素
    /// </summary>
    /// <param name="wrapElement">true表示包装每个元素，false包装整个字符串</param>
    /// <returns>this</returns>
    public StrJoiner SetWrapElement(bool wrapElement)
    {
        _wrapElement = wrapElement;
        return this;
    }

    /// <summary>
    /// 设置null元素处理逻辑
    /// </summary>
    /// <param name="nullMode">逻辑枚举</param>
    /// <returns>this</returns>
    public StrJoiner SetNullMode(NullMode nullMode)
    {
        _nullMode = nullMode;
        return this;
    }

    /// <summary>
    /// 追加对象到拼接器中
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns>this</returns>
    public StrJoiner Append(object? obj)
    {
        if (obj == null)
        {
            return Append((string?)null);
        }
        return Append(obj.ToString());
    }

    /// <summary>
    /// 追加字符串到拼接器中
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>this</returns>
    public StrJoiner Append(string? str)
    {
        if (str == null)
        {
            switch (_nullMode)
            {
                case NullMode.IGNORE:
                    return this;
                case NullMode.TO_EMPTY:
                    str = "";
                    break;
                case NullMode.NULL_STRING:
                    str = "null";
                    break;
            }
        }

        Prepare();
        if (_wrapElement && !string.IsNullOrEmpty(_prefix))
        {
            _sb!.Append(_prefix);
        }
        _sb!.Append(str);
        if (_wrapElement && !string.IsNullOrEmpty(_suffix))
        {
            _sb.Append(_suffix);
        }
        return this;
    }

    /// <summary>
    /// 追加字符到拼接器中
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>this</returns>
    public StrJoiner Append(char c) => Append(c.ToString());

    /// <summary>
    /// 合并一个StrJoiner到当前的StrJoiner
    /// </summary>
    /// <param name="strJoiner">其他的StrJoiner</param>
    /// <returns>this</returns>
    public StrJoiner Merge(StrJoiner strJoiner)
    {
        if (strJoiner != null && strJoiner._sb != null)
        {
            var otherStr = strJoiner.ToString();
            if (strJoiner._wrapElement)
            {
                Append(otherStr);
            }
            else
            {
                Append(otherStr);
            }
        }
        return this;
    }

    /// <summary>
    /// 长度
    /// </summary>
    /// <returns>长度</returns>
    public int Length()
    {
        return _sb != null ? _sb.Length : 0;
    }

    /// <summary>
    /// 转为字符串
    /// </summary>
    /// <returns>字符串</returns>
    public override string ToString()
    {
        if (_sb == null)
        {
            return _emptyResult;
        }

        string result = _sb.ToString();
        if (!_wrapElement && !string.IsNullOrEmpty(_suffix))
        {
            result += _suffix;
        }
        return result;
    }

    private void Prepare()
    {
        if (_hasContent)
        {
            if (_delimiter != null)
            {
                _sb!.Append(_delimiter);
            }
        }
        else
        {
            _sb ??= new StringBuilder();
            if (!_wrapElement && !string.IsNullOrEmpty(_prefix))
            {
                _sb.Append(_prefix);
            }
            _hasContent = true;
        }
    }

    /// <summary>
    /// null处理的模式
    /// </summary>
    public enum NullMode
    {
        /// <summary>
        /// 忽略null，即null元素不加入拼接的字符串
        /// </summary>
        IGNORE,
        /// <summary>
        /// null转为""
        /// </summary>
        TO_EMPTY,
        /// <summary>
        /// null转为null字符串
        /// </summary>
        NULL_STRING
    }
}
