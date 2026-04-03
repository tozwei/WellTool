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
}
