using System.Collections.Concurrent;

namespace WellTool.Setting.Dialect;

/// <summary>
/// Properties 文件读取封装类
/// </summary>
public class Props : ConcurrentDictionary<string, string>, IDisposable
{
    /// <summary>
    /// 默认配置文件扩展名
    /// </summary>
    public const string EXT_NAME = "properties";

    private readonly string? _filePath;
    private bool _disposed;

    /// <summary>
    /// 空构造
    /// </summary>
    public Props()
    {
    }

    /// <summary>
    /// 从文件加载 Properties
    /// </summary>
    /// <param name="path">文件路径</param>
    public Props(string path)
    {
        _filePath = path;
        Load(path);
    }

    /// <summary>
    /// 获得 Classpath 下的 Properties 文件
    /// </summary>
    /// <param name="resource">资源（相对路径）</param>
    /// <returns>Props</returns>
    public static Props GetProp(string resource)
    {
        return new Props(resource);
    }

    /// <summary>
    /// 加载 Properties 文件
    /// </summary>
    /// <param name="path">文件路径</param>
    private void Load(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Properties 文件不存在：{path}");
        }

        var lines = File.ReadAllLines(path);
        Parse(lines);
    }

    /// <summary>
    /// 解析 Properties 文件行
    /// </summary>
    /// <param name="lines">文件行</param>
    private void Parse(string[] lines)
    {
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 跳过空行和注释
            if (string.IsNullOrEmpty(trimmedLine) ||
                trimmedLine.StartsWith("#") ||
                trimmedLine.StartsWith("!"))
            {
                continue;
            }

            // 解析键值对（支持=和:分隔符）
            var separatorIndex = FindSeparator(trimmedLine);
            if (separatorIndex > 0)
            {
                var key = trimmedLine.Substring(0, separatorIndex).Trim();
                var value = trimmedLine.Substring(separatorIndex + 1).Trim();

                this[key] = value;
            }
        }
    }

    /// <summary>
    /// 查找分隔符位置
    /// </summary>
    /// <param name="line">行</param>
    /// <returns>分隔符位置</returns>
    private static int FindSeparator(string line)
    {
        var equalsPos = line.IndexOf('=');
        var colonPos = line.IndexOf(':');

        if (equalsPos >= 0 && colonPos >= 0)
        {
            return Math.Min(equalsPos, colonPos);
        }

        return Math.Max(equalsPos, colonPos);
    }

    /// <summary>
    /// 获取字符串值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>值</returns>
    public string GetStr(string key, string? defaultValue = null)
    {
        return TryGetValue(key, out var value) ? value : defaultValue ?? string.Empty;
    }

    /// <summary>
    /// 获取整数值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>值</returns>
    public int GetInt(string key, int defaultValue = 0)
    {
        var value = GetStr(key);
        return int.TryParse(value, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// 获取长整型值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>值</returns>
    public long GetLong(string key, long defaultValue = 0L)
    {
        var value = GetStr(key);
        return long.TryParse(value, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// 获取布尔值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>值</returns>
    public bool GetBool(string key, bool defaultValue = false)
    {
        var value = GetStr(key);
        return bool.TryParse(value, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// 获取双精度浮点数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>值</returns>
    public double GetDouble(string key, double defaultValue = 0.0)
    {
        var value = GetStr(key);
        return double.TryParse(value, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// 是否包含指定键
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>是否包含</returns>
    public new bool ContainsKey(string key)
    {
        return base.ContainsKey(key);
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
                Clear();
            }
            _disposed = true;
        }
    }
}
