namespace WellTool.Json
{
    /// <summary>
    /// 实现此接口的类可以通过实现Parse方法将JSON中的值解析为此对象的值
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    public interface JSONBeanParser<T>
    {
        /// <summary>
        /// value转Bean<br>
        /// 通过实现此接口，将JSON中的值填充到当前对象的字段值中，即对象自行实现JSON反序列化逻辑
        /// </summary>
        /// <param name="value">被解析的对象类型，可能为JSON或者普通String、Number等</param>
        void Parse(T value);
    }
}
