using System.Net;
using System.Threading;

namespace WellTool.Http.Server
{
    public class SimpleServer : HttpServerBase
    {
        private Action<HttpServerRequest, HttpServerResponse> _action;
        
        public SimpleServer(int port, Action<HttpServerRequest, HttpServerResponse> action) : base(port)
        {
            _action = action;
        }
        
        protected override void Run()
        {
            while (_running)
            {
                try
                {
                    var context = _listener.GetContext();
                    HandleRequest(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        
        protected override void HandleRequest(HttpListenerContext context)
        {
            var request = new HttpServerRequest(context.Request);
            var response = new HttpServerResponse(context.Response);
            
            try
            {
                _action(request, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.ContentType = "text/plain";
                response.Write(ex.Message);
            }
            finally
            {
                response.Close();
            }
        }
    }
}