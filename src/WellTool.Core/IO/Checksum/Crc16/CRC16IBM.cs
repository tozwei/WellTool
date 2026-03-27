namespace WellTool.Core.IO.Checksum.Crc16
{
    /// <summary>
    /// CRC16_IBM：多项式x16+x15+x2+1（0x8005），初始值0x0000，低位在前，高位在后，结果与0x0000异或
    /// 0xA001是0x8005按位颠倒后的结果
    /// </summary>
    public class CRC16IBM : CRC16Checksum
    {
        private static readonly int WC_POLY = 0xa001;

        /// <summary>
        /// Updates the current checksum with the specified byte.
        /// </summary>
        /// <param name="b">the byte to update the checksum with</param>
        public override void Update(int b)
        {
            wCRCin ^= (b & 0x00ff);
            for (int j = 0; j < 8; j++)
            {
                if ((wCRCin & 0x0001) != 0)
                {
                    wCRCin >>= 1;
                    wCRCin ^= WC_POLY;
                }
                else
                {
                    wCRCin >>= 1;
                }
            }
        }
    }
}