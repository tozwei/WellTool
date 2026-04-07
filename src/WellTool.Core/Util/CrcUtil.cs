using System;
using System.Security.Cryptography;

namespace WellTool.Core.Util;

/// <summary>
/// CRC工具类
/// </summary>
public static class CrcUtil
{
    /// <summary>
    /// 计算CRC32值
    /// </summary>
    /// <param name="data">数据</param>
    /// <returns>CRC32值</returns>
    public static uint Crc32(byte[] data)
    {
        if (data == null || data.Length == 0)
        {
            return 0;
        }

        using var crc32 = new Crc32Algorithm();
        var hash = crc32.ComputeHash(data);
        return BitConverter.ToUInt32(hash, 0);
    }

    /// <summary>
    /// 计算CRC32值
    /// </summary>
    /// <param name="text">文本</param>
    /// <returns>CRC32值</returns>
    public static uint Crc32(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }

        var data = System.Text.Encoding.UTF8.GetBytes(text);
        return Crc32(data);
    }

    /// <summary>
    /// 计算CRC16值
    /// </summary>
    /// <param name="data">数据</param>
    /// <returns>CRC16值</returns>
    public static ushort Crc16(byte[] data)
    {
        if (data == null || data.Length == 0)
        {
            return 0;
        }

        ushort crc = 0xFFFF;
        for (int i = 0; i < data.Length; i++)
        {
            crc ^= (ushort)(data[i] << 8);
            for (int j = 0; j < 8; j++)
            {
                if ((crc & 0x8000) != 0)
                {
                    crc = (ushort)((crc << 1) ^ 0x1021);
                }
                else
                {
                    crc <<= 1;
                }
            }
        }
        return crc;
    }

    /// <summary>
    /// 计算CRC16值
    /// </summary>
    /// <param name="text">文本</param>
    /// <returns>CRC16值</returns>
    public static ushort Crc16(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }

        var data = System.Text.Encoding.UTF8.GetBytes(text);
        return Crc16(data);
    }
}

/// <summary>
/// CRC32算法实现
/// </summary>
internal class Crc32Algorithm : HashAlgorithm
{
    private const uint Polynomial = 0xEDB88320;
    private static readonly uint[] _table = new uint[256];

    static Crc32Algorithm()
    {
        for (uint i = 0; i < 256; i++)
        {
            uint crc = i;
            for (int j = 0; j < 8; j++)
            {
                crc = (crc >> 1) ^ (Polynomial & (uint)-(crc & 1));
            }
            _table[i] = crc;
        }
    }

    private uint _crc = 0xFFFFFFFF;

    public override void Initialize()
    {
        _crc = 0xFFFFFFFF;
    }

    protected override void HashCore(byte[] array, int ibStart, int cbSize)
    {
        for (int i = ibStart; i < ibStart + cbSize; i++)
        {
            _crc = (_crc >> 8) ^ _table[array[i] ^ (_crc & 0xFF)];
        }
    }

    protected override byte[] HashFinal()
    {
        _crc ^= 0xFFFFFFFF;
        return BitConverter.GetBytes(_crc);
    }
}
