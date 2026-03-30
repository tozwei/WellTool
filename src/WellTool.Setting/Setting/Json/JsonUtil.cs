using System.Text.Json;

namespace WellTool.Setting.Json;

/// <summary>
/// 基于 System.Text.Json 的 JSON 读写工具
/// </summary>
public static class JsonUtil
{
    /// <summary>
    /// JSON 序列化选项
    /// </summary>
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    /// <summary>
    /// 从文件加载 JSON 文件
    /// </summary>
    /// <param name="path">JSON 文件路径</param>
    /// <returns>加载的内容（字典）</returns>
    public static IDictionary<string, object?> LoadByPath(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"JSON 文件不存在：{path}");
        }

        using var reader = File.OpenText(path);
        return Load(reader);
    }

    /// <summary>
    /// 从文件加载 JSON 文件并转换为指定类型
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="path">JSON 文件路径</param>
    /// <returns>加载的对象</returns>
    public static T LoadByPath<T>(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"JSON 文件不存在：{path}");
        }

        using var reader = File.OpenText(path);
        return Load<T>(reader);
    }

    /// <summary>
    /// 从流中加载 JSON
    /// </summary>
    /// <param name="stream">输入流</param>
    /// <returns>加载的字典</returns>
    public static IDictionary<string, object?> Load(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return Load(reader);
    }

    /// <summary>
    /// 从流中加载 JSON 并转换为指定类型
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="stream">输入流</param>
    /// <returns>加载的对象</returns>
    public static T Load<T>(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return Load<T>(reader);
    }

    /// <summary>
    /// 从 Reader 加载 JSON
    /// </summary>
    /// <param name="reader">文本读取器</param>
    /// <returns>加载的字典</returns>
    public static IDictionary<string, object?> Load(TextReader reader)
    {
        var content = reader.ReadToEnd();
        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content, DefaultOptions);
        return ConvertJsonElementToDictionary(jsonElement);
    }

    /// <summary>
    /// 将 JsonElement 转换为字典
    /// </summary>
    /// <param name="element">JsonElement</param>
    /// <returns>转换后的字典</returns>
    private static IDictionary<string, object?> ConvertJsonElementToDictionary(JsonElement element)
    {
        var dict = new Dictionary<string, object?>();
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in element.EnumerateObject())
            {
                dict[property.Name] = ConvertJsonElementToObject(property.Value);
            }
        }
        return dict;
    }

    /// <summary>
    /// 将 JsonElement 转换为对象
    /// </summary>
    /// <param name="element">JsonElement</param>
    /// <returns>转换后的对象</returns>
    private static object? ConvertJsonElementToObject(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.Object => ConvertJsonElementToDictionary(element),
            JsonValueKind.Array => element.EnumerateArray().Select(ConvertJsonElementToObject).ToList(),
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number => element.TryGetInt64(out var longValue) ? (object)longValue :
                                    element.TryGetDouble(out var doubleValue) ? (object)doubleValue :
                                    element.GetString(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null,
            _ => element.GetString()
        };
    }

    /// <summary>
    /// 从 Reader 加载 JSON 并转换为指定类型
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="reader">文本读取器</param>
    /// <returns>加载的对象</returns>
    public static T Load<T>(TextReader reader)
    {
        var content = reader.ReadToEnd();
        return JsonSerializer.Deserialize<T>(content, DefaultOptions)!;
    }

    /// <summary>
    /// 将对象转储为 JSON 格式到文件
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="path">输出文件路径</param>
    public static void Dump(object obj, string path)
    {
        using var writer = File.CreateText(path);
        Dump(obj, writer);
    }

    /// <summary>
    /// 将对象转储为 JSON 格式到 Writer
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="writer">文本写入器</param>
    public static void Dump(object obj, TextWriter writer)
    {
        var json = JsonSerializer.Serialize(obj, DefaultOptions);
        writer.Write(json);
    }
}