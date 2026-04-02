namespace WellTool.Crypto.Digest.Mac
{
    public class MacConfig
    {
        public MacAlgorithm Algorithm { get; set; }
        public byte[] Key { get; set; }
        public System.Text.Encoding Encoding { get; set; } = System.Text.Encoding.UTF8;

        public MacConfig() { }

        public MacConfig(MacAlgorithm algorithm, byte[] key)
        {
            Algorithm = algorithm;
            Key = key;
        }

        public MacConfig(MacAlgorithm algorithm, string key, System.Text.Encoding encoding = null)
        {
            Algorithm = algorithm;
            Encoding = encoding ?? System.Text.Encoding.UTF8;
            Key = Encoding.GetBytes(key);
        }
    }
}