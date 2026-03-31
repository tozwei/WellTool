using System.Net.Http;
using System.Text;

namespace WellTool.Http.Webservice
{
    public class SoapClient
    {
        private string _url;
        private string _namespace;
        private HttpClient _client;
        
        public SoapClient(string url, string ns)
        {
            _url = url;
            _namespace = ns;
            _client = new HttpClient();
        }
        
        public string Send(string method, object parameters)
        {
            var soapEnvelope = CreateSoapEnvelope(method, parameters);
            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", $"{_namespace}{method}");
            
            var response = _client.PostAsync(_url, content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        
        private string CreateSoapEnvelope(string method, object parameters)
        {
            var soap = $@"
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ns=""{_namespace}"">
    <soap:Body>
        <ns:{method}>
            {parameters}
        </ns:{method}>
    </soap:Body>
</soap:Envelope>";
            return soap;
        }
    }
}