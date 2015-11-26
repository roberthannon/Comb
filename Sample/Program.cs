using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Comb.Sample
{
    class Program
    {
        const string SampleEndpoint = "comb-kcm6nswvggn4fv627t5zahkwba.ap-southeast-2.cloudsearch.amazonaws.com";
            //"DEV.cloudsearch.amazonaws.com";

        static void Main()
        {
            Search();
            Update();
        }

        static void Search()
        {
            // TODO: Restriction of returned fields - probably based on result class requested.
            // TODO: Custom sorting expressions.

            var query = new SearchRequest
            {
                //Query = new SimpleQuery("boop |beep -bing"),
                Query = new StructuredQuery(new AndCondition(new[] { new FieldCondition("Boop Beep", "test"), new FieldCondition("beep") })),
                //Query = new StructuredQuery(new FieldCondition("profile", "doctype")),
                Start = 0,
                Size = 20,
                Sort = new[]
                {
                    new Sort("literal", SortDirection.Descending)
                    //new Sort(SortFields.Score, SortDirection.Descending)
                },
                Return = new[]
                {
                    Return.AllFields,
                    Return.Score
                },
                Facets = new Facet[]
                {
                    //new BucketFacet("location", new[] {
                    //    new Bucket(new Range(new LatLon(-36.81670599, 174.58786011), new LatLon(-36.96854668,174.96757507)))
                    //}),
                    //new BucketFacet("followercount", new[]
                    //{
                    //    new Bucket(new Range(0, 10, maxInclusive: true)),
                    //    new Bucket(new Range(10, 100, maxInclusive: true)),
                    //    new Bucket(new Range(100, 500, maxInclusive: true)),
                    //    new Bucket(new Range(500))
                    //})
                },
                Options = new SearchOptions { DefaultOperator = DefaultOperator.And }
            };

            var client = new CloudSearchClient(new CloudSearchSettings(SampleEndpoint));

            try
            {
                var results = client.SearchAsync<SearchResult>(query).Result;
                //var results = client.SearchAsync<Dictionary<string, string>>(query).Result;

                Console.WriteLine("URL:      " + results.Request.Url);
                Console.WriteLine("Resource: " + results.Status.ResourceId);
                Console.WriteLine("Time:     " + results.Status.TimeMs);
                Console.WriteLine("Found:    " + results.Hits.Found);
                Console.WriteLine("Start:    " + results.Hits.Start);
                Console.WriteLine("Returned: " + results.Hits.Hit.Length);
                Console.WriteLine();

                Console.WriteLine("HITS");
                Console.WriteLine();

                foreach (var hit in results.Hits.Hit)
                {
                    Console.WriteLine(hit.Id);

                    Console.WriteLine("  score: {0}", hit.Fields.Score);
                    Console.WriteLine("  test: {0}", hit.Fields.Test);
                    Console.WriteLine("  literal: {0}", hit.Fields.Literal);

                    //foreach (var field in hit.Fields)
                    //    Console.WriteLine("  {0}: {1}", field.Key, field.Value);
                }
                Console.WriteLine();

                if (results.Facets != null)
                {
                    Console.WriteLine("FACETS");
                    Console.WriteLine();

                    foreach (var facet in results.Facets)
                    {
                        Console.WriteLine(facet.Key);
                        var facetResult = facet.Value;

                        foreach (var bucket in facetResult.Buckets)
                        {
                            Console.WriteLine("  bucket: {0} \t count: {1}", bucket.Value, bucket.Count);
                        }
                    }
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
                        Console.WriteLine("Status: ({0}) {1}", (int)cloudSearchException.HttpStatusCode, cloudSearchException.HttpStatusCode);
                        Console.WriteLine("Error:  " + inner.Message);
                    }
                    else
                    {
                        Console.WriteLine("Error: " + inner.Message);
                    }
                }
            }
        }

        static void Update()
        {
            var client = new CloudSearchClient(new CloudSearchSettings(SampleEndpoint));

            try
            {
                var documentRequests = new[]
                {
                    new Add("54321", new IndexDoc{ Test = "bacon and cheese", Literal = "blue", LiteralArray = new[] { "cheese", "colours" }}),
                    new Add("12345", new IndexDoc{ Test = "things and apples", Literal = "whyohwhy" }),
                    new Add("12333", new IndexDoc{ Test = "the thing that should not be", Literal = "yellow" })
                };

                var results = client.UpdateAsync(documentRequests).Result;

                Console.WriteLine("Status:      " + results.Status);
                Console.WriteLine("Adds:        " + results.Adds);
                Console.WriteLine("Deletes:     " + results.Deletes);
                Console.WriteLine("Message:     " + results.Message);
                Console.WriteLine();
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    Console.WriteLine(inner.GetType().Name);

                    var cloudSearchException = inner as UpdateException;

                    if (cloudSearchException != null)
                    {
                        Console.WriteLine("Status: ({0}) {1}", (int)cloudSearchException.HttpStatusCode, cloudSearchException.HttpStatusCode);
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

    public class SearchResult
    {
        public string Test { get; set; }
        
        public string Literal { get; set; }

        [JsonProperty(Constants.Fields.Score)]
        public float Score { get; set; }
    }

    public class IndexDoc
    {
        public string Literal { get; set; }

        public string Test { get; set; }

        [JsonProperty("literal_array")]
        public IEnumerable<string> LiteralArray { get; set; }

        public IndexDoc()
        {
            LiteralArray = Enumerable.Empty<string>();
        }
    }
}
