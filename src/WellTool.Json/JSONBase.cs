using System;
using System.IO;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 基础类
    /// </summary>
    public abstract class JSONBase : JSON
    {
        /// <summary>
        /// JSON配置
        /// </summary>
        protected internal JSONConfig Config;

        /// <summary>
        /// 获取JSON配置
        /// </summary>
        /// <returns></returns>
        public JSONConfig GetConfig()
        {
            return Config;
        }

        /// <summary>
        /// 获取实体类对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>实体类对象</returns>
        public virtual T ToBean<T>()
        {
            return (T)ToBean(typeof(T));
        }

        /// <summary>
        /// 获取实体类对象
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>实体类对象</returns>
        public virtual object ToBean(Type type)
        {
            return JSONConverter.JsonConvert(type, this, Config);
        }

        /// <summary>
        /// 获取实体类对象
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="ignoreError">是否忽略转换错误</param>
        /// <returns>实体类对象</returns>
        public virtual object ToBean(Type type, bool ignoreError)
        {
            var config = Config.Copy().SetIgnoreError(ignoreError);
            return JSONConverter.JsonConvert(type, this, config);
        }

        /// <summary>
        /// 通过表达式获取JSON中嵌套的对象
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>对象</returns>
        public abstract object GetByPath(string expression);

        /// <summary>
        /// 通过表达式设置JSON中嵌套的对象
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="value">值</param>
        public abstract void PutByPath(string expression, object value);

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <returns>JSON字符串</returns>
        public string ToJSONString()
        {
            return ToJSONString(0);
        }

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <param name="indentFactor">缩进因子</param>
        /// <returns>JSON字符串</returns>
        public abstract string ToJSONString(int indentFactor);

        /// <summary>
        /// 将JSON内容写入Writer
        /// </summary>
        /// <param name="writer">Writer</param>
        /// <param name="indentFactor">缩进因子</param>
        /// <param name="indent">本级别缩进量</param>
        public abstract void Write(TextWriter writer, int indentFactor, int indent);

        /// <summary>
        /// 获取格式化后的JSON字符串
        /// </summary>
        /// <returns>格式化的JSON字符串</returns>
        public virtual string ToStringPretty()
        {
            return ToJSONString(4);
        }
    }
}
