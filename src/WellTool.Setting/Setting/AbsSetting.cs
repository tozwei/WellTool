using Microsoft.Extensions.Logging;

namespace WellTool.Setting;

/// <summary>
/// Setting 抽象类
/// </summary>
public abstract class AbsSetting : ISettingGetter, IDisposable
{
    /// <summary>
    /// 数组类型值默认分隔符
    /// </summary>
    public const string DEFAULT_DELIMITER = ",";

    /// <summary>
    /// 默认分组
    /// </summary>
    public const string DEFAULT_GROUP = "";

    private bool _disposed;

    /// <summary>
    /// 获得字符串类型值
    /// </summary>
    /// <param name="key">KEY</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>值，如果字符串为 null 返回默认值</returns>
    public virtual string GetStr(string key, string? defaultValue = null)
    {
        return GetStr(key, DEFAULT_GROUP, defaultValue);
    }

    /// <summary>
    /// 获得字符串类型值
    /// </summary>
    /// <param name="key">KEY</param>
    /// <param name="group">分组</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>值，如果字符串为 null 返回默认值</returns>
    public virtual string GetStr(string key, string group, string? defaultValue)
    {
        var value = GetByGroup(key, group);
        return value ?? defaultValue ?? string.Empty;
    }

    /// <summary>
    /// 获得字符串类型值，如果字符串为 null 或者""返回默认值
    /// </summary>
    /// <param name="key">KEY</param>
    /// <param name="group">分组</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>值，如果字符串为 null 或者""返回默认值</returns>
    public virtual string GetStrNotEmpty(string key, string group, string defaultValue)
    {
        var value = GetByGroup(key, group);
        return string.IsNullOrEmpty(value) ? defaultValue : value;
    }

    /// <summary>
    /// 获得指定分组的键对应值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="group">分组</param>
    /// <returns>值</returns>
    public abstract string? GetByGroup(string key, string group);

    // --------------------------------------------------------------- Get

    /// <summary>
    /// 带有日志提示的 get，如果没有定义指定的 KEY，则打印 debug 日志
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>值</returns>
    public virtual string? GetWithLog(string key, ILogger? logger = null)
    {
        var value = GetStr(key);
        if (value == null)
        {
            logger?.LogDebug("No key define for [{Key}]!", key);
        }
        return value;
    }

    /// <summary>
    /// 带有日志提示的 get，如果没有定义指定的 KEY，则打印 debug 日志
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="group">分组</param>
    /// <param name="logger">日志记录器</param>
    /// <returns>值</returns>
    public virtual string? GetByGroupWithLog(string key, string group, ILogger? logger = null)
    {
        var value = GetByGroup(key, group);
        if (value == null)
        {
            logger?.LogDebug("No key define for [{Key}] of group [{Group}] !", key, group);
        }
        return value;
    }

    // --------------------------------------------------------------- Get string array

    /// <summary>
    /// 获得数组型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    public virtual string[] GetStrings(string key)
    {
        return GetStrings(key, null);
    }

    /// <summary>
    /// 获得数组型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认的值</param>
    /// <returns>属性值</returns>
    public virtual string[] GetStringsWithDefault(string key, string[] defaultValue)
    {
        var value = GetStrings(key, null);
        return value.Length == 0 ? defaultValue : value;
    }

    /// <summary>
    /// 获得数组型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组名</param>
    /// <returns>属性值</returns>
    public virtual string[] GetStrings(string key, string? group)
    {
        return GetStrings(key, group ?? DEFAULT_GROUP, DEFAULT_DELIMITER);
    }

    /// <summary>
    /// 获得数组型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组名</param>
    /// <param name="separator">分隔符</param>
    /// <returns>属性值</returns>
    public virtual string[] GetStrings(string key, string? group, string separator)
    {
        var str = GetStr(key, group, null);
        if (str == null)
        {
            return Array.Empty<string>();
        }
        return str.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }

    // --------------------------------------------------------------- Get int

    /// <summary>
    /// 获得 int 型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值</returns>
    public virtual int GetInt(string key, int defaultValue)
    {
        return GetInt(key, DEFAULT_GROUP, defaultValue);
    }

    /// <summary>
    /// 获得 int 型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值</returns>
    public virtual int GetInt(string key, string group, int defaultValue)
    {
        var value = GetStr(key, group, null);
        return int.TryParse(value, out var result) ? result : defaultValue;
    }

    // --------------------------------------------------------------- Get long

    /// <summary>
    /// 获得 long 型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值</returns>
    public virtual long GetLong(string key, long defaultValue)
    {
        return GetLong(key, DEFAULT_GROUP, defaultValue);
    }

    /// <summary>
    /// 获得 long 型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值</returns>
    public virtual long GetLong(string key, string group, long defaultValue)
    {
        var value = GetStr(key, group, null);
        return long.TryParse(value, out var result) ? result : defaultValue;
    }

    // --------------------------------------------------------------- Get double

    /// <summary>
    /// 获得 double 型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值</returns>
    public virtual double GetDouble(string key, double defaultValue)
    {
        return GetDouble(key, DEFAULT_GROUP, defaultValue);
    }

    /// <summary>
    /// 获得 double 型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值</returns>
    public virtual double GetDouble(string key, string group, double defaultValue)
    {
        var value = GetStr(key, group, null);
        return double.TryParse(value, out var result) ? result : defaultValue;
    }

    // --------------------------------------------------------------- Get bool

    /// <summary>
    /// 获得 bool 型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值</returns>
    public virtual bool GetBool(string key, bool defaultValue)
    {
        return GetBool(key, DEFAULT_GROUP, defaultValue);
    }

    /// <summary>
    /// 获得 bool 型
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值</returns>
    public virtual bool GetBool(string key, string group, bool defaultValue)
    {
        var value = GetStr(key, group, null);
        return bool.TryParse(value, out var result) ? result : defaultValue;
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

/// <summary>
/// Setting 获取接口
/// </summary>
public interface ISettingGetter
{
    /// <summary>
    /// 获得指定分组的键对应值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="group">分组</param>
    /// <returns>值</returns>
    string? GetByGroup(string key, string group);
}
