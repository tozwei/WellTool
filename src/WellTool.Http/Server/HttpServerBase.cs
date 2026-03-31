using System.Net;
using System.Threading;

namespace WellTool.Http.Server
{
    public abstract class HttpServerBase
    {
        protected HttpListener _listener;
        protected Thread _thread;
        protected bool _running;
        
        public HttpServerBase(int port)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:{port}/");
        }
        
        public void Start()
        {
            _running = true;
            _listener.Start();
            _thread = new Thread(Run);
            _thread.Start();
        }
        
        public void Stop()
        {
            _running = false;
            _listener.Stop();
            _thread.Join();
        }
        
        protected abstract void Run();
        
        protected abstract void HandleRequest(HttpListenerContext context);
    }
}