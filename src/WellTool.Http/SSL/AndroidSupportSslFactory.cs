using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WellTool.Http.SSL
{
    /// <summary>
    /// 兼容Android低版本SSL连接
    /// <para>
    /// 在测试HttpUrlConnection的时候，发现一部分手机无法连接某些HTTPS站点，
    /// </para>
    /// </summary>
    public class AndroidSupportSslFactory : CustomProtocolsSslFactory
    {
        /// <summary>
        /// Android低版本SSL协议列表
        /// </summary>
        private static readonly string[] Protocols = new[]
        {
            "SSLv3",
            "TLSv1",
            "TLSv1.1",
            "TLSv1.2"
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        public AndroidSupportSslFactory() : base(Protocols)
        {
        }
    }
}
