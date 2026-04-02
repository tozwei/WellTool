namespace WellTool.Core.Lang.Caller
{
    /// <summary>
    /// 调用者接口<br>
    /// 可以通过此接口的实现类方法获取调用者、多级调用者以及判断是否被调用
    /// </summary>
    public interface Caller
    {
        /// <summary>
        /// 获得调用者
        /// </summary>
        /// <returns>调用者</returns>
        System.Type GetCaller();

        /// <summary>
        /// 获得调用者的调用者
        /// </summary>
        /// <returns>调用者的调用者</returns>
        System.Type GetCallerCaller();

        /// <summary>
        /// 获得调用者，指定第几级调用者 调用者层级关系：
        /// 
        /// <pre>
        /// 0 CallerUtil
        /// 1 调用CallerUtil中方法的类
        /// 2 调用者的调用者
        /// ...
        /// </pre>
        /// </summary>
        /// <param name="depth">层级。0表示CallerUtil本身，1表示调用CallerUtil的类，2表示调用者的调用者，依次类推</param>
        /// <returns>第几级调用者</returns>
        System.Type GetCaller(int depth);

        /// <summary>
        /// 是否被指定类调用
        /// </summary>
        /// <param name="clazz">调用者类</param>
        /// <returns>是否被调用</returns>
        bool IsCalledBy(System.Type clazz);
    }
}