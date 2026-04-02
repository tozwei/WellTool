namespace WellTool.Crypto.Digest.Mac
{
    public class MacException : CryptoException
    {
        public MacException() { }

        public MacException(string message) : base(message) { }

        public MacException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}