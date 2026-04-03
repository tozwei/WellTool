namespace WellTool.Json
{
    /// <summary>
    /// JSONString接口定义了一个toJSONString方法
    /// 实现此接口的类可以通过实现toJSONString方法来改变转JSON字符串的方式。
    /// </summary>
    public interface JSONString
    {
        /// <summary>
        /// 自定义转JSON字符串的方法
        /// </summary>
        /// <returns>JSON字符串</returns>
        string ToJSONString();
    }
}
