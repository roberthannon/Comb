using System;

namespace Comb
{
    public class CloudSearchSettings : ICloudSearchSettings
    {
        public CloudSearchSettings(string endpoint, IHttpClientFactory httpClientFactory = null)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

            Endpoint = endpoint;
            HttpClientFactory = httpClientFactory ?? new DefaultHttpClientFactory();
        }

        public string Endpoint { get; }

        public IHttpClientFactory HttpClientFactory { get; }
    }
}