namespace WellTool.Core.Lang.Hash
{
    /// <summary>
    /// 64位Hash计算接口
    /// </summary>
    /// <typeparam name="T">被计算hash的对象类型</typeparam>
    public delegate long Hash64<in T>(T t);
}