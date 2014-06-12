using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comb.Tests.Support
{
    class TestHttpContent : HttpContent
    {
        readonly string _content;

        public TestHttpContent(string content)
        {
            _content = content;
        }

        protected async override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            var bytes = Encoding.UTF8.GetBytes(_content);

            await stream.WriteAsync(bytes, 0, bytes.Length);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = Encoding.UTF8.GetByteCount(_content);
            return true;
        }
    }
}