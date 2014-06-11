using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Comb.Tests.Support
{
    public class HttpMessageHandlerStub : HttpMessageHandler
    {
        readonly HttpResponseMessage _response;

        public HttpMessageHandlerStub(HttpResponseMessage response)
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

    public class HttpContentStub : HttpContent
    {
        protected override Task SerializeToStreamAsync(
            Stream stream,
            TransportContext context)
        {
            throw new System.NotImplementedException();
        }

        protected override bool TryComputeLength(out long length)
        {
            throw new System.NotImplementedException();
        }
    }
}
