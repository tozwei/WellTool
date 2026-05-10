using System;
using System.Collections.Concurrent;
using System.Text;

namespace WellTool.Setting.Profile
{
    public class Profile
    {
        public static readonly string DEFAULT_PROFILE = "default";

        private string _profile;
        private Encoding _charset;
        private bool _useVar;
        private readonly ConcurrentDictionary<string, Setting> _settingMap = new ConcurrentDictionary<string, Setting>();

        public Profile() : this(DEFAULT_PROFILE)
        {
        }

        public Profile(string profile) : this(profile, Encoding.UTF8, false)
        {
        }

        public Profile(string profile, Encoding charset, bool useVar)
        {
            _profile = profile;
            _charset = charset;
            _useVar = useVar;
        }

        public Setting GetSetting(string name)
        {
            string nameForProfile = FixNameForProfile(name);
            return _settingMap.GetOrAdd(nameForProfile, key => new Setting(key, _charset, _useVar));
        }

        public Profile SetProfile(string profile)
        {
            _profile = profile;
            return this;
        }

        public Profile SetCharset(Encoding charset)
        {
            _charset = charset;
            return this;
        }

        public Profile SetUseVar(bool useVar)
        {
            _useVar = useVar;
            return this;
        }

        public Profile Clear()
        {
            _settingMap.Clear();
            return this;
        }

        private string FixNameForProfile(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Setting name must be not blank !");
            }
            string actualProfile = string.IsNullOrEmpty(_profile) ? "" : _profile;
            if (!name.Contains("."))
            {
                return string.Format("{0}/{1}.setting", actualProfile, name);
            }
            return string.Format("{0}/{1}", actualProfile, name);
        }
    }
}
