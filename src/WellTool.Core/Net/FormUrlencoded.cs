using WellTool.Core.Codec;

namespace WellTool.Core.Net
{
    /// <summary>
    /// application/x-www-form-urlencoded，遵循W3C HTML Form content types规范，如空格须转+，+须被编码
    /// 规范见：https://url.spec.whatwg.org/#urlencoded-serializing
    /// </summary>
    public class FormUrlencoded
    {
        /// <summary>
        /// query中的value，默认除"-", "_", ".", "*"外都编码
        /// 这个类似于JDK提供的URLEncoder
        /// </summary>
        public static readonly PercentCodec All = RFC3986.Unreserved
            .RemoveSafe('~')
            .AddSafe('*')
            .SetEncodeSpaceAsPlus(true);
    }
}