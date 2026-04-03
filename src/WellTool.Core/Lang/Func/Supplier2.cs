using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 2参数提供者
    /// </summary>
    public delegate T2 Supplier2<T1, T2>(T1 arg1);

    /// <summary>
    /// 2参数提供者
    /// </summary>
    public delegate T3 Supplier3<T1, T2, T3>(T1 arg1, T2 arg2);

    /// <summary>
    /// 4参数提供者
    /// </summary>
    public delegate T4 Supplier4<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3);

    /// <summary>
    /// 5参数提供者
    /// </summary>
    public delegate T5 Supplier5<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    /// <summary>
    /// 消费者接口
    /// </summary>
    public interface IConsumer<in T>
    {
        /// <summary>
        /// 消费值
        /// </summary>
        void Accept(T value);
    }

    /// <summary>
    /// 2参数消费者
    /// </summary>
    public delegate void Consumer2<T1, T2>(T1 arg1, T2 arg2);

    /// <summary>
    /// 3参数消费者
    /// </summary>
    public delegate void Consumer3<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
}
