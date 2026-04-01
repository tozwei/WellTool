using System.Data;
using System;

namespace WellTool.DB.Transaction
{
    /// <summary>
    /// 事务隔离级别
    /// </summary>
    public enum TransactionLevel
    {
        /// <summary>
        /// 未指定
        /// </summary>
        [IsolationLevel(IsolationLevel.Unspecified)]
        Unspecified = 0,

        /// <summary>
        /// 读未提交
        /// </summary>
        [IsolationLevel(IsolationLevel.ReadUncommitted)]
        ReadUncommitted = 1,

        /// <summary>
        /// 读已提交
        /// </summary>
        [IsolationLevel(IsolationLevel.ReadCommitted)]
        ReadCommitted = 2,

        /// <summary>
        /// 可重复读
        /// </summary>
        [IsolationLevel(IsolationLevel.RepeatableRead)]
        RepeatableRead = 4,

        /// <summary>
        /// 可序列化
        /// </summary>
        [IsolationLevel(IsolationLevel.Serializable)]
        Serializable = 8
    }

    /// <summary>
    /// IsolationLevel 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class IsolationLevelAttribute : Attribute
    {
        /// <summary>
        /// IsolationLevel
        /// </summary>
        public IsolationLevel IsolationLevel { get; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="isolationLevel">IsolationLevel</param>
        public IsolationLevelAttribute(IsolationLevel isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }
    }
}