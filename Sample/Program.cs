using System;
using System.Collections.Generic;
using Comb.StructuredQueries;
using Newtonsoft.Json;

namespace Comb.Sample
{
    class Program
    {
        const string SearchEndpoint = "comb-kcm6nswvggn4fv627t5zahkwba.ap-southeast-2.cloudsearch.amazonaws.com";

        public static readonly Field Silly = new Field("silly");
        public static readonly Field Test = new Field("test");

        static void Main()
        {
            // TODO: Restriction of returned fields - probably based on result class requested.
            // TODO: Custom sorting expressions.

            var query = new SearchRequest
            {
                //Query = new SimpleQuery("boop |beep -bing"),
                Query = new StructuredQuery(new AndCondition(
                    new FieldCondition("test", "boop"),
                    new StringValue("beep")
                    //new AndCondition(new[]
                    //{
                    //    new StringCondition("Beep"), 
                    //    new StringCondition("Bing")
                    //})
                )),
                //Query = new StructuredQuery(new OrCondition(new[] { new StringCondition("literal", "one") }, 123)),
                Start = 0,
                Size = 20,
                Sort = new List<Sort>
                {
                    new Sort(Silly, SortDirection.Descending)
                },
                Return = new List<Return>
                {
                    Return.AllFields,
                    Return.Score,
                }
            };

            var client = new CloudSearchClient(new CloudSearchSettings
            {
                Endpoint = SearchEndpoint
            });

            try
            {
                var results = client.SearchAsync<Result>(query).Result;

                Console.WriteLine("URL:      " + results.Request.Url);
                Console.WriteLine("Resource: " + results.Status.ResourceId);
                Console.WriteLine("Time:     " + results.Status.TimeMs);
                Console.WriteLine("Found:    " + results.Hits.Found);
                Console.WriteLine("Start:    " + results.Hits.Start);
                Console.WriteLine("Returned: " + results.Hits.Hit.Length);
                Console.WriteLine();

                foreach (var hit in results.Hits.Hit)
                {
                    Console.WriteLine(hit.Id);
                    Console.WriteLine(hit.Fields.Test);
                    Console.WriteLine(hit.Fields.Literal);
                    Console.WriteLine(hit.Fields.Score);
                    Console.WriteLine();
                }
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    Console.WriteLine(inner.GetType().Name);

                    var cloudSearchException = inner as SearchException;

                    if (cloudSearchException != null)
                    {
                        Console.WriteLine("Status: ({0}) {1}", (int) cloudSearchException.HttpStatusCode, cloudSearchException.HttpStatusCode);
                        Console.WriteLine("Error:  " + inner.Message);
                    }
                    else
                    {
                        Console.WriteLine("Error: " + inner.Message);
                    }
                }
            }
        }
    }

    public class Result
    {
        public string Test { get; set; }
        
        public string Literal { get; set; }

        [JsonProperty(Constants.Fields.Score)]
        public float Score { get; set; }
    }
}
