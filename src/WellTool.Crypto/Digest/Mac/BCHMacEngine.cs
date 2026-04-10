using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

namespace WellTool.Crypto.Digest.Mac;

/// <summary>
/// BouncyCastle的HMAC算法实现引擎，使用<see cref="IMac"/> 实现摘要
/// 当引入BouncyCastle库时自动使用其作为Provider
/// </summary>
public class BCHMacEngine : BCMacEngine
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="digest">摘要算法，为<see cref="IDigest"/> 的接口实现</param>
    /// <param name="key">密钥</param>
    /// <param name="iv">加盐</param>
    public BCHMacEngine(IDigest digest, byte[] key, byte[] iv)
        : this(digest, new ParametersWithIV(new KeyParameter(key), iv))
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="digest">摘要算法，为<see cref="IDigest"/> 的接口实现</param>
    /// <param name="key">密钥</param>
    public BCHMacEngine(IDigest digest, byte[] key)
        : this(digest, new KeyParameter(key))
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="digest">摘要算法</param>
    /// <param name="parameters">参数，例如密钥可以用<see cref="KeyParameter"/></param>
    public BCHMacEngine(IDigest digest, ICipherParameters parameters)
        : this(new Org.BouncyCastle.Crypto.Macs.HMac(digest), parameters)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mac"><see cref="Org.BouncyCastle.Crypto.Macs.HMac"/></param>
    /// <param name="parameters">参数，例如密钥可以用<see cref="KeyParameter"/></param>
    public BCHMacEngine(Org.BouncyCastle.Crypto.Macs.HMac mac, ICipherParameters parameters)
        : base(mac, parameters)
    {
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="digest">摘要算法</param>
    /// <param name="parameters">参数，例如密钥可以用<see cref="KeyParameter"/></param>
    /// <returns>新的BCHMacEngine实例</returns>
    public static BCHMacEngine Init(IDigest digest, ICipherParameters parameters)
    {
        return new BCHMacEngine(digest, parameters);
    }
}
