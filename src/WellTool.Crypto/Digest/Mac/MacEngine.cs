namespace WellTool.Crypto.Digest.Mac
{
    public interface MacEngine
    {
        void Init(byte[] key);
        void Update(byte[] data);
        byte[] DoFinal();
        void Reset();
    }
}