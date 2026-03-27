using System;

namespace WellTool.Core.IO.Checksum
{
    /// <summary>
    /// CRC8 循环冗余校验码（Cyclic Redundancy Check）实现
    /// 代码来自：https://github.com/BBSc0der
    /// </summary>
    public class CRC8
    {
        private readonly short init;
        private readonly short[] crcTable = new short[256];
        private short value;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="polynomial">Polynomial, typically one of the POLYNOMIAL_* constants.</param>
        /// <param name="init">Initial value, typically either 0xff or zero.</param>
        public CRC8(int polynomial, short init)
        {
            this.value = this.init = init;
            for (int dividend = 0; dividend < 256; dividend++)
            {
                int remainder = dividend;// << 8;
                for (int bit = 0; bit < 8; ++bit)
                {
                    if ((remainder & 0x01) != 0)
                    {
                        remainder = (remainder >> 1) ^ polynomial;
                    }
                    else
                    {
                        remainder >>= 1;
                    }
                }
                crcTable[dividend] = (short)remainder;
            }
        }

        /// <summary>
        /// Updates the current checksum with the specified array of bytes.
        /// </summary>
        /// <param name="buffer">the byte array to update the checksum with</param>
        /// <param name="offset">the start offset in the data</param>
        /// <param name="len">the number of bytes to use for the update</param>
        public void Update(byte[] buffer, int offset, int len)
        {
            for (int i = 0; i < len; i++)
            {
                int data = buffer[offset + i] ^ value;
                value = (short)(crcTable[data & 0xff] ^ (value << 8));
            }
        }

        /// <summary>
        /// Updates the current checksum with the specified array of bytes. Equivalent to calling <code>Update(buffer, 0, buffer.Length)</code>.
        /// </summary>
        /// <param name="buffer">the byte array to update the checksum with</param>
        public void Update(byte[] buffer)
        {
            Update(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Updates the current checksum with the specified byte.
        /// </summary>
        /// <param name="b">the byte to update the checksum with</param>
        public void Update(int b)
        {
            Update(new byte[] { (byte)b }, 0, 1);
        }

        /// <summary>
        /// Returns the current checksum value.
        /// </summary>
        /// <returns>the current checksum value</returns>
        public long GetValue()
        {
            return value & 0xff;
        }

        /// <summary>
        /// Resets the checksum to its initial value.
        /// </summary>
        public void Reset()
        {
            value = init;
        }
    }
}