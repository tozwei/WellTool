namespace WellTool.Crypto.Symmetric.Fpe
{
    public interface FPE
    {
        byte[] Encrypt(byte[] data, byte[] key, byte[] tweak);
        byte[] Decrypt(byte[] data, byte[] key, byte[] tweak);
        string Encrypt(string data, string key, string tweak, System.Text.Encoding encoding = null);
        string Decrypt(string data, string key, string tweak, System.Text.Encoding encoding = null);
    }
}