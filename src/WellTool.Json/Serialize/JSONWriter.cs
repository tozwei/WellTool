using System.IO;
using System.Text;
using WellTool.Core.Date;
using WellTool.Core.Util;
using WellTool.Core.Math;
using WellTool.Core.Lang;
using WellTool.Json;

namespace WellTool.Json.Serialize;

/// <summary>
/// JSON数据写出器<br>
/// 通过简单的append方式将JSON的键值对等信息写出到<see cref="TextWriter"/>中。
/// </summary>
public class JSONWriter : TextWriter
{
    /// <summary>
    /// 缩进因子，定义每一级别增加的缩进量
    /// </summary>
    private readonly int _indentFactor;
    /// <summary>
    /// 本级别缩进量
    /// </summary>
    private readonly int _indent;
    /// <summary>
    /// TextWriter
    /// </summary>
    private readonly TextWriter _writer;
    /// <summary>
    /// JSON选项
    /// </summary>
    private readonly JSONConfig _config;

    /// <summary>
    /// 写出当前值是否需要分隔符
    /// </summary>
    private bool _needSeparator;
    /// <summary>
    /// 是否为JSONArray模式
    /// </summary>
    private bool _arrayMode;

    /// <summary>
    /// 创建JSONWriter
    /// </summary>
    /// <param name="writer"><see cref="TextWriter"/></param>
    /// <param name="indentFactor">缩进因子，定义每一级别增加的缩进量</param>
    /// <param name="indent">本级别缩进量</param>
    /// <param name="config">JSON选项</param>
    /// <returns>JSONWriter</returns>
    public static JSONWriter Of(TextWriter writer, int indentFactor, int indent, JSONConfig config)
    {
        return new JSONWriter(writer, indentFactor, indent, config);
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="writer"><see cref="TextWriter"/></param>
    /// <param name="indentFactor">缩进因子，定义每一级别增加的缩进量</param>
    /// <param name="indent">本级别缩进量</param>
    /// <param name="config">JSON选项</param>
    public JSONWriter(TextWriter writer, int indentFactor, int indent, JSONConfig config)
    {
        _writer = writer;
        _indentFactor = indentFactor;
        _indent = indent;
        _config = config;
    }

    /// <summary>
    /// JSONObject写出开始，默认写出"{"
    /// </summary>
    /// <returns>this</returns>
    public JSONWriter BeginObj()
    {
        WriteRaw('{');
        return this;
    }

    /// <summary>
    /// JSONArray写出开始，默认写出"["
    /// </summary>
    /// <returns>this</returns>
    public JSONWriter BeginArray()
    {
        WriteRaw('[');
        _arrayMode = true;
        return this;
    }

    /// <summary>
    /// 结束，默认根据开始的类型，补充"}"或"]"
    /// </summary>
    /// <returns>this</returns>
    public JSONWriter End()
    {
        // 换行缩进
        WriteLF().WriteSpace(_indent);
        WriteRaw(_arrayMode ? ']' : '}');
        Flush();
        _arrayMode = false;
        // 当前对象或数组结束，当新的
        _needSeparator = true;
        return this;
    }

    /// <summary>
    /// 写出键，自动处理分隔符和缩进，并包装键名
    /// </summary>
    /// <param name="key">键名</param>
    /// <returns>this</returns>
    public JSONWriter WriteKey(string key)
    {
        if (_needSeparator)
        {
            WriteRaw(',');
        }
        // 换行缩进
        WriteLF().WriteSpace(_indentFactor + _indent);
        return WriteRaw(JSONUtil.Quote(key));
    }

    /// <summary>
    /// 写出值，自动处理分隔符和缩进，自动判断类型，并根据不同类型写出特定格式的值<br>
    /// 如果写出的值为{@code null}或者{@link JSONNull}，且配置忽略null，则跳过。
    /// </summary>
    /// <param name="value">值</param>
    /// <returns>this</returns>
    public JSONWriter WriteValue(object value)
    {
        if (JSONUtil.IsNull(value) && _config.IsIgnoreNullValue())
        {
            return this;
        }
        return WriteValueDirect(value, null);
    }

    /// <summary>
    /// 写出字段名及字段值，如果字段值是{@code null}且忽略null值，则不写出任何内容
    /// </summary>
    /// <param name="key">字段名</param>
    /// <param name="value">字段值</param>
    /// <returns>this</returns>
    public JSONWriter WriteField(string key, object value)
    {
        if (JSONUtil.IsNull(value) && _config.IsIgnoreNullValue())
        {
            return this;
        }
        return WriteKey(key).WriteValueDirect(value, null);
    }

    /// <summary>
    /// 写出字段名及字段值，如果字段值是{@code null}且忽略null值，则不写出任何内容
    /// </summary>
    /// <param name="pair">键值对</param>
    /// <param name="filter">键值对的过滤器，可以编辑键值对</param>
    /// <returns>this</returns>
    public JSONWriter WriteField(KeyValuePair<object, object> pair, Func<KeyValuePair<object, object>, bool> filter)
    {
        if (JSONUtil.IsNull(pair.Value) && _config.IsIgnoreNullValue())
        {
            return this;
        }

        if (filter == null || filter(pair))
        {
            if (!_arrayMode)
            {
                // JSONArray只写值，JSONObject写键值对
                WriteKey(pair.Key.ToString());
            }
            return WriteValueDirect(pair.Value, filter);
        }
        return this;
    }

    /// <summary>
    /// 写入字符数组的一部分
    /// </summary>
    /// <param name="buffer">字符数组</param>
    /// <param name="index">开始索引</param>
    /// <param name="count">写入字符数</param>
    public override void Write(char[] buffer, int index, int count)
    {
        _writer.Write(buffer, index, count);
    }

    /// <summary>
    /// 刷新缓冲区
    /// </summary>
    public override void Flush()
    {
        _writer.Flush();
    }

    /// <summary>
    /// 关闭写入器
    /// </summary>
    public override void Close()
    {
        _writer.Close();
    }

    /// <summary>
    /// 获取编码
    /// </summary>
    public override Encoding Encoding => _writer.Encoding;

    // ------------------------------------------------------------------------------ Private methods
    /// <summary>
    /// 写出值，自动处理分隔符和缩进，自动判断类型，并根据不同类型写出特定格式的值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="filter">键值对过滤器</param>
    /// <returns>this</returns>
    private JSONWriter WriteValueDirect(object value, Func<KeyValuePair<object, object>, bool> filter)
    {
        if (_arrayMode)
        {
            if (_needSeparator)
            {
                WriteRaw(',');
            }
            // 换行缩进
            WriteLF().WriteSpace(_indentFactor + _indent);
        }
        else
        {
            WriteRaw(':').WriteSpace(1);
        }
        _needSeparator = true;
        return WriteObjValue(value, filter);
    }

    /// <summary>
    /// 写出JSON的值，根据值类型不同，输出不同内容
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="filter">过滤器</param>
    /// <returns>this</returns>
    private JSONWriter WriteObjValue(object value, Func<KeyValuePair<object, object>, bool> filter)
    {
        var indent = _indentFactor + _indent;
        if (value == null || value is JSONNull)
        {
            WriteRaw(JSONNull.NULL.ToString());
        }
        else if (value is JSONObject jsonObject)
        {
            jsonObject.Write(_writer, _indentFactor, indent);
        }
        else if (value is JSONArray jsonArray)
        {
            jsonArray.Write(_writer, _indentFactor, indent);
        }
        else if (value is System.Collections.IDictionary || value is System.Collections.DictionaryEntry)
        {
            new JSONObject(value).Write(_writer, _indentFactor, indent);
        }
        else if (value is System.Collections.IEnumerable && !(value is string))
        {
            new JSONArray(value).Write(_writer, _indentFactor, indent);
        }
        else if (value is System.Collections.IEnumerator)
        {
            new JSONArray(value).Write(_writer, _indentFactor, indent);
        }
        else if (value is System.Array)
        {
            new JSONArray(value).Write(_writer, _indentFactor, indent);
        }
        else if (value is byte || value is sbyte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong || value is float || value is double || value is decimal)
        {
            // 避免设置日期格式后writeLongAsString失效
            if (value is long && _config.IsWriteLongAsString())
            {
                // long可能溢出，此时可选是否将long写出为字符串类型
                WriteStrValue(value.ToString());
            }
            else
            {
                WriteNumberValue(Convert.ToDouble(value));
            }
        }
        else if (value is DateTime || value is DateTimeOffset || value is System.TimeSpan)
        {
            var format = _config?.GetDateFormat();
            WriteRaw(FormatDate(value, format));
        }
        else if (value is bool)
        {
            WriteBooleanValue((bool)value);
        }
        else if (value is JSONString)
        {
            WriteJSONStringValue((JSONString)value);
        }
        else
        {
            WriteStrValue(value.ToString());
        }

        return this;
    }

    /// <summary>
    /// 写出数字，根据<see cref="JSONConfig.IsStripTrailingZeros"/> 配置不同，写出不同数字<br>
    /// 主要针对Double型是否去掉小数点后多余的0<br>
    /// 此方法输出的值不包装引号。
    /// </summary>
    /// <param name="number">数字</param>
    private void WriteNumberValue(double number)
    {
        // 可配置是否去除末尾多余0，例如如果为true,5.0返回5
        var isStripTrailingZeros = _config == null || _config.IsStripTrailingZeros();
        WriteRaw(number.ToString());
    }

    /// <summary>
    /// 写出Boolean值，直接写出true或false,不适用引号包装
    /// </summary>
    /// <param name="value">Boolean值</param>
    private void WriteBooleanValue(bool value)
    {
        WriteRaw(value.ToString().ToLower());
    }

    /// <summary>
    /// 输出实现了<see cref="JSONString"/>接口的对象，通过调用<see cref="JSONString.ToJSONString()"/>获取JSON字符串<br>
    /// <see cref="JSONString"/>按照JSON对象对待，此方法输出的JSON字符串不包装引号。<br>
    /// 如果ToJSONString()返回null，调用ToString()方法并使用双引号包装。
    /// </summary>
    /// <param name="jsonString"><see cref="JSONString"/></param>
    private void WriteJSONStringValue(JSONString jsonString)
    {
        string valueStr;
        try
        {
            valueStr = jsonString.ToJSONString();
        }
        catch (Exception e)
        {
            throw new JSONException(e.Message);
        }
        if (valueStr != null)
        {
            WriteRaw(valueStr);
        }
        else
        {
            WriteStrValue(jsonString.ToString());
        }
    }

    /// <summary>
    /// 写出字符串值，并包装引号并转义字符<br>
    /// 对所有双引号做转义处理（使用双反斜杠做转义）<br>
    /// 为了能在HTML中较好的显示，会将&lt;/转义为&lt;<br>
    /// JSON字符串中不能包含控制字符和未经转义的引号和反斜杠
    /// </summary>
    /// <param name="value">字符串</param>
    private void WriteStrValue(string value)
    {
        WriteRaw(JSONUtil.Quote(value));
    }

    /// <summary>
    /// 写出空格
    /// </summary>
    /// <param name="count">空格数</param>
    private void WriteSpace(int count)
    {
        if (_indentFactor > 0)
        {
            for (int i = 0; i < count; i++)
            {
                WriteRaw(' ');
            }
        }
    }

    /// <summary>
    /// 写出换换行符
    /// </summary>
    /// <returns>this</returns>
    private JSONWriter WriteLF()
    {
        if (_indentFactor > 0)
        {
            WriteRaw('\n');
        }
        return this;
    }

    /// <summary>
    /// 写入原始字符串值，不做任何处理
    /// </summary>
    /// <param name="value">字符串</param>
    /// <returns>this</returns>
    private JSONWriter WriteRaw(string value)
    {
        _writer.Write(value);
        return this;
    }

    /// <summary>
    /// 写入原始字符值，不做任何处理
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>this</returns>
    private JSONWriter WriteRaw(char c)
    {
        _writer.Write(c);
        return this;
    }

    /// <summary>
    /// 按照给定格式格式化日期，格式为空时返回时间戳字符串
    /// </summary>
    /// <param name="dateObj">日期对象</param>
    /// <param name="format">格式</param>
    /// <returns>日期字符串</returns>
    private static string FormatDate(object dateObj, string format)
    {
        if (!string.IsNullOrEmpty(format))
        {
            string dateStr;
            if (dateObj is DateTime dateTime)
            {
                dateStr = WellTool.Core.Date.DateUtil.Format(dateTime, format);
            }
            else if (dateObj is DateTimeOffset dateTimeOffset)
            {
                dateStr = WellTool.Core.Date.DateUtil.Format(dateTimeOffset.DateTime, format);
            }
            else
            {
                dateStr = dateObj.ToString();
            }

            if (format == "yyyy-MM-dd HH:mm:ss" || format == "yyyy-MM-dd HH:mm:ss.SSS")
            {
                // Hutool自定义的秒和毫秒表示，默认不包装双引号
                return dateStr;
            }
            //用户定义了日期格式
            return JSONUtil.Quote(dateStr);
        }

        //默认使用时间戳
        long timeMillis;
        if (dateObj is DateTime dateTime1)
        {
            timeMillis = (long)(dateTime1 - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
        else if (dateObj is DateTimeOffset dateTimeOffset1)
        {
            timeMillis = (long)(dateTimeOffset1 - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalMilliseconds;
        }
        else
        {
            throw new NotSupportedException($"Unsupported Date type: {dateObj.GetType()}");
        }
        return timeMillis.ToString();
    }
}