using WellTool.Core.IO.Checksum.Crc16;

namespace WellTool.Core.IO.Checksum
{
    /// <summary>
    /// CRC16 循环冗余校验码（Cyclic Redundancy Check）实现，默认IBM算法
    /// </summary>
    public class CRC16
    {
        private readonly CRC16Checksum crc16;

        public CRC16() : this(new CRC16IBM())
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="crc16Checksum"><see cref="CRC16Checksum"/> 实现</param>
        public CRC16(CRC16Checksum crc16Checksum) {
            this.crc16 = crc16Checksum;
        }

        /// <summary>
        /// 获取16进制的CRC16值
        /// </summary>
        /// <returns>16进制的CRC16值</returns>
        public string GetHexValue() {
            return this.crc16.GetHexValue();
        }

        /// <summary>
        /// 获取16进制的CRC16值
        /// </summary>
        /// <param name="isPadding">不足4位时，是否填充0以满足位数</param>
        /// <returns>16进制的CRC16值，4位</returns>
        public string GetHexValue(bool isPadding) {
            return crc16.GetHexValue(isPadding);
        }

        /// <summary>
        /// Returns the current checksum value.
        /// </summary>
        /// <returns>the current checksum value</returns>
        public long GetValue() {
            return crc16.GetValue();
        }

        /// <summary>
        /// Resets the checksum to its initial value.
        /// </summary>
        public void Reset() {
            crc16.Reset();
        }

        /// <summary>
        /// Updates the current checksum with the specified array of bytes.
        /// </summary>
        /// <param name="b">the byte array to update the checksum with</param>
        /// <param name="off">the start offset in the data</param>
        /// <param name="len">the number of bytes to use for the update</param>
        public void Update(byte[] b, int off, int len) {
            crc16.Update(b, off, len);
        }

        /// <summary>
        /// Updates the current checksum with the specified byte.
        /// </summary>
        /// <param name="b">the byte to update the checksum with</param>
        public void Update(int b) {
            crc16.Update(b);
        }
    }
}