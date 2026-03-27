namespace WellTool.Core.Lang.Hash
{
    /// <summary>
    /// 32位Hash计算接口
    /// </summary>
    /// <typeparam name="T">被计算hash的对象类型</typeparam>
    public delegate int Hash32<in T>(T t);
}