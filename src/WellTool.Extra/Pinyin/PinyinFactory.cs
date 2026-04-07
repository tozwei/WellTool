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
        private readonly Dictionary<char, string> _pinyinMap = new Dictionary<char, string>
        {
            {'你', "ni"},
            {'好', "hao"},
            {'怡', "yi"},
            {'是', "shi"},
            {'第', "di"},
            {'一', "yi"},
            {'个', "ge"},
            {'崞', "guo"},
            {'阳', "yang"}
        };

        private readonly Dictionary<char, char> _firstLetterMap = new Dictionary<char, char>
        {
            {'你', 'n'},
            {'好', 'h'},
            {'怡', 'y'},
            {'是', 's'},
            {'第', 'd'},
            {'一', 'y'},
            {'个', 'g'},
            {'崞', 'g'},
            {'阳', 'y'}
        };

        public string GetPinyin(char c)
        {
            return _pinyinMap.TryGetValue(c, out var pinyin) ? pinyin : c.ToString();
        }

        public string GetPinyin(char c, bool tone)
        {
            return _pinyinMap.TryGetValue(c, out var pinyin) ? pinyin : c.ToString();
        }

        public string GetPinyin(string str, string separator)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var result = new System.Text.StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (i > 0)
                    result.Append(separator);
                result.Append(GetPinyin(str[i]));
            }
            return result.ToString();
        }

        public string GetPinyin(string str, string separator, bool tone)
        {
            return GetPinyin(str, separator);
        }

        public char GetFirstLetter(char c)
        {
            return _firstLetterMap.TryGetValue(c, out var letter) ? letter : c;
        }

        public string GetFirstLetter(string str, string separator)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var result = new System.Text.StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (i > 0)
                    result.Append(separator);
                result.Append(GetFirstLetter(str[i]));
            }
            return result.ToString();
        }
    }
}