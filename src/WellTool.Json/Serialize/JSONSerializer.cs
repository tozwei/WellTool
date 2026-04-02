using System;

namespace WellTool.Json.Serialize
{
    /// <summary>
    /// JSON 序列化器接口
    /// </summary>
    public interface JSONSerializer
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="writer">写入器</param>
        void Serialize(object obj, object writer);
    }

    /// <summary>
    /// JSON 数组序列化器
    /// </summary>
    public interface JSONArraySerializer : JSONSerializer
    {
    }

    /// <summary>
    /// JSON 对象序列化器
    /// </summary>
    public interface JSONObjectSerializer : JSONSerializer
    {
    }

    /// <summary>
    /// JSON 反序列化器
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    public interface JSONDeserializer<T>
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json">JSON 对象</param>
        /// <returns>目标类型实例</returns>
        T Deserialize(object json);
    }
}
