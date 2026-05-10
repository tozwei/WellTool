namespace WellTool.Setting.Profile
{
    public static class GlobalProfile
    {
        private static Profile? _instance;
        private static readonly object _lock = new object();

        public static Profile SetProfile(string profile)
        {
            lock (_lock)
            {
                _instance = new Profile(profile);
                return _instance;
            }
        }

        public static Setting GetSetting(string settingName)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Profile();
                }
                return _instance.GetSetting(settingName);
            }
        }
    }
}
