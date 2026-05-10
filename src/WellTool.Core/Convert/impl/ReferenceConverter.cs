using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 寮曠敤绫诲瀷杞崲鍣?
    /// </summary>
    public class ReferenceConverter : IConverter
    {
        /// <summary>
        /// 杞崲鍊?
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            // 濡傛灉鍊兼槸 WeakReference 鎴栫被浼煎紩鐢ㄧ被鍨嬶紝鎻愬彇鍏剁洰鏍?
            if (value is WeakReference wr)
            {
                return wr.Target;
            }

            // 濡傛灉鍊煎凡缁忔槸鐩爣寮曠敤绫诲瀷锛岀洿鎺ヨ繑鍥?
            if (targetType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            // 灏濊瘯杞崲
            return System.Convert.ChangeType(value, targetType);
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(WeakReference), typeof(object) };
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被鍨?
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(object) };
        }
    }
}

