using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 瀛楃涓茶浆鎹㈠櫒
    /// </summary>
    public class StringConverter : IConverter
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
            return value.ToString();
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
        /// </summary>
        /// <returns>鏀寔鐨勬簮绫诲瀷鏁扮粍</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(object) };
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被鍨?        /// </summary>
        /// <returns>鏀寔鐨勭洰鏍囩被鍨嬫暟缁?/returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(string) };
        }
    }
}
