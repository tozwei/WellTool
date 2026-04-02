namespace WellTool.Crypto.Digest.Mac
{
    public static class MacFactory
    {
        public static MacEngine Create(MacAlgorithm algorithm)
        {
            return new HMacEngine(algorithm);
        }
    }
}