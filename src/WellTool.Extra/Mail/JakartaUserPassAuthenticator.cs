using System.Net;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// Jakarta用户密码认证器
    /// 用于 Jakarta Mail 的认证
    /// </summary>
    public class JakartaUserPassAuthenticator : ICredentials
    {
        private readonly string _user;
        private readonly string _password;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        public JakartaUserPassAuthenticator(string user, string password)
        {
            _user = user;
            _password = password;
        }

        /// <summary>
        /// 获取凭证
        /// </summary>
        public NetworkCredential GetCredential(string domain, string username)
        {
            return new NetworkCredential(_user, _password);
        }
    }
}
