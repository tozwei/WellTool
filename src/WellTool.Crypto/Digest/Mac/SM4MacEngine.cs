using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace WellTool.Crypto.Digest.Mac;

/// <summary>
/// SM4算法的MAC引擎实现
/// </summary>
public class SM4MacEngine : CBCBlockCipherMacEngine
{
    private const int MacSize = 128;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="parameters"><see cref="ICipherParameters"/></param>
    public SM4MacEngine(ICipherParameters parameters)
        : base(new SM4Engine(), MacSize, parameters)
    {
    }
}
