using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Comb.Searching;
using Comb.Searching.Expressions;
using Comb.Searching.Queries;
using Comb.Searching.Queries.Structured;
using Comb.Searching.Responses;

namespace Comb.Sample
{
    class Program
    {
        const string SearchEndpoint = "search-comb-kcm6nswvggn4fv627t5zahkwba.ap-southeast-2.cloudsearch.amazonaws.com";

        public static readonly DomainExpression Silly = new DomainExpression("silly");
        public static readonly DomainExpression Test = new DomainExpression("test");

        static void Main()
        {
            // TODO: Restriction of returned fields - probably based on result class requested.
            // TODO: Custom sorting expressions.

            var query = new SearchRequest
            {
//              Query = new SimpleQuery("boop |beep -bing"),
                Query = new StructuredQuery(new AndCondition(new Condition[]
                {
                    new StringCondition("test", "boop"),
                    new AndCondition(new[]
                    {
                        new StringCondition("Beep"), 
                        new StringCondition("Bing")
                    })
                })),
                Start = 0,
                Size = 20,
                Sort = new List<Sort>
                {
                    new Sort(Silly, SortDirection.Descending)
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
