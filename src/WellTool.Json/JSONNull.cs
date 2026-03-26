namespace WellTool.Json
{
    /// <summary>
    /// JSON null 值
    /// </summary>
    public class JSONNull
    {
        /// <summary>
        /// JSON null 单例
        /// </summary>
        public static readonly JSONNull Instance = new JSONNull();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private JSONNull() { }

        /// <summary>
        /// 重写 ToString 方法
        /// </summary>
        /// <returns>"null"</returns>
        public override string ToString()
        {
            return "null";
        }

        /// <summary>
        /// 重写 Equals 方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object obj)
        {
            return obj is JSONNull;
        }

        /// <summary>
        /// 重写 GetHashCode 方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}