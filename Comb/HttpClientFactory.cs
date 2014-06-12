using System.Net.Http;

namespace Comb
{
    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        public virtual HttpClient MakeInstance()
        {
            return new HttpClient();
        }
    }
}