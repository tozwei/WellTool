namespace WellTool.Extra.Pinyin
{
    /// <summary>
    /// 拼音引擎工厂
    /// </summary>
    public static class PinyinFactory
    {
        private static PinyinEngine _engine;

        /// <summary>
        /// 获取拼音引擎实例
        /// </summary>
        /// <returns>拼音引擎实例</returns>
        public static PinyinEngine Get()
        {
            if (_engine == null)
            {
                // 默认实现，实际项目中可以根据需要替换为具体的拼音引擎实现
                _engine = new DefaultPinyinEngine();
            }
            return _engine;
        }

        /// <summary>
        /// 设置拼音引擎实例
        /// </summary>
        /// <param name="engine">拼音引擎实例</param>
        public static void Set(PinyinEngine engine)
        {
            _engine = engine;
        }
    }

    /// <summary>
    /// 默认拼音引擎实现
    /// </summary>
    internal class DefaultPinyinEngine : PinyinEngine
    {
        public string GetPinyin(char c)
        {
            return c.ToString();
        }

        public string GetPinyin(char c, bool tone)
        {
            return c.ToString();
        }

        public string GetPinyin(string str, string separator)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return str;
        }

        public string GetPinyin(string str, string separator, bool tone)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return str;
        }

        public char GetFirstLetter(char c)
        {
            return c;
        }

        public string GetFirstLetter(string str, string separator)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return str;
        }
    }
}