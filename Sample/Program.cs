using System;
using System.Collections.Generic;
using Comb.Queries;
using Comb.Responses;

namespace Comb.Sample
{
    class Program
    {
        const string SearchEndpoint = "search-comb-kcm6nswvggn4fv627t5zahkwba.ap-southeast-2.cloudsearch.amazonaws.com";

        static void Main()
        {
            var query = new SearchQuery
            {
                Start = 0,
                Size = 20,
                Sort = new List<Sort>
                {
                    new Sort { Name = "_id", Direction = SortDirection.Ascending }
                }
            };

            var client = new SearchClient(SearchEndpoint);
            var results = client.SearchAsync<Result>(query).Result;

            Console.WriteLine(results.Status.ResourceId);
            Console.WriteLine(results.Status.TimeMs);
            Console.WriteLine(results.Hits.Found);
            Console.WriteLine(results.Hits.Start);

            foreach (var hit in results.Hits.Hit)
            {
                Console.WriteLine(hit.Fields.Id);
                Console.WriteLine(hit.Fields.Test);
            }
        }
    }

    public class Result : ISearchResponse
    {
        public string Id { get; set; }

        public string Test { get; set; }
    }
}
