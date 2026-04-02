using System.Security.Cryptography;

namespace WellTool.Crypto
{
    public static class ProviderFactory
    {
        public static CspParameters CreateCspParameters(string providerName, int keySize)
        {
            return new CspParameters
            {
                ProviderName = providerName,
                KeyNumber = (int)KeyNumber.Exchange
            };
        }

        public static RSA CreateRSA(int keySize = 2048)
        {
            return RSA.Create(keySize);
        }

        public static Aes CreateAES(int keySize = 256)
        {
            var aes = Aes.Create();
            aes.KeySize = keySize;
            return aes;
        }

        public static DES CreateDES()
        {
            return DES.Create();
        }

        public static TripleDES CreateTripleDES()
        {
            return TripleDES.Create();
        }
    }
}