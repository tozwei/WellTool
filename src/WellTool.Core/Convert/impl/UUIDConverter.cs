using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// UUID杞崲鍣?    /// </summary>
    public class UUIDConverter : IConverter
    {
        /// <summary>
        /// 杞崲鍊?        /// </summary>
        /// <param name="value">瑕佽浆鎹㈢殑鍊?/param>
        /// <param name="targetType">鐩爣绫诲瀷</param>
        /// <returns>杞崲鍚庣殑鍊?/returns>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }
            if (value is Guid guid)
            {
                return guid;
            }
            var str = value.ToString().Trim();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            return Guid.Parse(str);
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
        /// </summary>
        /// <returns>鏀寔鐨勬簮绫诲瀷鏁扮粍</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(string), typeof(Guid) };
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被鍨?        /// </summary>
        /// <returns>鏀寔鐨勭洰鏍囩被鍨嬫暟缁?/returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(Guid) };
        }
    }
}
