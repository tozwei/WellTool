namespace WellTool.Http.Http
{
    /// <summary>
    /// HTTP状态码
    /// </summary>
    public static class Status
    {
        /// <summary>OK</summary>
        public const int OK = 200;

        /// <summary>Created</summary>
        public const int Created = 201;

        /// <summary>NoContent</summary>
        public const int NoContent = 204;

        /// <summary>MovedPermanently</summary>
        public const int MovedPermanently = 301;

        /// <summary>Found</summary>
        public const int Found = 302;

        /// <summary>NotModified</summary>
        public const int NotModified = 304;

        /// <summary>BadRequest</summary>
        public const int BadRequest = 400;

        /// <summary>Unauthorized</summary>
        public const int Unauthorized = 401;

        /// <summary>Forbidden</summary>
        public const int Forbidden = 403;

        /// <summary>NotFound</summary>
        public const int NotFound = 404;

        /// <summary>InternalServerError</summary>
        public const int InternalServerError = 500;

        /// <summary>BadGateway</summary>
        public const int BadGateway = 502;

        /// <summary>ServiceUnavailable</summary>
        public const int ServiceUnavailable = 503;
    }
}