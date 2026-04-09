namespace WellTool.Http.Cookie;

/// <summary>
/// Cookie 类，用于表示 HTTP Cookie
/// </summary>
public class Cookie
{
    /// <summary>
    /// Cookie 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Cookie 值
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 域名
    /// </summary>
    public string? Domain { get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    public string Path { get; set; } = "/";

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime? Expires { get; set; }

    /// <summary>
    /// 是否仅在 HTTPS 下传输
    /// </summary>
    public bool Secure { get; set; }

    /// <summary>
    /// 是否仅在 HTTP 协议下访问
    /// </summary>
    public bool HttpOnly { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">Cookie 名称</param>
    /// <param name="value">Cookie 值</param>
    public Cookie(string name, string value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// 转换为 Cookie 字符串
    /// </summary>
    /// <returns>Cookie 字符串</returns>
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.Append($"{Name}={Value}");
        
        if (!string.IsNullOrEmpty(Domain))
        {
            sb.Append($"; Domain={Domain}");
        }
        
        if (!string.IsNullOrEmpty(Path))
        {
            sb.Append($"; Path={Path}");
        }
        
        if (Expires.HasValue)
        {
            sb.Append($"; Expires={Expires.Value.ToString("R")}");
        }
        
        if (Secure)
        {
            sb.Append("; Secure");
        }
        
        if (HttpOnly)
        {
            sb.Append("; HttpOnly");
        }
        
        return sb.ToString();
    }

    /// <summary>
    /// 从 Set-Cookie 头解析 Cookie
    /// </summary>
    /// <param name="setCookieHeader">Set-Cookie 头值</param>
    /// <returns>Cookie 对象</returns>
    public static Cookie? Parse(string setCookieHeader)
    {
        if (string.IsNullOrEmpty(setCookieHeader))
        {
            return null;
        }

        var parts = setCookieHeader.Split(';');
        if (parts.Length == 0)
        {
            return null;
        }

        // 解析名称和值
        var nameValue = parts[0].Trim();
        var nameValueParts = nameValue.Split('=');
        if (nameValueParts.Length < 2)
        {
            return null;
        }

        var cookie = new Cookie(nameValueParts[0].Trim(), nameValueParts[1].Trim());

        // 解析其他属性
        for (int i = 1; i < parts.Length; i++)
        {
            var part = parts[i].Trim();
            var attrParts = part.Split('=');
            var attrName = attrParts[0].Trim().ToLower();
            var attrValue = attrParts.Length > 1 ? attrParts[1].Trim() : string.Empty;

            switch (attrName)
            {
                case "domain":
                    cookie.Domain = attrValue;
                    break;
                case "path":
                    cookie.Path = attrValue;
                    break;
                case "expires":
                    if (DateTime.TryParse(attrValue, out var expires))
                    {
                        cookie.Expires = expires;
                    }
                    break;
                case "secure":
                    cookie.Secure = true;
                    break;
                case "httponly":
                    cookie.HttpOnly = true;
                    break;
            }
        }

        return cookie;
    }
}
