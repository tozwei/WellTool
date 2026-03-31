using System;

namespace WellTool.JWT
{
    /// <summary>
    /// 注册的标准载荷（Payload）声明
    /// </summary>
    /// <typeparam name="T">实现此接口的类的类型</typeparam>
    public interface RegisteredPayload<T> where T : RegisteredPayload<T>
    {
        /// <summary>
        /// 设置 jwt签发者("iss")的Payload值
        /// </summary>
        /// <param name="issuer">jwt签发者</param>
        /// <returns>this</returns>
        T SetIssuer(string issuer);

        /// <summary>
        /// 设置jwt所面向的用户("sub")的Payload值
        /// </summary>
        /// <param name="subject">jwt所面向的用户</param>
        /// <returns>this</returns>
        T SetSubject(string subject);

        /// <summary>
        /// 设置接收jwt的一方("aud")的Payload值
        /// </summary>
        /// <param name="audience">接收jwt的一方</param>
        /// <returns>this</returns>
        T SetAudience(params string[] audience);

        /// <summary>
        /// 设置jwt的过期时间("exp")的Payload值，这个过期时间必须要大于签发时间
        /// </summary>
        /// <param name="expiresAt">jwt的过期时间</param>
        /// <returns>this</returns>
        T SetExpiresAt(DateTime expiresAt);

        /// <summary>
        /// 设置不可用时间点界限("nbf")的Payload值
        /// </summary>
        /// <param name="notBefore">不可用时间点界限，在这个时间点之前，jwt不可用</param>
        /// <returns>this</returns>
        T SetNotBefore(DateTime notBefore);

        /// <summary>
        /// 设置jwt的签发时间("iat")
        /// </summary>
        /// <param name="issuedAt">签发时间</param>
        /// <returns>this</returns>
        T SetIssuedAt(DateTime issuedAt);

        /// <summary>
        /// 设置jwt的唯一身份标识("jti")
        /// </summary>
        /// <param name="jwtId">唯一身份标识</param>
        /// <returns>this</returns>
        T SetJWTId(string jwtId);

        /// <summary>
        /// 设置Payload值
        /// </summary>
        /// <param name="name">payload名</param>
        /// <param name="value">payload值</param>
        /// <returns>this</returns>
        T SetPayload(string name, object value);
    }
}