using System.Net.Http;

namespace Comb.Tests.Support
{
    class TestHttpClientFactory : IHttpClientFactory
    {
        readonly TestHttpMessageHandler _httpHandler;

        public TestHttpClientFactory(TestHttpMessageHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public HttpClient MakeInstance()
        {
            return new HttpClient(_httpHandler);
        }
    }
}
