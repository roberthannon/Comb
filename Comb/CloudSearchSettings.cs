using System;

namespace Comb
{
    public class CloudSearchSettings
    {
        internal static readonly IHttpClientFactory DefaultHttpClientFactory = new DefaultHttpClientFactory();
        internal static readonly CloudSearchSettings Default = new CloudSearchSettings();

        IHttpClientFactory _httpClientFactory;

        public string Endpoint { get; set; }

        public IHttpClientFactory HttpClientFactory
        {
            get { return _httpClientFactory ?? DefaultHttpClientFactory; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _httpClientFactory = value;
            }
        }
    }
}