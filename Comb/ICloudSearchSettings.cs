namespace Comb
{
    public interface ICloudSearchSettings
    {
        string Endpoint { get; }
        IHttpClientFactory HttpClientFactory { get; }
    }
}