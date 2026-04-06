using System.Net;
using System.Net.Mail;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// 用户名密码验证器
    /// </summary>
    public class UserPassAuthenticator : ICredentials
    {
        private readonly string _user;
        private readonly string _pass;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        public UserPassAuthenticator(string user, string pass)
        {
            _user = user;
            _pass = pass;
        }

        /// <summary>
        /// 获取网络凭证
        /// </summary>
        /// <param name="uri">统一资源标识符</param>
        /// <param name="authType">认证类型</param>
        /// <returns>网络凭证</returns>
        public NetworkCredential GetCredential(Uri uri, string authType)
        {
            return new NetworkCredential(_user, _pass);
        }
    }
}
