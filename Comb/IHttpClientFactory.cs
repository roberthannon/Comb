using System.Net.Http;

namespace Comb
{
    public interface IHttpClientFactory
    {
        HttpClient MakeInstance();
    }
}
