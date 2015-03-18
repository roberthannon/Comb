using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Comb
{
    public class CloudSearchSettings : ICloudSearchSettings
    {
        readonly string _endpoint;
        readonly IHttpClientFactory _httpClientFactory;
        readonly JsonSerializerSettings _documentSerializerSettings;

        public CloudSearchSettings(string endpoint, IHttpClientFactory httpClientFactory = null, JsonSerializerSettings documentSerializerSettings = null)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            _endpoint = endpoint;
            _httpClientFactory = httpClientFactory ?? new DefaultHttpClientFactory();
            _documentSerializerSettings = documentSerializerSettings ?? new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        public string Endpoint
        {
            get { return _endpoint; }
        }

        public IHttpClientFactory HttpClientFactory
        {
            get { return _httpClientFactory; }
        }

        public JsonSerializerSettings DocumentSerializerSettings
        {
            get { return _documentSerializerSettings; }
        }
    }
}