using System.Text;

namespace WellTool.Setting;

/// <summary>
/// Setting 文件加载器
/// </summary>
public class SettingLoader : IDisposable
{
    /// <summary>
    /// 注释符号（当有此符号在行首，表示此行为注释）
    /// </summary>
    private const char COMMENT_FLAG_PRE = '#';

    /// <summary>
    /// 赋值分隔符（用于分隔键值对）
    /// </summary>
    private readonly char _assignFlag = '=';

    /// <summary>
    /// 本设置对象的字符集
    /// </summary>
    private readonly Encoding _charset;

    /// <summary>
    /// 是否使用变量
    /// </summary>
    private readonly bool _isUseVariable;

    /// <summary>
    /// GroupedMap
    /// </summary>
    private readonly GroupedMap _groupedMap;

    private bool _disposed;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="groupedMap">GroupedMap</param>
    public SettingLoader(GroupedMap groupedMap)
        : this(groupedMap, Encoding.UTF8, false)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="groupedMap">GroupedMap</param>
    /// <param name="charset">编码</param>
    /// <param name="isUseVariable">是否使用变量</param>
    public SettingLoader(GroupedMap groupedMap, Encoding charset, bool isUseVariable)
    {
        _groupedMap = groupedMap;
        _charset = charset;
        _isUseVariable = isUseVariable;
    }

    /// <summary>
    /// 加载设置文件
    /// </summary>
    /// <param name="filePath">配置文件路径</param>
    /// <returns>加载成功与否</returns>
    public bool Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Setting 文件不存在：{filePath}");
        }

        using var fileStream = File.OpenRead(filePath);
        return Load(fileStream);
    }

    /// <summary>
    /// 加载设置文件。此方法不会关闭流对象
    /// </summary>
    /// <param name="settingStream">文件流</param>
    /// <returns>加载成功与否</returns>
    public bool Load(Stream settingStream)
    {
        _groupedMap.Clear();

        using var reader = new StreamReader(settingStream, _charset, true, 1024, leaveOpen: true);
        string? line;
        var currentGroup = AbsSetting.DEFAULT_GROUP;

        while ((line = reader.ReadLine()) != null)
        {
            var trimmedLine = line.Trim();

            // 跳过空行和注释
            if (string.IsNullOrEmpty(trimmedLine) ||
                trimmedLine.StartsWith(COMMENT_FLAG_PRE.ToString()))
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
            var separatorIndex = trimmedLine.IndexOf(_assignFlag);
            if (separatorIndex > 0)
            {
                var key = trimmedLine.Substring(0, separatorIndex).Trim();
                var value = trimmedLine.Substring(separatorIndex + 1).Trim();

                if (_isUseVariable)
                {
                    value = ReplaceVar(value, currentGroup);
                }

                _groupedMap.Put(currentGroup, key, value);
            }
        }

        return true;
    }

    /// <summary>
    /// 替换字符串中的变量
    /// </summary>
    /// <param name="value">包含变量的字符串</param>
    /// <param name="currentGroup">当前分组</param>
    /// <returns>替换后的字符串</returns>
    private string ReplaceVar(string value, string currentGroup)
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

            // 先查找当前分组的变量，再查找默认分组
            var varValue = _groupedMap.Get(currentGroup, varName) ??
                          _groupedMap.Get(AbsSetting.DEFAULT_GROUP, varName);

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
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // 释放托管资源
            }
            _disposed = true;
        }
    }
}
