using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Comb.Tests.Support
{
    class TestHttpMessageHandler : HttpMessageHandler
    {
        public HttpResponseMessage Response { get; set; }

        public TestHttpMessageHandler()
        {
            Response = ResponseSamples.OK();
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(Response);
        }
    }
}
