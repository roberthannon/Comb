using System;

namespace Comb
{
    public class CloudSearchSettings : ICloudSearchSettings
    {
        readonly string _endpoint;
        readonly IHttpClientFactory _httpClientFactory;

        public CloudSearchSettings(string endpoint, IHttpClientFactory httpClientFactory = null)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            _endpoint = endpoint;
            _httpClientFactory = httpClientFactory ?? new DefaultHttpClientFactory();
        }

        public string Endpoint
        {
            get { return _endpoint; }
        }

        public IHttpClientFactory HttpClientFactory
        {
            get { return _httpClientFactory; }
        }
    }
}