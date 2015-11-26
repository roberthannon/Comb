using Newtonsoft.Json;
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
        readonly ICloudSearchSettings _settings;
        readonly HttpClient _searchClient;
        readonly HttpClient _documentClient;
        readonly JsonSerializer _responseDeserializer;

        public CloudSearchClient(ICloudSearchSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");

            _settings = settings;

            _searchClient = settings.HttpClientFactory.MakeInstance();
            _searchClient.BaseAddress = new Uri(string.Format("http://search-{0}/{1}/", settings.Endpoint, Constants.ApiVersion));

            _documentClient = settings.HttpClientFactory.MakeInstance();
            _documentClient.BaseAddress = new Uri(string.Format("http://doc-{0}/{1}/", settings.Endpoint, Constants.ApiVersion));

            _responseDeserializer = JsonSerializer.Create(JsonSettings.Default);
        }

        public Task<UpdateResponse> UpdateAsync(DocumentRequest request)
        {
            return UpdateAsync(new[] { request });
        }

        public Task<UpdateResponse> UpdateAsync(IEnumerable<DocumentRequest> requests)
        {
            return PostDocuments(_documentClient, "documents/batch", requests);
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

            if (request.Options != null)
            {
                queryString["q.options"] = info.Options = JsonConvert.SerializeObject(request.Options, JsonSettings.Default);
            }

            if (request.Start.HasValue)
                queryString["start"] = info.Start = request.Start.ToString();

            if (request.Size.HasValue)
                queryString["size"] = info.Size = request.Size.ToString();

            if (request.Sort.Any())
                queryString["sort"] = info.Sort = string.Join(",", request.Sort);

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

                    var response = _responseDeserializer.Deserialize<SearchResponse<T>>(jsonReader);
                    response.Request = info;
                    return response;
                }
            }
        }

        async Task<UpdateResponse> PostDocuments(HttpClient httpClient, string url, object body)
        {
            var serializedBody = JsonConvert.SerializeObject(body, JsonSettings.Default/*_settings.DocumentSerializerSettings*/); // TODO Custom serialiser settings?

            using (var response = await httpClient.PostAsync(url, new StringContent(serializedBody, Encoding.UTF8, "application/json")))
            {
                using (var content = await response.Content.ReadAsStreamAsync())
                using (var streamReader = new StreamReader(content, Encoding.UTF8))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    if (!response.IsSuccessStatusCode)
                        throw HandleUpdateError(response, jsonReader);

                    return _responseDeserializer.Deserialize<UpdateResponse>(jsonReader);
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

            var response = _responseDeserializer.Deserialize<SearchErrorResponse>(jsonReader);

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
                var responseObject = _responseDeserializer.Deserialize<UpdateResponse>(jsonReader);

                if (responseObject != null && responseObject.Message != null)
                    message = responseObject.Message;

                return new UpdateException(responseObject, httpResponse.StatusCode, message);
            }

            return new UpdateException(null, httpResponse.StatusCode, message);
        }
    }
}
