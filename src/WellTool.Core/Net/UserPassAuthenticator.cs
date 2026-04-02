using System.Net;

namespace WellTool.Core.Net
{
    /// <summary>
    /// 账号密码形式的认证器
    /// </summary>
    public class UserPassAuthenticator
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
        /// 构造
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        public UserPassAuthenticator(string user, char[] pass)
        {
            _user = user;
            _pass = new string(pass);
        }

        /// <summary>
        /// 获取认证凭据
        /// </summary>
        /// <returns>认证凭据</returns>
        public ICredentials GetCredentials()
        {
            return new NetworkCredential(_user, _pass);
        }

        /// <summary>
        /// 为WebRequest设置认证信息
        /// </summary>
        /// <param name="request">Web请求</param>
        public void SetCredentials(WebRequest request)
        {
            request.Credentials = GetCredentials();
        }
    }
}