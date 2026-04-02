using System;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 获取接口
    /// </summary>
    /// <typeparam name="T">键类型</typeparam>
    public interface IJSONGetter<in T>
    {
        /// <summary>
        /// 获取对象，键不存在时返回默认值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        object GetObj(T key);

        /// <summary>
        /// 获取对象，键不存在时返回默认值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        object GetObj(T key, object defaultValue);

        /// <summary>
        /// 获取字符串值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>字符串值，键不存在或类型不匹配返回 null</returns>
        string GetStr(T key);

        /// <summary>
        /// 获取整数值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>整数值</returns>
        int GetInt(T key);

        /// <summary>
        /// 获取整数值，带默认值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>整数值</returns>
        int GetInt(T key, int defaultValue);

        /// <summary>
        /// 获取长整型值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>长整型值</returns>
        long GetLong(T key);

        /// <summary>
        /// 获取长整型值，带默认值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>长整型值</returns>
        long GetLong(T key, long defaultValue);

        /// <summary>
        /// 获取双精度值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>双精度值</returns>
        double GetDouble(T key);

        /// <summary>
        /// 获取双精度值，带默认值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>双精度值</returns>
        double GetDouble(T key, double defaultValue);

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>布尔值</returns>
        bool GetBool(T key);

        /// <summary>
        /// 获取布尔值，带默认值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>布尔值</returns>
        bool GetBool(T key, bool defaultValue);

        /// <summary>
        /// 获取 JSON 对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>JSONObject</returns>
        JSONObject GetJSONObject(T key);

        /// <summary>
        /// 获取 JSON 数组
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>JSONArray</returns>
        JSONArray GetJSONArray(T key);
    }
}
