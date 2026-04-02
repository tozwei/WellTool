using System;
using System.Diagnostics;
using WellTool.Core.Exceptions;

namespace WellTool.Core.Lang.Caller
{
    /// <summary>
    /// 通过StackTrace方式获取调用者
    /// </summary>
    public class StackTraceCaller : Caller
    {
        private const int Offset = 3; // 偏移量，跳过当前类和调用类

        /// <summary>
        /// 获得调用者
        /// </summary>
        /// <returns>调用者</returns>
        public Type GetCaller()
        {
            var stackTrace = new StackTrace();
            if (Offset + 1 >= stackTrace.FrameCount)
            {
                return null;
            }
            var stackFrame = stackTrace.GetFrame(Offset + 1);
            return stackFrame.GetMethod().DeclaringType;
        }

        /// <summary>
        /// 获得调用者的调用者
        /// </summary>
        /// <returns>调用者的调用者</returns>
        public Type GetCallerCaller()
        {
            var stackTrace = new StackTrace();
            if (Offset + 2 >= stackTrace.FrameCount)
            {
                return null;
            }
            var stackFrame = stackTrace.GetFrame(Offset + 2);
            return stackFrame.GetMethod().DeclaringType;
        }

        /// <summary>
        /// 获得调用者，指定第几级调用者
        /// </summary>
        /// <param name="depth">层级</param>
        /// <returns>第几级调用者</returns>
        public Type GetCaller(int depth)
        {
            var stackTrace = new StackTrace();
            if (Offset + depth >= stackTrace.FrameCount)
            {
                return null;
            }
            var stackFrame = stackTrace.GetFrame(Offset + depth);
            return stackFrame.GetMethod().DeclaringType;
        }

        /// <summary>
        /// 是否被指定类调用
        /// </summary>
        /// <param name="clazz">调用者类</param>
        /// <returns>是否被调用</returns>
        public bool IsCalledBy(Type clazz)
        {
            var stackTrace = new StackTrace();
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                var stackFrame = stackTrace.GetFrame(i);
                if (stackFrame.GetMethod().DeclaringType == clazz)
                {
                    return true;
                }
            }
            return false;
        }
    }
}