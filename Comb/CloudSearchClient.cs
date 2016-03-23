using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            _searchClient.BaseAddress = new Uri($"http://search-{settings.Endpoint}/{Constants.ApiVersion}/");

            _documentClient = settings.HttpClientFactory.MakeInstance();
            _documentClient.BaseAddress = new Uri($"http://doc-{settings.Endpoint}/{Constants.ApiVersion}/");

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
            var parameters = new Dictionary<string, string>();

            if (request.Query != null)
            {
                parameters["q"]        = request.Query.Definition;
                parameters["q.parser"] = request.Query.Parser;
            }

            if (request.Filter != null)
                parameters["fq"] = request.Filter.Definition;

            if (request.Options != null)
                parameters["q.options"] = JsonConvert.SerializeObject(request.Options, JsonSettings.Default);

            if (request.Start.HasValue)
                parameters["start"] = request.Start.ToString();

            if (request.Size.HasValue)
                parameters["size"] = request.Size.ToString();

            if (request.Sort.Any())
                parameters["sort"] = string.Join(",", request.Sort);

            if (request.Return.Any())
                parameters["return"] = string.Join(",", request.Return);

            var facets = request.Facets.ToDictionary(f => $"facet.{f.Field.Name}", f => f.Definition);

            foreach (var f in facets)
                parameters[f.Key] = f.Value;

            var expressions = request.Expressions.ToDictionary(e => $"expr.{e.Name}", e => e.Definition);

            foreach (var e in expressions)
                parameters[e.Key] = e.Value;

            return RunSearch<T>(_searchClient, "search", parameters);
        }

        async Task<SearchResponse<T>> RunSearch<T>(HttpClient httpClient, string url, IDictionary<string, string> parameters)
        {
            var info = new SearchInfo(url, parameters);

            var request = _settings.SearchMethod == SearchHttpMethod.Post ?
                httpClient.PostAsync(url, new FormUrlEncodedContent(parameters)) :
                httpClient.GetAsync(MakeGetUrl(url, parameters));

            using (var httpResponse = await request.ConfigureAwait(false))
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

        static string MakeGetUrl(string url, IDictionary<string, string> parameters)
        {
            if (parameters == null || !parameters.Any())
                return url;

            var sb = new StringBuilder(url);

            sb.Append("?");

            var isSubsequent = false;

            foreach (var parameter in parameters)
            {
                if (isSubsequent)
                    sb.Append("&");
                else
                    isSubsequent = true;

                sb.Append(parameter.Key);
                sb.Append("=");
                sb.Append(Uri.EscapeDataString(parameter.Value));
            }

            return sb.ToString();
        }
    }
}
