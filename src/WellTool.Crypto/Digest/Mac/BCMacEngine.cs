using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;

namespace WellTool.Crypto.Digest.Mac;

/// <summary>
/// BouncyCastle的MAC算法实现引擎，使用<see cref="IMac"/> 实现摘要
/// 当引入BouncyCastle库时自动使用其作为Provider
/// </summary>
public class BCMacEngine : MacEngine
{
    private IMac _mac;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mac"><see cref="IMac"/></param>
    /// <param name="parameters">参数，例如密钥可以用<see cref="KeyParameter"/></param>
    public BCMacEngine(IMac mac, ICipherParameters parameters)
    {
        _mac = mac;
        _mac.Init(parameters);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="key">密钥</param>
    public void Init(byte[] key)
    {
        _mac.Init(new KeyParameter(key));
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data">数据</param>
    public void Update(byte[] data)
    {
        _mac.BlockUpdate(data, 0, data.Length);
    }

    /// <summary>
    /// 完成计算
    /// </summary>
    /// <returns>结果</returns>
    public byte[] DoFinal()
    {
        var result = new byte[_mac.GetMacSize()];
        _mac.DoFinal(result, 0);
        return result;
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset()
    {
        _mac.Reset();
    }

    /// <summary>
    /// 获得 <see cref="IMac"/>
    /// </summary>
    /// <returns><see cref="IMac"/></returns>
    public IMac GetMac()
    {
        return _mac;
    }

    /// <summary>
    /// 获取算法名称
    /// </summary>
    /// <returns>算法名称</returns>
    public string GetAlgorithm()
    {
        return _mac.AlgorithmName;
    }
}
