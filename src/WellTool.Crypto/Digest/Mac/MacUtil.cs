using System.Text;

namespace WellTool.Crypto.Digest.Mac
{
    public static class MacUtil
    {
        public static byte[] Mac(byte[] data, byte[] key, MacAlgorithm algorithm)
        {
            var engine = new HMacEngine(algorithm);
            engine.Init(key);
            engine.Update(data);
            return engine.DoFinal();
        }

        public static string MacHex(byte[] data, byte[] key, MacAlgorithm algorithm)
        {
            var mac = Mac(data, key, algorithm);
            return BitConverter.ToString(mac).Replace("-", "").ToLower();
        }

        public static string MacHex(string data, string key, MacAlgorithm algorithm, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            return MacHex(encoding.GetBytes(data), encoding.GetBytes(key), algorithm);
        }
    }
}