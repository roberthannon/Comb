using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Comb.Queries;
using Comb.Responses;
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

        public async Task<SearchResponse<T>> SearchAsync<T>(SearchQuery query)
            where T : ISearchResponse
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Format("http://{0}/{1}/", Endpoint, ApiVersion));

                var queryString = HttpUtility.ParseQueryString(String.Empty);

                queryString["q"] = "boop";

                if (query.Start.HasValue)
                    queryString["start"] = query.Start.ToString();

                if (query.Size.HasValue)
                    queryString["size"] = query.Size.ToString();

                if (query.Sort.Any())
                    queryString["sort"] = string.Join(",", query.Sort.Select(x => x.ToString()).ToArray());

                Console.WriteLine(queryString.ToString());

                using (var response = await client.GetAsync("search?" + queryString))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new NotImplementedException();

                    using (var content = await response.Content.ReadAsStreamAsync())
                    using (var streamReader = new StreamReader(content, Encoding.UTF8))
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = JsonSerializer.Create(new JsonSerializerSettings());
                        var result = serializer.Deserialize<SearchResponse<T>>(jsonReader);

                        foreach (var hit in result.Hits.Hit)
                            hit.Fields.Id = hit.Id;

                        return result;
                    }
                }
            }
        }
    }
}

