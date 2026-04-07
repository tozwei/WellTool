using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 无参无返回值委托
    /// </summary>
    public delegate void VoidFunc();

    /// <summary>
    /// 单参数无返回值委托
    /// </summary>
    public delegate void VoidFunc<T>(T arg);

    /// <summary>
    /// 双参数无返回值委托
    /// </summary>
    public delegate void VoidFunc<T1, T2>(T1 arg1, T2 arg2);

    /// <summary>
    /// 2参数无返回值委托
    /// </summary>
    public delegate void VoidFunc2<T1, T2>(T1 arg1, T2 arg2);

    /// <summary>
    /// 4参数无返回值委托
    /// </summary>
    public delegate void VoidFunc4<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    /// <summary>
    /// 5参数无返回值委托
    /// </summary>
    public delegate void VoidFunc5<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
}