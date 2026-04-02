namespace WellTool.Crypto.Digest.Mac
{
    public class MacBuilder
    {
        private MacEngine _engine;
        private byte[] _key;

        public MacBuilder(MacAlgorithm algorithm)
        {
            _engine = MacFactory.Create(algorithm);
        }

        public MacBuilder SetKey(byte[] key)
        {
            _key = key;
            return this;
        }

        public MacBuilder SetKey(string key, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            _key = encoding.GetBytes(key);
            return this;
        }

        public MacBuilder Update(byte[] data)
        {
            if (_key == null)
            {
                throw new System.InvalidOperationException("Key not set");
            }
            _engine.Init(_key);
            _engine.Update(data);
            return this;
        }

        public MacBuilder Update(string data, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            return Update(encoding.GetBytes(data));
        }

        public byte[] Build()
        {
            return _engine.DoFinal();
        }

        public string BuildHex()
        {
            var mac = Build();
            return BitConverter.ToString(mac).Replace("-", "").ToLower();
        }
    }
}