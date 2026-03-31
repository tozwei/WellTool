namespace WellTool.Crypto.Digest
{
    public class SM3
    {
        public static byte[] Digest(byte[] data)
        {
            // 这里使用Bouncy Castle实现SM3
            // 实际实现需要引用Bouncy Castle库
            throw new NotImplementedException("SM3 algorithm requires Bouncy Castle library");
        }
        
        public static string DigestHex(byte[] data)
        {
            var digest = Digest(data);
            return BitConverter.ToString(digest).Replace("-", "").ToLower();
        }
        
        public static string DigestHex(string data)
        {
            return DigestHex(System.Text.Encoding.UTF8.GetBytes(data));
        }
    }
}