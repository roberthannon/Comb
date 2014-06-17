using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Comb
{
    public class CloudSearchClient : ICloudSearchClient
    {
        readonly HttpClient _searchClient;
        readonly HttpClient _documentClient;
        readonly JsonSerializer _jsonSerializer;

        public CloudSearchClient()
            : this(CloudSearchSettings.Default)
        {
        }

        public CloudSearchClient(CloudSearchSettings settings)
        {
            _searchClient = settings.HttpClientFactory.MakeInstance();
            _searchClient.BaseAddress = new Uri(string.Format("http://search-{0}/{1}/", settings.Endpoint, Constants.ApiVersion));

            _documentClient = settings.HttpClientFactory.MakeInstance();
            _documentClient.BaseAddress = new Uri(string.Format("http://doc-{0}/{1}/", settings.Endpoint, Constants.ApiVersion));

            _jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings());
        }

        public Task UpdateAsync(DocumentRequest request)
        {
            return Post<DocumentResponse>(_documentClient, "documents/batch", new List<DocumentRequest>
            {
                request
            });
        }

        public Task UpdateAsync(IEnumerable<DocumentRequest> requests)
        {
            return Post<DocumentResponse>(_documentClient, "documents/batch", requests);
        }

        public Task<SearchResponse<T>> SearchAsync<T>(SearchRequest request)
        {
            var queryString = HttpUtility.ParseQueryString(String.Empty);

            if (request.Query != null)
            {
                queryString["q"] = request.Query.Definition;
                queryString["q.parser"] = request.Query.Parser;
            }

            if (request.Start.HasValue)
                queryString["start"] = request.Start.ToString();

            if (request.Size.HasValue)
                queryString["size"] = request.Size.ToString();

            if (request.Sort.Any())
                queryString["sort"] = string.Join(",", request.Sort.Select(x => x.ToString()).ToArray());

            return RunSearch<T>(_searchClient, "search", queryString);
        }

        async Task<SearchResponse<T>> RunSearch<T>(HttpClient httpClient, string url, NameValueCollection queryString)
        {
            if (queryString != null)
                url = string.Format("{0}?{1}", url, queryString);

            try
            {
                using (var getResponse = await httpClient.GetAsync(url).ConfigureAwait(false))
                {
                    if (!getResponse.IsSuccessStatusCode)
                        throw new NotImplementedException();

                    using (var content = await getResponse.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new StreamReader(content, Encoding.UTF8))
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var response = _jsonSerializer.Deserialize<SearchResponse<T>>(jsonReader);
                        response.Status.Url = url;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        async Task<T> Post<T>(HttpClient httpClient, string url, object body)
        {
            HttpContent input;

            using (var sw = new StringWriter())
            {
                _jsonSerializer.Serialize(sw, body);
                input = new StringContent(sw.ToString(), Encoding.UTF8, "application/json");
            }

            using (var response = await httpClient.PostAsync(url, input))
            {
                if (!response.IsSuccessStatusCode)
                    throw new NotImplementedException();

                using (var content = await response.Content.ReadAsStreamAsync())
                using (var streamReader = new StreamReader(content, Encoding.UTF8))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    return _jsonSerializer.Deserialize<T>(jsonReader);
                }
            }
        }
    }
}

