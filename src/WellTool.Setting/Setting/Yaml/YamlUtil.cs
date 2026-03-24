using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WellTool.Setting.Yaml;

/// <summary>
/// 基于 YamlDotNet 的 YAML 读写工具
/// </summary>
public static class YamlUtil
{
    /// <summary>
    /// 从文件加载 YAML 文件
    /// </summary>
    /// <param name="path">YAML 文件路径</param>
    /// <returns>加载的内容（字典）</returns>
    public static IDictionary<string, object?> LoadByPath(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"YAML 文件不存在：{path}");
        }

        using var reader = File.OpenText(path);
        return Load(reader);
    }

    /// <summary>
    /// 从文件加载 YAML 文件并转换为指定类型
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="path">YAML 文件路径</param>
    /// <returns>加载的对象</returns>
    public static T LoadByPath<T>(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"YAML 文件不存在：{path}");
        }

        using var reader = File.OpenText(path);
        return Load<T>(reader);
    }

    /// <summary>
    /// 从流中加载 YAML
    /// </summary>
    /// <param name="stream">输入流</param>
    /// <returns>加载的字典</returns>
    public static IDictionary<string, object?> Load(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return Load(reader);
    }

    /// <summary>
    /// 从流中加载 YAML 并转换为指定类型
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
    /// 从 Reader 加载 YAML
    /// </summary>
    /// <param name="reader">文本读取器</param>
    /// <returns>加载的字典</returns>
    public static IDictionary<string, object?> Load(TextReader reader)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var content = reader.ReadToEnd();
        return deserializer.Deserialize<IDictionary<string, object?>>(content);
    }

    /// <summary>
    /// 从 Reader 加载 YAML 并转换为指定类型
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="reader">文本读取器</param>
    /// <returns>加载的对象</returns>
    public static T Load<T>(TextReader reader)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var content = reader.ReadToEnd();
        return deserializer.Deserialize<T>(content);
    }

    /// <summary>
    /// 将对象转储为 YAML 格式到文件
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="path">输出文件路径</param>
    public static void Dump(object obj, string path)
    {
        using var writer = File.CreateText(path);
        Dump(obj, writer);
    }

    /// <summary>
    /// 将对象转储为 YAML 格式到 Writer
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="writer">文本写入器</param>
    public static void Dump(object obj, TextWriter writer)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
            .Build();

        var yaml = serializer.Serialize(obj);
        writer.Write(yaml);
    }
}
