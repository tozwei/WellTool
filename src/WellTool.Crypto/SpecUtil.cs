using System.Security.Cryptography;

namespace WellTool.Crypto
{
    public static class SpecUtil
    {
        public static CipherMode GetCipherMode(Mode mode)
        {
            return mode switch
            {
                Mode.ECB => CipherMode.ECB,
                Mode.CBC => CipherMode.CBC,
                Mode.CFB => CipherMode.CFB,
                Mode.OFB => CipherMode.OFB,
                Mode.CTR => CipherMode.CTR,
                _ => throw new System.ArgumentException("Invalid cipher mode")
            };
        }

        public static PaddingMode GetPaddingMode(Padding padding)
        {
            return padding switch
            {
                Padding.NoPadding => PaddingMode.None,
                Padding.PKCS5Padding => PaddingMode.PKCS7,
                Padding.PKCS7Padding => PaddingMode.PKCS7,
                _ => throw new System.ArgumentException("Invalid padding mode")
            };
        }

        public static int GetKeySize(SymmetricAlgorithm algorithm, int keySize)
        {
            var legalKeySizes = algorithm.LegalKeySizes;
            foreach (var size in legalKeySizes)
            {
                if (keySize >= size.MinSize && keySize <= size.MaxSize && (keySize - size.MinSize) % size.SkipSize == 0)
                {
                    return keySize;
                }
            }
            throw new System.ArgumentException($"Invalid key size: {keySize}");
        }

        public static int GetBlockSize(SymmetricAlgorithm algorithm)
        {
            return algorithm.BlockSize;
        }
    }
}