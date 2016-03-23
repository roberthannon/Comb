using System;

namespace Comb
{
    public class CloudSearchSettings : ICloudSearchSettings
    {
        public CloudSearchSettings(string endpoint, IHttpClientFactory httpClientFactory = null, SearchHttpMethod searchMethod = SearchHttpMethod.Get)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            Endpoint = endpoint;
            HttpClientFactory = httpClientFactory ?? new DefaultHttpClientFactory();
            SearchMethod = searchMethod;
        }

        public string Endpoint { get; }

        public IHttpClientFactory HttpClientFactory { get; }

        public SearchHttpMethod SearchMethod { get; }
    }
}