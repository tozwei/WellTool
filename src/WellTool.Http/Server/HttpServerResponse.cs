using System.Net;
using System.Text;

namespace WellTool.Http.Server
{
    public class HttpServerResponse
    {
        private HttpListenerResponse _response;
        
        public HttpServerResponse(HttpListenerResponse response)
        {
            _response = response;
        }
        
        public int StatusCode
        {
            get => _response.StatusCode;
            set => _response.StatusCode = value;
        }
        
        public string ContentType
        {
            get => _response.ContentType;
            set => _response.ContentType = value;
        }
        
        public void AddHeader(string name, string value)
        {
            _response.AddHeader(name, value);
        }
        
        public void Write(string content)
        {
            var buffer = Encoding.UTF8.GetBytes(content);
            _response.ContentLength64 = buffer.Length;
            _response.OutputStream.Write(buffer, 0, buffer.Length);
        }
        
        public void Close()
        {
            _response.OutputStream.Close();
            _response.Close();
        }
    }
}