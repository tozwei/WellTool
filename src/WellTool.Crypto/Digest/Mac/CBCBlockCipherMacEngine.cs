using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;

namespace WellTool.Crypto.Digest.Mac;

/// <summary>
/// <see cref="CbcBlockCipherMac"/>实现的MAC算法，使用CBC Block方式
/// </summary>
public class CBCBlockCipherMacEngine : BCMacEngine
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="cipher">摘要算法，为<see cref="IBlockCipher"/> 的接口实现</param>
    /// <param name="macSizeInBits">mac结果的bits长度，必须为8的倍数</param>
    /// <param name="key">密钥</param>
    /// <param name="iv">加盐</param>
    public CBCBlockCipherMacEngine(IBlockCipher cipher, int macSizeInBits, byte[] key, byte[] iv)
        : this(cipher, macSizeInBits, new ParametersWithIV(new KeyParameter(key), iv))
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="cipher">算法，为<see cref="IBlockCipher"/> 的接口实现</param>
    /// <param name="macSizeInBits">mac结果的bits长度，必须为8的倍数</param>
    /// <param name="key">密钥</param>
    public CBCBlockCipherMacEngine(IBlockCipher cipher, int macSizeInBits, byte[] key)
        : this(cipher, macSizeInBits, new KeyParameter(key))
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="cipher">算法，为<see cref="IBlockCipher"/> 的接口实现</param>
    /// <param name="macSizeInBits">mac结果的bits长度，必须为8的倍数</param>
    /// <param name="parameters">参数，例如密钥可以用<see cref="KeyParameter"/></param>
    public CBCBlockCipherMacEngine(IBlockCipher cipher, int macSizeInBits, ICipherParameters parameters)
        : this(new CbcBlockCipherMac(cipher, macSizeInBits), parameters)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mac"><see cref="CbcBlockCipherMac"/></param>
    /// <param name="parameters">参数，例如密钥可以用<see cref="KeyParameter"/></param>
    public CBCBlockCipherMacEngine(CbcBlockCipherMac mac, ICipherParameters parameters)
        : base(mac, parameters)
    {
    }


}
