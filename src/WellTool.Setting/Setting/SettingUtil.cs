using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace WellTool.Setting;

/// <summary>
/// Setting 工具类
/// 提供静态方法获取配置文件
/// </summary>
public static class SettingUtil
{
    /// <summary>
    /// 配置文件缓存
    /// </summary>
    private static readonly ConcurrentDictionary<string, Setting> _settingMap = new();

    /// <summary>
    /// 获取当前环境下的配置文件<br/>
    /// name 可以为不包括扩展名的文件名（默认.setting 为结尾），也可以是文件名全称
    /// </summary>
    /// <param name="name">文件名，如果没有扩展名，默认为.setting</param>
    /// <returns>当前环境下配置文件</returns>
    public static Setting Get(string name)
    {
        return _settingMap.GetOrAdd(name, filePath =>
        {
            var extName = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(extName))
            {
                filePath = $"{filePath}.{Setting.EXT_NAME}";
            }
            return new Setting(filePath, true);
        });
    }

    /// <summary>
    /// 获取给定路径找到的第一个配置文件<br/>
    /// name 可以为不包括扩展名的文件名（默认.setting 为结尾），也可以是文件名全称
    /// </summary>
    /// <param name="names">文件名，如果没有扩展名，默认为.setting</param>
    /// <returns>当前环境下配置文件，如果未找到则返回 null</returns>
    public static Setting? GetFirstFound(params string[] names)
    {
        foreach (var name in names)
        {
            try
            {
                return Get(name);
            }
            catch (FileNotFoundException)
            {
                // 忽略异常，继续查找下一个
            }
        }
        return null;
    }
}
