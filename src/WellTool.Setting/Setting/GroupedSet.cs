using System.Text;

namespace WellTool.Setting;

/// <summary>
/// 分组化的Set集合类
/// 
/// 在配置文件中可以用中括号分隔不同的分组，每个分组会放在独立的Set中
/// 无分组的集合和[]分组集合会合并成员，重名的分组也会合并成员
/// 
/// 配置示例:
/// [group1]
/// aaa
/// bbb
/// ccc
/// 
/// [group2]
/// aaa
/// ccc
/// ddd
/// </summary>
public class GroupedSet : Dictionary<string, HashSet<string>>
{
    /// <summary>
    /// 注释符号（当有此符号在行首，表示此行为注释）
    /// </summary>
    private const string CommentFlagPre = "#";
    /// <summary>
    /// 分组行识别的环绕标记
    /// </summary>
    private static readonly char[] GroupSurround = { '[', ']' };

    /// <summary>
    /// 本设置对象的字符集
    /// </summary>
    private Encoding _charset = Encoding.UTF8;
    /// <summary>
    /// 设定文件路径
    /// </summary>
    private string? _groupedSetPath;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="charset">字符集</param>
    public GroupedSet(Encoding charset)
    {
        _charset = charset;
    }

    /// <summary>
    /// 构造函数，使用相对于Class文件根目录的相对路径
    /// </summary>
    /// <param name="pathBaseClassLoader">相对路径</param>
    /// <param name="charset">字符集</param>
    public GroupedSet(string pathBaseClassLoader, Encoding charset)
    {
        if (string.IsNullOrEmpty(pathBaseClassLoader))
        {
            pathBaseClassLoader = string.Empty;
        }

        var url = GetResourceUrl(pathBaseClassLoader);
        if (url == null)
        {
            throw new SettingRuntimeException($"Can not find GroupSet file: [{pathBaseClassLoader}]");
        }
        Init(url, charset);
    }

    /// <summary>
    /// 构造函数，相对于classes读取文件
    /// </summary>
    /// <param name="pathBaseClassLoader">相对路径</param>
    public GroupedSet(string pathBaseClassLoader)
    {
        Init(pathBaseClassLoader, Encoding.UTF8);
    }

    /// <summary>
    /// 初始化设定文件
    /// </summary>
    /// <param name="path">设定文件的路径</param>
    /// <param name="charset">字符集</param>
    /// <returns>成功初始化与否</returns>
    public bool Init(string path, Encoding charset)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new SettingRuntimeException("Null GroupSet path or charset define!");
        }
        _charset = charset;
        _groupedSetPath = path;

        return Load(path);
    }

    /// <summary>
    /// 加载设置文件
    /// </summary>
    /// <param name="path">配置文件路径</param>
    /// <returns>加载是否成功</returns>
    public bool Load(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new SettingRuntimeException("Null GroupSet path define!");
        }

        try
        {
            var lines = File.ReadAllLines(path, _charset);
            LoadLines(lines);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 重新加载配置文件
    /// </summary>
    public void Reload()
    {
        if (!string.IsNullOrEmpty(_groupedSetPath))
        {
            Load(_groupedSetPath);
        }
    }

    /// <summary>
    /// 从文本行加载
    /// </summary>
    /// <param name="lines">文本行</param>
    private void LoadLines(string[] lines)
    {
        Clear();

        HashSet<string>? valueSet = null;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 跳过注释行和空行
            if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith(CommentFlagPre))
            {
                continue;
            }

            // 转义处理：\# 表示值中的#
            if (trimmedLine.StartsWith("\\" + CommentFlagPre))
            {
                trimmedLine = trimmedLine.Substring(1);
            }

            // 记录分组名
            if (trimmedLine.Length > 0 && 
                trimmedLine[0] == GroupSurround[0] && 
                trimmedLine[^1] == GroupSurround[1])
            {
                var group = trimmedLine.Substring(1, trimmedLine.Length - 2).Trim();
                if (!TryGetValue(group, out valueSet))
                {
                    valueSet = new HashSet<string>();
                    this[group] = valueSet;
                }
                continue;
            }

            // 添加值
            if (valueSet == null)
            {
                valueSet = new HashSet<string>();
                this[string.Empty] = valueSet;
            }
            valueSet.Add(trimmedLine);
        }
    }

    /// <summary>
    /// 获得设定文件的路径
    /// </summary>
    /// <returns></returns>
    public string? GetPath()
    {
        return _groupedSetPath;
    }

    /// <summary>
    /// 获得所有分组名
    /// </summary>
    /// <returns></returns>
    public ICollection<string> GetGroups()
    {
        return Keys;
    }

    /// <summary>
    /// 获得对应分组的所有值
    /// </summary>
    /// <param name="group">分组名</param>
    /// <returns>分组的值集合</returns>
    public HashSet<string>? GetValues(string group)
    {
        if (group == null)
        {
            group = string.Empty;
        }
        TryGetValue(group, out var valueSet);
        return valueSet;
    }

    /// <summary>
    /// 是否在给定分组的集合中包含指定值
    /// </summary>
    /// <param name="group">分组名</param>
    /// <param name="value">测试的值</param>
    /// <param name="otherValues">其他值</param>
    /// <returns>是否包含</returns>
    public bool Contains(string group, string value, params string[] otherValues)
    {
        if (otherValues != null && otherValues.Length > 0)
        {
            var valueList = new List<string>(otherValues) { value };
            return Contains(group, valueList);
        }

        var valueSet = GetValues(group);
        if (valueSet == null || valueSet.Count == 0)
        {
            return false;
        }
        return valueSet.Contains(value);
    }

    /// <summary>
    /// 是否在给定分组的集合中全部包含指定值集合
    /// </summary>
    /// <param name="group">分组名</param>
    /// <param name="values">测试的值集合</param>
    /// <returns>是否包含</returns>
    public bool Contains(string group, IEnumerable<string> values)
    {
        var valueSet = GetValues(group);
        if (valueSet == null || valueSet.Count == 0)
        {
            return false;
        }

        return values.All(valueSet.Contains);
    }

    private static string? GetResourceUrl(string path, Type? clazz = null)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        // 尝试作为文件路径
        if (File.Exists(path))
            return path;

        // 尝试从程序集加载
        var assembly = clazz?.Assembly ?? typeof(GroupedSet).Assembly;
        var name = assembly.GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith(path) || n.EndsWith(path.Replace("/", ".")));

        return name;
    }
}
