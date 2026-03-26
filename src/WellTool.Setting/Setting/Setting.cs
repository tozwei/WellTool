using System.Collections.Concurrent;
using System.Text;

namespace WellTool.Setting;

/// <summary>
/// 设置工具类。用于支持设置（配置）文件<br/>
/// 用于替换 Properties 类，提供功能更加强大的配置文件，同时对 Properties 文件向下兼容
/// 
/// <pre>
///  1、支持变量，默认变量命名为 ${变量名}，变量只能识别读入行的变量，例如第 6 行的变量在第三行无法读取
///  2、支持分组，分组为中括号括起来的内容，中括号以下的行都为此分组的内容，无分组相当于空字符分组，若某个 key 是 name，加上分组后的键相当于 group.name
///  3、注释以#开头，但是空行和不带"="的行也会被跳过，但是建议加#
///  4、store 方法不会保存注释内容，慎重使用
/// </pre>
/// </summary>
public class Setting : AbsSetting, IDisposable
{
    /// <summary>
    /// 默认字符集
    /// </summary>
    public static readonly Encoding DEFAULT_CHARSET = Encoding.UTF8;

    /// <summary>
    /// 默认配置文件扩展名
    /// </summary>
    public const string EXT_NAME = "setting";

    /// <summary>
    /// 附带分组的键值对存储
    /// </summary>
    private readonly GroupedMap _groupedMap = new();

    /// <summary>
    /// 本设置对象的字符集
    /// </summary>
    protected Encoding _charset;

    /// <summary>
    /// 是否使用变量
    /// </summary>
    protected bool _isUseVariable;

    /// <summary>
    /// 设定文件的资源
    /// </summary>
    protected string? _filePath;

    private bool _disposed;

    // ------------------------------------------------------------------------------------- Constructor start

    /// <summary>
    /// 空构造
    /// </summary>
    public Setting()
    {
        _charset = DEFAULT_CHARSET;
    }

    /// <summary>
    /// 设置是否使用变量替换
    /// </summary>
    /// <param name="isUseVariable">是否使用变量替换</param>
    /// <returns>当前Setting实例</returns>
    public Setting SetUseVariable(bool isUseVariable)
    {
        _isUseVariable = isUseVariable;
        return this;
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="path">相对路径或绝对路径</param>
    public Setting(string path) : this(path, false)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="path">相对路径或绝对路径</param>
    /// <param name="isUseVariable">是否使用变量</param>
    public Setting(string path, bool isUseVariable)
    {
        _charset = DEFAULT_CHARSET;
        _isUseVariable = isUseVariable;
        _filePath = path;
        Load(path);
    }

    // ------------------------------------------------------------------------------------- Constructor end

    // ------------------------------------------------------------------------------------- Get method start

    /// <summary>
    /// 获得指定分组的键对应值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="group">分组</param>
    /// <returns>值</returns>
    public override string? GetByGroup(string key, string group)
    {
        var value = _groupedMap.Get(group, key);

        if (_isUseVariable && !string.IsNullOrEmpty(value))
        {
            value = ReplaceVar(value, group);
        }

        return value;
    }

    /// <summary>
    /// 替换字符串中的变量
    /// </summary>
    /// <param name="value">包含变量的字符串</param>
    /// <param name="currentGroup">当前分组</param>
    /// <returns>替换后的字符串</returns>
    private string? ReplaceVar(string value, string currentGroup)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var result = value;
        var startPos = result.IndexOf("${", StringComparison.Ordinal);

        while (startPos >= 0)
        {
            var endPos = result.IndexOf('}', startPos);
            if (endPos < 0)
            {
                break;
            }

            var varName = result.Substring(startPos + 2, endPos - startPos - 2);
            string? varValue = null;

            // 检查变量名是否包含分组前缀
            var dotIndex = varName.IndexOf('.');
            if (dotIndex > 0)
            {
                // 带分组前缀的变量，如 ${demo.driver}
                var group = varName.Substring(0, dotIndex);
                var key = varName.Substring(dotIndex + 1);
                varValue = _groupedMap.Get(group, key);
            }
            else
            {
                // 简单变量，先从当前分组获取，然后从默认分组获取
                varValue = _groupedMap.Get(currentGroup, varName) ?? _groupedMap.Get(AbsSetting.DEFAULT_GROUP, varName);
            }

            if (varValue != null)
            {
                result = result.Replace("${" + varName + "}", varValue);
            }
            else
            {
                startPos = endPos;
            }

            startPos = result.IndexOf("${", startPos, StringComparison.Ordinal);
        }

        return result;
    }

    /// <summary>
    /// 加载配置文件
    /// </summary>
    /// <param name="path">文件路径</param>
    private void Load(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"配置文件不存在：{path}");
        }

        // 尝试使用 UTF8 编码读取文件
        string[] lines;
        try
        {
            lines = File.ReadAllLines(path, Encoding.UTF8);
        }
        catch
        {
            // 如果 UTF8 失败，使用默认编码
            lines = File.ReadAllLines(path);
        }
        
        Parse(lines);
    }

    /// <summary>
    /// 解析配置文件行
    /// </summary>
    /// <param name="lines">配置文件的行</param>
    private void Parse(string[] lines)
    {
        var currentGroup = AbsSetting.DEFAULT_GROUP;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 跳过空行和注释
            if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("#"))
            {
                continue;
            }

            // 检查是否是分组
            if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
            {
                currentGroup = trimmedLine.Substring(1, trimmedLine.Length - 2).Trim();
                continue;
            }

            // 解析键值对
            var separatorIndex = trimmedLine.IndexOf('=');
            if (separatorIndex > 0)
            {
                var key = trimmedLine.Substring(0, separatorIndex).Trim();
                var value = trimmedLine.Substring(separatorIndex + 1).Trim();

                _groupedMap.Put(currentGroup, key, value);
            }
        }
    }

    // ------------------------------------------------------------------------------------- Get method end

    // ------------------------------------------------------------------------------------- Put method start

    /// <summary>
    /// 加入键值对到默认分组
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>此 key 之前存在的值，如果没有返回 null</returns>
    public string? Put(string key, string? value)
    {
        return _groupedMap.Put(AbsSetting.DEFAULT_GROUP, key, value ?? string.Empty);
    }

    /// <summary>
    /// 加入键值对到指定分组
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="group">分组</param>
    /// <returns>此 key 之前存在的值，如果没有返回 null</returns>
    public string? Put(string key, string? value, string group)
    {
        return _groupedMap.Put(group, key, value ?? string.Empty);
    }

    // ------------------------------------------------------------------------------------- Put method end

    // ------------------------------------------------------------------------------------- Contains method start

    /// <summary>
    /// 指定分组中是否包含指定 key
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="group">分组</param>
    /// <returns>是否包含 key</returns>
    public bool ContainsKey(string key, string group)
    {
        return _groupedMap.ContainsKey(group, key);
    }

    /// <summary>
    /// 默认分组中是否包含指定 key
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>是否包含 key</returns>
    public bool ContainsKey(string key)
    {
        return ContainsKey(key, AbsSetting.DEFAULT_GROUP);
    }

    // ------------------------------------------------------------------------------------- Contains method end

    // ------------------------------------------------------------------------------------- Remove method start

    /// <summary>
    /// 删除指定分组的键
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="group">分组</param>
    /// <returns>被删除的值，如果值不存在，返回 null</returns>
    public string? Remove(string key, string group)
    {
        return _groupedMap.Remove(group, key);
    }

    /// <summary>
    /// 删除默认分组的键
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>被删除的值，如果值不存在，返回 null</returns>
    public string? Remove(string key)
    {
        return Remove(key, AbsSetting.DEFAULT_GROUP);
    }

    // ------------------------------------------------------------------------------------- Remove method end

    // ------------------------------------------------------------------------------------- Other method start

    /// <summary>
    /// 获取所有分组
    /// </summary>
    /// <returns>所有分组</returns>
    public ICollection<string> Groups()
    {
        return _groupedMap.Groups();
    }

    /// <summary>
    /// 清空所有配置
    /// </summary>
    public void Clear()
    {
        _groupedMap.Clear();
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _groupedMap.Clear();
            }
            _disposed = true;
        }
        base.Dispose(disposing);
    }

    // ------------------------------------------------------------------------------------- Other method end
}
