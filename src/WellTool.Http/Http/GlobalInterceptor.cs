using System.Collections.Generic;

namespace WellTool.Http.Http
{
    /// <summary>
    /// 全局拦截器
    /// </summary>
    public static class GlobalInterceptor
    {
        private static readonly List<IHttpInterceptor> _interceptors = new List<IHttpInterceptor>();

        /// <summary>
        /// 添加拦截器
        /// </summary>
        /// <param name="interceptor">拦截器</param>
        public static void Add(IHttpInterceptor interceptor)
        {
            _interceptors.Add(interceptor);
        }

        /// <summary>
        /// 移除拦截器
        /// </summary>
        /// <param name="interceptor">拦截器</param>
        public static void Remove(IHttpInterceptor interceptor)
        {
            _interceptors.Remove(interceptor);
        }

        /// <summary>
        /// 获取所有拦截器
        /// </summary>
        /// <returns>拦截器列表</returns>
        public static List<IHttpInterceptor> GetAll()
        {
            return _interceptors;
        }

        /// <summary>
        /// 清空所有拦截器
        /// </summary>
        public static void Clear()
        {
            _interceptors.Clear();
        }
    }
}