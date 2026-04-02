using System;
using System.IO;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 基础接口
    /// </summary>
    public interface IJSON
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>JSON 配置</returns>
        JSONConfig GetConfig();

        /// <summary>
        /// 通过表达式获取 JSON 中嵌套的对象
        /// </summary>
        /// <param name="expression">表达式，如 "name" 或 "user[0].name"</param>
        /// <returns>对象</returns>
        object GetByPath(string expression);

        /// <summary>
        /// 设置表达式指定位置的值
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="value">值</param>
        void PutByPath(string expression, object value);

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <returns>JSON 字符串</returns>
        string ToJSONString();

        /// <summary>
        /// 转为格式化 JSON 字符串
        /// </summary>
        /// <param name="indentFactor">缩进因子</param>
        /// <returns>格式化 JSON 字符串</returns>
        string ToJSONString(int indentFactor);
    }

    /// <summary>
    /// JSON 基础类，实现 IJSON 接口
    /// </summary>
    public abstract class JSONBase : IJSON
    {
        /// <summary>
        /// 配置项
        /// </summary>
        protected JSONConfig Config;

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>JSON 配置</returns>
        public virtual JSONConfig GetConfig()
        {
            return Config ?? (Config = JSONConfig.Create());
        }

        /// <summary>
        /// 通过表达式获取 JSON 中嵌套的对象
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>对象</returns>
        public abstract object GetByPath(string expression);

        /// <summary>
        /// 设置表达式指定位置的值
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="value">值</param>
        public abstract void PutByPath(string expression, object value);

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <returns>JSON 字符串</returns>
        public abstract override string ToString();

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <param name="indentFactor">缩进因子</param>
        /// <returns>JSON 字符串</returns>
        public abstract string ToJSONString(int indentFactor);

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <returns>JSON 字符串</returns>
        public string ToJSONString()
        {
            return ToJSONString(0);
        }

        /// <summary>
        /// 写入到 Writer
        /// </summary>
        /// <param name="writer">Writer</param>
        /// <param name="indentFactor">缩进因子</param>
        /// <param name="indent">缩进</param>
        public abstract void Write(TextWriter writer, int indentFactor, int indent);

        /// <summary>
        /// 转换为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns>目标类型实例</returns>
        public abstract T ToBean<T>();

        /// <summary>
        /// 转换为指定类型
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <returns>目标类型实例</returns>
        public abstract object ToBean(Type type);
    }
}
