using System;

namespace WellTool.Core.Getter
{
    public interface IBasicTypeGetter
    {
        bool GetBool(string key, bool defaultValue = false);
        char GetChar(string key, char defaultValue = '\0');
        byte GetByte(string key, byte defaultValue = 0);
        short GetShort(string key, short defaultValue = 0);
        int GetInt(string key, int defaultValue = 0);
        long GetLong(string key, long defaultValue = 0);
        float GetFloat(string key, float defaultValue = 0f);
        double GetDouble(string key, double defaultValue = 0d);
        string GetString(string key, string defaultValue = null);
        T GetEnum<T>(string key, T defaultValue = default) where T : Enum;
    }

    public abstract class BasicTypeGetter : IBasicTypeGetter
    {
        public abstract object GetObject(string key);

        public bool GetBool(string key, bool defaultValue = false)
        {
            var obj = GetObject(key);
            return obj == null ? defaultValue : System.Convert.ToBoolean(obj);
        }

        public char GetChar(string key, char defaultValue = '\0')
        {
            var obj = GetObject(key);
            return obj == null ? defaultValue : System.Convert.ToChar(obj);
        }

        public byte GetByte(string key, byte defaultValue = 0)
        {
            var obj = GetObject(key);
            return obj == null ? defaultValue : System.Convert.ToByte(obj);
        }

        public short GetShort(string key, short defaultValue = 0)
        {
            var obj = GetObject(key);
            return obj == null ? defaultValue : System.Convert.ToInt16(obj);
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            var obj = GetObject(key);
            return obj == null ? defaultValue : System.Convert.ToInt32(obj);
        }

        public long GetLong(string key, long defaultValue = 0)
        {
            var obj = GetObject(key);
            return obj == null ? defaultValue : System.Convert.ToInt64(obj);
        }

        public float GetFloat(string key, float defaultValue = 0f)
        {
            var obj = GetObject(key);
            return obj == null ? defaultValue : System.Convert.ToSingle(obj);
        }

        public double GetDouble(string key, double defaultValue = 0d)
        {
            var obj = GetObject(key);
            return obj == null ? defaultValue : System.Convert.ToDouble(obj);
        }

        public string GetString(string key, string defaultValue = null)
        {
            var obj = GetObject(key);
            return obj == null ? defaultValue : obj.ToString();
        }

        public T GetEnum<T>(string key, T defaultValue = default) where T : Enum
        {
            var obj = GetObject(key);
            if (obj == null)
            {
                return defaultValue;
            }
            return (T)Enum.Parse(typeof(T), obj.ToString());
        }
    }
}
