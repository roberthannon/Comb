using System;
using System.Collections.Generic;
using Comb.StructuredQueries;

namespace Comb.Sample
{
    class Program
    {
        const string SearchEndpoint = "comb-kcm6nswvggn4fv627t5zahkwba.ap-southeast-2.cloudsearch.amazonaws.com";

        public static readonly DomainExpression Silly = new DomainExpression("silly");
        public static readonly DomainExpression Test = new DomainExpression("test");

        static void Main()
        {
            // TODO: Restriction of returned fields - probably based on result class requested.
            // TODO: Custom sorting expressions.

            var query = new SearchRequest
            {
//              Query = new SimpleQuery("boop |beep -bing"),
/*              Query = new StructuredQuery(new AndCondition(new Condition[]
                {
                    new StringCondition("test", "boop"),
                    new AndCondition(new[]
                    {
                        new StringCondition("Beep"), 
                        new StringCondition("Bing")
                    })
                })),
*/
                Query = new StructuredQuery(new NotCondition(new StringCondition("literal", "*"))),
                Start = 0,
                Size = 20,
                Sort = new List<Sort>
                {
                    new Sort(Silly, SortDirection.Descending)
                }
            };

            var client = new CloudSearchClient(new CloudSearchSettings
            {
                Endpoint = SearchEndpoint
            });

            var results = client.SearchAsync<Result>(query).Result;

            foreach (var hit in results.Hits.Hit)
            {
                Console.WriteLine(hit.Id);
                Console.WriteLine(hit.Fields.Test);
                Console.WriteLine(hit.Fields.Literal);
                Console.WriteLine();
            }
        }
    }

    public class Result
    {
        public string Test { get; set; }
        public string Literal { get; set; }
    }
}
