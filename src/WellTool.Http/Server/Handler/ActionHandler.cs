using WellTool.Http.Server.Action;

namespace WellTool.Http.Server.Handler
{
    /// <summary>
    /// 动作处理器
    /// </summary>
    public class ActionHandler
    {
        private readonly IAction _action;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="action">动作</param>
        public ActionHandler(IAction action)
        {
            _action = action;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        public void Handle(HttpServerRequest request, HttpServerResponse response)
        {
            _action.Handle(request, response);
        }
    }
}