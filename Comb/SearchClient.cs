using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Comb
{
    public class SearchClient : ISearchClient
    {
        public const string ApiVersion = "2013-01-01";

        public string Endpoint { get; set; }

        public SearchClient(string endpoint)
        {
            Endpoint = endpoint;
        }

        public async Task<SearchResult<T>> SearchAsync<T>()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Format("http://{0}/{1}/", Endpoint, ApiVersion));

                using (var response = await client.GetAsync("search?q=boop"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new NotImplementedException();

                    using (var content = await response.Content.ReadAsStreamAsync())
                    using (var streamReader = new StreamReader(content, Encoding.UTF8))
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = JsonSerializer.Create(new JsonSerializerSettings());
                        var result = serializer.Deserialize<SearchResult<T>>(jsonReader);

                        return result;
                    }
                }
            }
        }
    }

    public class SearchResult<T>
    {
        public Status Status { get; set; }

        /// <summary>
        /// Contains the number of matching documents, the index of the first document included in
        /// the response, and an array that lists the document IDs and data for each hit.
        /// </summary>
        public Hits<T> Hits { get; set; }
    }

    public class Status
    {
        /// <summary>
        /// The encrypted Resource ID.
        /// </summary>
        [JsonProperty("rid")]
        public string ResourceId { get; set; }

        /// <summary>
        /// How long it took to process the search request in milliseconds.
        /// </summary>
        [JsonProperty("time-ms")]
        public int TimeMs { get; set; }
    }


    public class Hits<T>
    {
        /// <summary>
        /// The total number of hits that match the search request after Amazon CloudSearch finished
        /// processing the request.
        /// </summary>
        public int Found { get; set; }

        /// <summary>
        /// The index of the first hit returned in this response.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// An array containing a result for each document hit.
        /// </summary>
        public Hit<T>[] Hit { get; set; }
    }

    public class Hit<T>
    {
        /// <summary>
        /// The unique identifier for the document found.
        /// </summary>
        public string Id { get; set; }

        public T Fields { get; set; }
    }
}

