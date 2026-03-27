namespace WellTool.Core.Lang.Hash
{
    /// <summary>
    /// 128位Hash计算接口
    /// </summary>
    /// <typeparam name="T">被计算hash的对象类型</typeparam>
    public delegate Number128 Hash128<in T>(T t);
}