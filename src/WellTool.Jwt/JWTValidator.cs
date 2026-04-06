using System;
using WellTool.JWT.Signers;
using WellTool.JWT.Exceptions;

namespace WellTool.JWT
{
    /// <summary>
    /// JWT数据校验器，用于校验包括：
    /// <ul>
    ///     <li>算法是否一致</li>
    ///     <li>算法签名是否正确</li>
    ///     <li>字段值是否有效（例如时间未过期等）</li>
    /// </ul>
    /// </summary>
    public class JWTValidator
    {
        private readonly JWT _jwt;

        /// <summary>
        /// 创建JWT验证器
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <returns>JWTValidator</returns>
        public static JWTValidator Of(string token)
        {
            return new JWTValidator(JWT.Of(token));
        }

        /// <summary>
        /// 创建JWT验证器
        /// </summary>
        /// <param name="jwt">JWT对象</param>
        /// <returns>JWTValidator</returns>
        public static JWTValidator Of(JWT jwt)
        {
            return new JWTValidator(jwt);
        }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="jwt">JWT对象</param>
    public JWTValidator(JWT jwt)
    {
        _jwt = jwt;
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="jwt">JWT对象</param>
    /// <param name="key">密钥</param>
    public JWTValidator(JWT jwt, byte[] key)
    {
        _jwt = jwt;
        _jwt.SetKey(key);
    }

    /// <summary>
    /// 验证JWT（验证算法和日期）
    /// </summary>
    /// <returns>验证是否通过</returns>
    public bool Validate()
    {
        try
        {
            ValidateAlgorithm();
            ValidateDate();
            return true;
        }
        catch (ValidateException)
        {
            return false;
        }
    }

    /// <summary>
    /// 验证算法，使用JWT对象自带的{@link IJwtSigner}
    /// </summary>
        /// <returns>this</returns>
        /// <exception cref="ValidateException">验证失败的异常</exception>
        public JWTValidator ValidateAlgorithm()
        {
            return ValidateAlgorithm(null);
        }

        /// <summary>
        /// 验证算法，使用自定义的{@link IJwtSigner}
        /// </summary>
        /// <param name="signer">用于验证算法的签名器</param>
        /// <returns>this</returns>
        /// <exception cref="ValidateException">验证失败的异常</exception>
        public JWTValidator ValidateAlgorithm(IJwtSigner signer)
        {
            ValidateAlgorithm(_jwt, signer);
            return this;
        }

        /// <summary>
        /// 检查JWT的以下三两个时间：
        /// 
        /// <ul>
        ///     <li>{@link JWTPayload#NOT_BEFORE}：被检查时间必须晚于生效时间</li>
        ///     <li>{@link JWTPayload#EXPIRES_AT}：被检查时间必须早于失效时间</li>
        ///     <li>{@link JWTPayload#ISSUED_AT}：签发时间必须早于失效时间</li>
        /// </ul>
        /// <p>
        /// 如果某个时间没有设置，则不检查（表示无限制）
        /// </p>
        /// </summary>
        /// <returns>this</returns>
        /// <exception cref="ValidateException">验证失败的异常</exception>
        public JWTValidator ValidateDate()
        {
            return ValidateDate(DateTime.UtcNow);
        }

        /// <summary>
        /// 检查JWT的以下三两个时间：
        /// 
        /// <ul>
        ///     <li>{@link JWTPayload#NOT_BEFORE}：生效时间不能晚于当前时间</li>
        ///     <li>{@link JWTPayload#EXPIRES_AT}：失效时间不能早于当前时间</li>
        ///     <li>{@link JWTPayload#ISSUED_AT}： 签发时间不能晚于当前时间</li>
        /// </ul>
        /// <p>
        /// 如果某个时间没有设置，则不检查（表示无限制）
        /// </p>
        /// </summary>
        /// <param name="dateToCheck">被检查的时间，一般为当前时间</param>
        /// <returns>this</returns>
        /// <exception cref="ValidateException">验证失败的异常</exception>
        public JWTValidator ValidateDate(DateTime dateToCheck)
        {
            ValidateDate(_jwt.Payload, dateToCheck, 0L);
            return this;
        }

        /// <summary>
        /// 检查JWT的以下三两个时间：
        /// 
        /// <ul>
        ///     <li>{@link JWTPayload#NOT_BEFORE}：生效时间不能晚于当前时间</li>
        ///     <li>{@link JWTPayload#EXPIRES_AT}：失效时间不能早于当前时间</li>
        ///     <li>{@link JWTPayload#ISSUED_AT}： 签发时间不能晚于当前时间</li>
        /// </ul>
        /// <p>
        /// 如果某个时间没有设置，则不检查（表示无限制）
        /// </p>
        /// </summary>
        /// <param name="dateToCheck">被检查的时间，一般为当前时间</param>
        /// <param name="leeway">容忍空间，单位：秒。当不能晚于当前时间时，向后容忍；不能早于向前容忍。</param>
        /// <returns>this</returns>
        /// <exception cref="ValidateException">验证失败的异常</exception>
        public JWTValidator ValidateDate(DateTime dateToCheck, long leeway)
        {
            ValidateDate(_jwt.Payload, dateToCheck, leeway);
            return this;
        }

        /// <summary>
        /// 验证算法
        /// </summary>
        /// <param name="jwt">{@link JWT}对象</param>
        /// <param name="signer">用于验证的签名器</param>
        /// <exception cref="ValidateException">验证异常</exception>
        private static void ValidateAlgorithm(JWT jwt, IJwtSigner signer)
        {
            var algorithmId = jwt.GetAlgorithm();
            if (signer == null)
            {
                signer = jwt.GetSigner();
            }

            if (string.IsNullOrEmpty(algorithmId))
            {
                // 可能无签名
                if (signer == null || signer is NoneJwtSigner)
                {
                    return;
                }
                throw new ValidateException("No algorithm defined in header!");
            }

            if (signer == null)
            {
                throw new ArgumentException("No Signer for validate algorithm!");
            }

            var algorithmIdInSigner = signer.GetAlgorithmId();
            if (!string.Equals(algorithmId, algorithmIdInSigner))
            {
                throw new ValidateException($"Algorithm [{algorithmId}] defined in header doesn't match to [{algorithmIdInSigner}]!");
            }

            // 通过算法验证签名是否正确
            if (!jwt.Verify(signer))
            {
                throw new ValidateException("Signature verification failed!");
            }
        }

        /// <summary>
        /// 检查JWT的以下三两个时间：
        /// 
        /// <ul>
        ///     <li>{@link JWTPayload#NOT_BEFORE}：生效时间不能晚于当前时间</li>
        ///     <li>{@link JWTPayload#EXPIRES_AT}：失效时间不能早于当前时间</li>
        ///     <li>{@link JWTPayload#ISSUED_AT}： 签发时间不能晚于当前时间</li>
        /// </ul>
        /// <p>
        /// 如果某个时间没有设置，则不检查（表示无限制）
        /// </p>
        /// </summary>
        /// <param name="payload">{@link JWTPayload}</param>
        /// <param name="now">当前时间</param>
        /// <param name="leeway">容忍空间，单位：秒。当不能晚于当前时间时，向后容忍；不能早于向前容忍。</param>
        /// <exception cref="ValidateException">验证异常</exception>
        private static void ValidateDate(JWTPayload payload, DateTime now, long leeway)
        {
            // 检查生效时间（生效时间不能晚于当前时间）
            var notBefore = GetDateTimeFromClaims(payload.GetClaimsJson(), JWTPayload.NOT_BEFORE);
            ValidateNotAfter(JWTPayload.NOT_BEFORE, notBefore, now, leeway);

            // 检查失效时间（失效时间不能早于当前时间）
            var expiresAt = GetDateTimeFromClaims(payload.GetClaimsJson(), JWTPayload.EXPIRES_AT);
            ValidateNotBefore(JWTPayload.EXPIRES_AT, expiresAt, now, leeway);

            // 检查签发时间（签发时间不能晚于当前时间）
            var issueAt = GetDateTimeFromClaims(payload.GetClaimsJson(), JWTPayload.ISSUED_AT);
            ValidateNotAfter(JWTPayload.ISSUED_AT, issueAt, now, leeway);
        }

        /// <summary>
        /// 从Claims中获取DateTime值
        /// </summary>
        /// <param name="claims">Claims字典</param>
        /// <param name="key">键</param>
        /// <returns>DateTime值，不存在则返回null</returns>
        private static DateTime? GetDateTimeFromClaims(Dictionary<string, object> claims, string key)
        {
            if (claims.TryGetValue(key, out var value))
            {
                if (value is long timestamp)
                {
                    return DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
                }
                else if (value is string dateStr)
                {
                    if (DateTime.TryParse(dateStr, out var date))
                    {
                        return date;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 验证指定字段的时间不能晚于当前时间<br>
        /// 被检查的日期不存在则跳过
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="dateToCheck">被检查的字段日期</param>
        /// <param name="now">当前时间</param>
        /// <param name="leeway">容忍空间，单位：秒。向后容忍</param>
        /// <exception cref="ValidateException">验证异常</exception>
        private static void ValidateNotAfter(string fieldName, DateTime? dateToCheck, DateTime now, long leeway)
        {
            if (!dateToCheck.HasValue)
            {
                return;
            }
            if (leeway > 0)
            {
                now = now.AddSeconds(leeway);
            }
            if (dateToCheck.Value > now)
            {
                throw new ValidateException($"'{fieldName}':[{dateToCheck.Value}] is after now:[{now}]");
            }
        }

        /// <summary>
        /// 验证指定字段的时间不能早于当前时间<br>
        /// 被检查的日期不存在则跳过
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="dateToCheck">被检查的字段日期</param>
        /// <param name="now">当前时间</param>
        /// <param name="leeway">容忍空间，单位：秒。。向前容忍</param>
        /// <exception cref="ValidateException">验证异常</exception>
        private static void ValidateNotBefore(string fieldName, DateTime? dateToCheck, DateTime now, long leeway)
        {
            if (!dateToCheck.HasValue)
            {
                return;
            }
            if (leeway > 0)
            {
                now = now.AddSeconds(-leeway);
            }
            if (dateToCheck.Value < now)
            {
                throw new ValidateException($"'{fieldName}':[{dateToCheck.Value}] is before now:[{now}]");
            }
        }
    }
}