using System;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 基础接口
    /// </summary>
    public interface JSON
    {
        /// <summary>
        /// 获取JSON配置
        /// </summary>
        /// <returns></returns>
        JSONConfig GetConfig();
        
        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <param name="indentFactor">缩进因子</param>
        /// <returns>JSON字符串</returns>
        string ToJSONString(int indentFactor);
    }
}
