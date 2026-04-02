using System;
using System.Collections.Generic;

namespace WellTool.Json.Serialize
{
    /// <summary>
    /// 全局序列化映射表
    /// </summary>
    public static class GlobalSerializeMapping
    {
        private static readonly Dictionary<Type, JSONArraySerializer> ArraySerializers = new Dictionary<Type, JSONArraySerializer>();
        private static readonly Dictionary<Type, JSONObjectSerializer> ObjectSerializers = new Dictionary<Type, JSONObjectSerializer>();
        private static readonly Dictionary<Type, JSONDeserializer<object>> Deserializers = new Dictionary<Type, JSONDeserializer<object>>();

        /// <summary>
        /// 添加数组序列化器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="serializer">序列化器</param>
        public static void Put<T>(JSONArraySerializer serializer)
        {
            ArraySerializers[typeof(T)] = serializer;
        }

        /// <summary>
        /// 添加对象序列化器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="serializer">序列化器</param>
        public static void Put<T>(JSONObjectSerializer serializer)
        {
            ObjectSerializers[typeof(T)] = serializer;
        }

        /// <summary>
        /// 添加序列化器
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="serializer">序列化器</param>
        public static void Put(Type type, JSONArraySerializer serializer)
        {
            ArraySerializers[type] = serializer;
        }

        /// <summary>
        /// 添加序列化器
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="serializer">序列化器</param>
        public static void Put(Type type, JSONObjectSerializer serializer)
        {
            ObjectSerializers[type] = serializer;
        }

        /// <summary>
        /// 添加反序列化器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="deserializer">反序列化器</param>
        public static void Put<T>(JSONDeserializer<T> deserializer)
        {
            Deserializers[typeof(T)] = new WrappedDeserializer<T>(deserializer);
        }

        /// <summary>
        /// 包装反序列化器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        private class WrappedDeserializer<T> : JSONDeserializer<object>
        {
            private readonly JSONDeserializer<T> _deserializer;

            public WrappedDeserializer(JSONDeserializer<T> deserializer)
            {
                _deserializer = deserializer;
            }

            public object Deserialize(object json)
            {
                return _deserializer.Deserialize(json);
            }
        }

        /// <summary>
        /// 添加反序列化器
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="deserializer">反序列化器</param>
        public static void Put(Type type, JSONDeserializer<object> deserializer)
        {
            Deserializers[type] = deserializer;
        }

        /// <summary>
        /// 获取数组序列化器
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>序列化器</returns>
        public static JSONArraySerializer GetArraySerializer(Type type)
        {
            return ArraySerializers.TryGetValue(type, out var serializer) ? serializer : null;
        }

        /// <summary>
        /// 获取对象序列化器
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>序列化器</returns>
        public static JSONObjectSerializer GetObjectSerializer(Type type)
        {
            return ObjectSerializers.TryGetValue(type, out var serializer) ? serializer : null;
        }

        /// <summary>
        /// 获取反序列化器
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>反序列化器</returns>
        public static JSONDeserializer<object> GetDeserializer(Type type)
        {
            return Deserializers.TryGetValue(type, out var deserializer) ? deserializer : null;
        }

        /// <summary>
        /// 移除序列化器
        /// </summary>
        /// <param name="type">类型</param>
        public static void Remove(Type type)
        {
            ArraySerializers.Remove(type);
            ObjectSerializers.Remove(type);
            Deserializers.Remove(type);
        }

        /// <summary>
        /// 清空所有映射
        /// </summary>
        public static void Clear()
        {
            ArraySerializers.Clear();
            ObjectSerializers.Clear();
            Deserializers.Clear();
        }
    }
}
