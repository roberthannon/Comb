using System;

namespace Comb.Sample
{
    class Program
    {
        const string SearchEndpoint = "search-dev6-n4lportalsearch-ndrqsgbvshx4jd7zr2uhk3wmtm.ap-southeast-2.cloudsearch.amazonaws.com";

        static void Main()
        {
            var client = new SearchClient(SearchEndpoint);
            var results = client.SearchAsync<Result>().Result;

            Console.WriteLine(results.Status.ResourceId);
            Console.WriteLine(results.Status.TimeMs);
            Console.WriteLine(results.Hits.Found);
            Console.WriteLine(results.Hits.Start);

            foreach (var hit in results.Hits.Hit)
            {
                Console.WriteLine(hit.Id);
                Console.WriteLine(hit.Fields.Test);
            }
        }
    }

    public class Result
    {
        public string Test { get; set; }
    }
}
