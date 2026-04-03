using System;
using System.IO;

namespace WellTool.Json
{
    /// <summary>
    /// JSON支持，继承此类实现实体类与JSON的相互转换
    /// </summary>
    public class JSONSupport : JSONString
    {
        /// <summary>
        /// JSON String转Bean
        /// </summary>
        /// <param name="jsonString">JSON String</param>
        public virtual void Parse(string jsonString)
        {
            Parse(new JSONObject(jsonString));
        }

        /// <summary>
        /// JSON转Bean
        /// </summary>
        /// <param name="json">JSON</param>
        public virtual void Parse(JSON json)
        {
            var support = JSONConverter.JsonConvert(GetType(), json, new JSONConfig());
            if (support != null)
            {
                // 简单实现：将属性值从support复制到this
                var properties = GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.CanRead && property.CanWrite)
                    {
                        var value = property.GetValue(support);
                        property.SetValue(this, value);
                    }
                }
            }
        }

        /// <summary>
        /// 转为JSON对象
        /// </summary>
        /// <returns>JSONObject对象</returns>
        public virtual JSONObject ToJSON()
        {
            return new JSONObject(this);
        }

        /// <summary>
        /// 自定义转JSON字符串的方法
        /// </summary>
        /// <returns>JSON字符串</returns>
        public virtual string ToJSONString()
        {
            return ToJSON().ToString();
        }

        /// <summary>
        /// 美化的JSON（使用回车缩进显示JSON），用于打印输出debug
        /// </summary>
        /// <returns>美化的JSON</returns>
        public virtual string ToPrettyString()
        {
            return ToJSON().ToStringPretty();
        }

        /// <summary>
        /// 返回JSON字符串
        /// </summary>
        /// <returns>JSON字符串</returns>
        public override string ToString()
        {
            return ToJSONString();
        }
    }
}
