namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 3参数Consumer
    /// </summary>
    /// <typeparam name="P1">参数一类型</typeparam>
    /// <typeparam name="P2">参数二类型</typeparam>
    /// <typeparam name="P3">参数三类型</typeparam>
    public delegate void Consumer3<in P1, in P2, in P3>(P1 p1, P2 p2, P3 p3);
}