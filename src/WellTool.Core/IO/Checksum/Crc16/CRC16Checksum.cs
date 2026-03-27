using System;

namespace WellTool.Core.IO.Checksum.Crc16
{
    /// <summary>
    /// CRC16 Checksum，用于提供多种CRC16算法的通用实现
    /// 通过继承此类，重写update和reset完成相应算法。
    /// </summary>
    public abstract class CRC16Checksum
    {
        /// <summary>
        /// CRC16 Checksum 结果值
        /// </summary>
        protected int wCRCin;

        public CRC16Checksum()
        {
            Reset();
        }

        /// <summary>
        /// Returns the current checksum value.
        /// </summary>
        /// <returns>the current checksum value</returns>
        public long GetValue()
        {
            return wCRCin;
        }

        /// <summary>
        /// 获取16进制的CRC16值
        /// </summary>
        /// <returns>16进制的CRC16值</returns>
        public string GetHexValue()
        {
            return GetHexValue(false);
        }

        /// <summary>
        /// 获取16进制的CRC16值
        /// </summary>
        /// <param name="isPadding">不足4位时，是否填充0以满足位数</param>
        /// <returns>16进制的CRC16值，4位</returns>
        public string GetHexValue(bool isPadding)
        {
            string hex = GetValue().ToString("X");
            if (isPadding)
            {
                hex = hex.PadLeft(4, '0');
            }

            return hex;
        }

        /// <summary>
        /// Resets the checksum to its initial value.
        /// </summary>
        public virtual void Reset()
        {
            wCRCin = 0x0000;
        }

        /// <summary>
        /// 计算全部字节
        /// </summary>
        /// <param name="b">字节</param>
        public void Update(byte[] b)
        {
            Update(b, 0, b.Length);
        }

        /// <summary>
        /// Updates the current checksum with the specified array of bytes.
        /// </summary>
        /// <param name="b">the byte array to update the checksum with</param>
        /// <param name="off">the start offset in the data</param>
        /// <param name="len">the number of bytes to use for the update</param>
        public virtual void Update(byte[] b, int off, int len)
        {
            for (int i = off; i < off + len; i++)
                Update(b[i]);
        }

        /// <summary>
        /// Updates the current checksum with the specified byte.
        /// </summary>
        /// <param name="b">the byte to update the checksum with</param>
        public abstract void Update(int b);
    }
}