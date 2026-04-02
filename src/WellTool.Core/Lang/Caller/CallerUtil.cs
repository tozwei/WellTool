using System;
using System.Diagnostics;

namespace WellTool.Core.Lang.Caller
{
    /// <summary>
    /// 调用者。可以通过此类的方法获取调用者、多级调用者以及判断是否被调用
    /// </summary>
    public class CallerUtil
    {
        private static readonly Caller Instance;

        static CallerUtil()
        {
            Instance = TryCreateCaller();
        }

        /// <summary>
        /// 获得调用者
        /// </summary>
        /// <returns>调用者</returns>
        public static Type GetCaller()
        {
            return Instance.GetCaller();
        }

        /// <summary>
        /// 获得调用者的调用者
        /// </summary>
        /// <returns>调用者的调用者</returns>
        public static Type GetCallerCaller()
        {
            return Instance.GetCallerCaller();
        }

        /// <summary>
        /// 获得调用者，指定第几级调用者<br>
        /// 调用者层级关系：
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
        public static Type GetCaller(int depth)
        {
            return Instance.GetCaller(depth);
        }

        /// <summary>
        /// 是否被指定类调用
        /// </summary>
        /// <param name="clazz">调用者类</param>
        /// <returns>是否被调用</returns>
        public static bool IsCalledBy(Type clazz)
        {
            return Instance.IsCalledBy(clazz);
        }

        /// <summary>
        /// 获取调用此方法的方法名
        /// </summary>
        /// <param name="isFullName">是否返回全名，全名包括方法所在类的全路径名</param>
        /// <returns>调用此方法的方法名</returns>
        public static string GetCallerMethodName(bool isFullName)
        {
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(2);
            var methodName = stackFrame.GetMethod().Name;
            if (!isFullName)
            {
                return methodName;
            }

            return stackFrame.GetMethod().DeclaringType.FullName + "." + methodName;
        }

        /// <summary>
        /// 尝试创建Caller实现
        /// </summary>
        /// <returns>Caller实现</returns>
        private static Caller TryCreateCaller()
        {
            return new StackTraceCaller();
        }
    }
}