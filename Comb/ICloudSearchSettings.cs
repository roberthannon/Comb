using Newtonsoft.Json;

namespace Comb
{
    public interface ICloudSearchSettings
    {
        string Endpoint { get; }
        IHttpClientFactory HttpClientFactory { get; }
        JsonSerializerSettings DocumentSerializerSettings { get; }
    }
}