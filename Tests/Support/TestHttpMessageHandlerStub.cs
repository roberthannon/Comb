using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Comb.Tests.Support
{
    class TestHttpMessageHandler : HttpMessageHandler
    {
        readonly HttpResponseMessage _response;

        public TestHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }
}
