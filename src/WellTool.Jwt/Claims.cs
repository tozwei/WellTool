using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace WellTool.JWT
{
    /// <summary>
    /// Claims 认证，简单的JsonDocument包装
    /// </summary>
    public class Claims
    {
        private Dictionary<string, object> _claims;

        /// <summary>
        /// 增加Claims属性，如果属性值为{@code null}，则移除这个属性
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        protected void SetClaim(string name, object value)
        {
            Init();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name must be not null!");
            }
            if (value == null)
            {
                _claims.Remove(name);
                return;
            }
            _claims[name] = value;
        }

        /// <summary>
        /// 加入多个Claims属性
        /// </summary>
        /// <param name="headerClaims">多个Claims属性</param>
        protected void PutAll(Dictionary<string, object> headerClaims)
        {
            if (headerClaims != null && headerClaims.Count > 0)
            {
                foreach (var entry in headerClaims)
                {
                    SetClaim(entry.Key, entry.Value);
                }
            }
        }

        /// <summary>
        /// 获取指定名称属性
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>属性</returns>
        public object GetClaim(string name)
        {
            Init();
            _claims.TryGetValue(name, out var value);
            return value;
        }

        /// <summary>
        /// 获取Claims的JSON字符串形式
        /// </summary>
        /// <returns>JSON字符串</returns>
        public Dictionary<string, object> GetClaimsJson()
        {
            Init();
            return _claims;
        }

        /// <summary>
        /// 解析JWT JSON
        /// </summary>
        /// <param name="tokenPart">JWT JSON</param>
        public void Parse(string tokenPart)
        {
            var jsonStr = JWT.Base64UrlDecode(tokenPart, Encoding.UTF8);
            _claims = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonStr);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            Init();
            return JsonSerializer.Serialize(_claims);
        }

        private void Init()
        {
            if (_claims == null)
            {
                _claims = new Dictionary<string, object>();
            }
        }
    }
}