using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

            _jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public Task<UpdateResponse> UpdateAsync(DocumentRequest request)
        {
            return UpdateAsync(new[] { request });
        }

        public Task<UpdateResponse> UpdateAsync(IEnumerable<DocumentRequest> requests)
        {
            return PostDocuments<UpdateResponse>(_documentClient, "documents/batch", requests);
        }

        public Task<SearchResponse<EmptyResult>> SearchAsync(SearchRequest request)
        {
            return SearchAsync<EmptyResult>(request);
        }

        public Task<SearchResponse<T>> SearchAsync<T>(SearchRequest request)
        {
            var queryString = HttpUtility.ParseQueryString(String.Empty);
            var info = new SearchInfo();

            if (request.Query != null)
            {
                queryString["q"]        = info.Query  = request.Query.Definition;
                queryString["q.parser"] = info.Parser = request.Query.Parser;
            }

            if (request.Filter != null)
            {
                queryString["fq"] = info.Filter = request.Filter.Definition;
            }

            if (request.Start.HasValue)
                queryString["start"] = info.Start = request.Start.ToString();

            if (request.Size.HasValue)
                queryString["size"] = info.Size = request.Size.ToString();

            if (request.Sort.Any())
                queryString["sort"] = info.Sort = string.Join(",", request.Sort.Select(x => x.ToString()).ToArray());

            if (request.Return.Any())
                queryString["return"] = info.Return = string.Join(",", request.Return);

            info.Facets = request.Facets.Select(facet =>
            {
                var queryParamName = string.Format("facet.{0}", facet.Field.Name);
                var queryParamValue = facet.Definition;
                queryString[queryParamName] = queryParamValue;
                return new KeyValuePair<string, string>(queryParamName, queryParamValue);
            }).ToArray();

            info.Expressions = request.Expressions.Select(expression =>
            {
                var queryParamName = string.Format("expr.{0}", expression.Name);
                var queryParamValue = expression.Definition;
                queryString[queryParamName] = queryParamValue;
                return new KeyValuePair<string, string>(queryParamName, queryParamValue);
            }).ToArray();

            return RunSearch<T>(_searchClient, "search", queryString, info);
        }

        async Task<SearchResponse<T>> RunSearch<T>(HttpClient httpClient, string url, NameValueCollection queryString, SearchInfo info)
        {
            if (queryString != null)
                url = info.Url = string.Format("{0}?{1}", url, queryString);

            using (var httpResponse = await httpClient.GetAsync(url).ConfigureAwait(false))
            {
                using (var content = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var streamReader = new StreamReader(content, Encoding.UTF8))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    if (!httpResponse.IsSuccessStatusCode)
                        throw HandleSearchError(httpResponse, jsonReader, info);

                    var response = _jsonSerializer.Deserialize<SearchResponse<T>>(jsonReader);
                    response.Request = info;
                    return response;
                }
            }
        }

        async Task<T> PostDocuments<T>(HttpClient httpClient, string url, object body)
        {
            HttpContent input;

            using (var sw = new StringWriter())
            {
                _jsonSerializer.Serialize(sw, body);
                input = new StringContent(sw.ToString(), Encoding.UTF8, "application/json");
            }

            using (var response = await httpClient.PostAsync(url, input))
            {
                using (var content = await response.Content.ReadAsStreamAsync())
                using (var streamReader = new StreamReader(content, Encoding.UTF8))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    if (!response.IsSuccessStatusCode)
                        throw HandleUpdateError(response, jsonReader);

                    return _jsonSerializer.Deserialize<T>(jsonReader);
                }
            }
        }

        Exception HandleSearchError(HttpResponseMessage httpResponse, JsonTextReader jsonReader, SearchInfo info)
        {
            // TODO: 500 level errors should be marked Retry
            // TODO: 400 level errors should be marked as bad input.
            // TODO: Backoffs etc
            // http://docs.aws.amazon.com/cloudsearch/latest/developerguide/error-handling.html
            // http://docs.aws.amazon.com/general/latest/gr/api-retries.html

            var response = _jsonSerializer.Deserialize<SearchErrorResponse>(jsonReader);

            return new SearchException(info, httpResponse.StatusCode, response.Message);
        }

        Exception HandleUpdateError(HttpResponseMessage httpResponse, JsonTextReader jsonReader)
        {
            // TODO: 500 level errors should be marked Retry
            // TODO: 400 level errors should be marked as bad input.
            // TODO: Backoffs etc
            // http://docs.aws.amazon.com/cloudsearch/latest/developerguide/error-handling.html
            // http://docs.aws.amazon.com/general/latest/gr/api-retries.html

            var message = httpResponse.ReasonPhrase;

            // If we have a 400 error, get error message from returned json object
            if (400 <= (int)httpResponse.StatusCode && (int)httpResponse.StatusCode < 500)
            {
                var responseObject = _jsonSerializer.Deserialize<UpdateResponse>(jsonReader);
                if (responseObject != null && responseObject.Message != null)
                    message = responseObject.Message;
            }

            return new UpdateException(httpResponse.StatusCode, message);
        }
    }
}
