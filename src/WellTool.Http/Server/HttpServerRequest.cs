using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.IO;

namespace WellTool.Http.Server
{
    public class HttpServerRequest
    {
        private HttpListenerRequest _request;
        
        public HttpServerRequest(HttpListenerRequest request)
        {
            _request = request;
        }
        
        public string Method => _request.HttpMethod;
        public string Url => _request.Url.ToString();
        public string Path => _request.Url.LocalPath;
        public NameValueCollection Headers => _request.Headers;
        public NameValueCollection QueryString => _request.QueryString;
        
        public string GetBody()
        {
            using (var reader = new StreamReader(_request.InputStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}