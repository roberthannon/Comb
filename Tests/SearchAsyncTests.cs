using Comb.Tests.Support;
using NUnit.Framework;
using System.Collections.Generic;

namespace Comb.Tests
{
    public class SearchAsyncTests
    {
        TestHttpMessageHandler _httpHandler;
        CloudSearchClient _cloudSearchClient;

        [SetUp]
        public void SetUp()
        {
            _httpHandler = new TestHttpMessageHandler();
            _cloudSearchClient = new CloudSearchClient(new CloudSearchSettings("cloudsearch.example.com", new TestHttpClientFactory(_httpHandler)));
        }

        [Test]
        public async void InfoIncludesDetailsOfSimpleQuery()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop")
            });

            Assert.That(response.Request.Url, Is.EqualTo("search"));
            Assert.That(response.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
            {
                { "q", "boop" },
                { "q.parser", "simple" }
            }));
        }

        [Test]
        public async void InfoIncludesDetailsOfStructuredQuery()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new StructuredQuery(new FieldCondition("two", "one"))
            });

            Assert.That(response.Request.Url, Is.EqualTo("search"));
            Assert.That(response.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
            {
                { "q", "one:'two'" },
                { "q.parser", "structured" }
            }));
        }

        [Test]
        public async void InfoIncludesSizeAndStart()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("abc"),
                Size = 123,
                Start = 456
            });

            Assert.That(response.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
            {
                { "q", "abc" },
                { "q.parser", "simple" },
                { "start", "456" },
                { "size", "123" }
            }));
        }

        [Test]
        public async void InfoIncludesFacets()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop"),
                Facets = new[]
                {
                    new Facet("status"),
                    new SortFacet("genre", FacetSortType.Bucket, 6),
                    new SortFacet("metadata", FacetSortType.Count, 53),
                    new BucketFacet("colour", new[]
                    {
                        new Bucket("red"), new Bucket("green"), new Bucket("blue")
                    }),
                    new BucketFacet("century", new[]
                    {
                        new Bucket(new Range(1600,1700,true,false)), new Bucket(new Range(1700,1800,true,false)), new Bucket(new Range(1800,2000,true,false))
                    }, FacetMethodType.Interval)
                }
            });

            Assert.That(response.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
            {
                { "q", "boop" },
                { "q.parser", "simple" },
                { "facet.status", "{}" },
                { "facet.genre", "{sort:'bucket',size:6}" },
                { "facet.metadata", "{sort:'count',size:53}" },
                { "facet.colour", "{buckets:[\"red\",\"green\",\"blue\"],method:\"filter\"}" },
                { "facet.century", "{buckets:[\"[1600,1700}\",\"[1700,1800}\",\"[1800,2000}\"],method:\"interval\"}" }
            }));
        }

        [Test]
        public async void InfoIncludesSortExpression()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop"),
                Sort = new List<Sort>
                {
                    new Sort("createddate", SortDirection.Ascending),
                    new Sort(new Expression("mysort", "two*three+four"), SortDirection.Descending),
                    new Sort(Fields.Id, SortDirection.Ascending)
                }
            });

            Assert.That(response.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
            {
                { "q", "boop" },
                { "q.parser", "simple" },
                { "sort", "createddate asc,mysort desc,_id asc" },
                { "expr.mysort", "two*three+four" }
            }));
        }

        [Test]
        public async void InfoIncludesReturnFields()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new StructuredQuery(new FieldCondition("yellow")),
                Return = new List<Return> { new Return("this"), new Return("that"), new Return(Fields.Score) }
            });

            Assert.That(response.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
            {
                { "q", "'yellow'" },
                { "q.parser", "structured" },
                { "return", "this,that,_score" }
            }));
        }

        [Test]
        public async void InfoIncludesReturnExpression()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop"),
                Return = new List<Return>
                {
                    new Return(new Expression("one", "two*three+four"))
                }
            });

            Assert.That(response.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
            {
                { "q", "boop" },
                { "q.parser", "simple" },
                { "return", "one" },
                { "expr.one", "two*three+four" }
            }));
        }

        [Test]
        public async void InfoIncludesFilterQuery()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop"),
                Filter = new StructuredQuery(new AndCondition(new[] { new FieldCondition("thingy 1", "somefield"), new FieldCondition("thingy 2") }))
            });

            Assert.That(response.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
            {
                { "q", "boop" },
                { "q.parser", "simple" },
                { "fq", "(and somefield:'thingy 1' 'thingy 2')" }
            }));
        }

        [Test]
        public async void InfoIncludesOptions()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop"),
                Options = new SearchOptions { DefaultOperator = DefaultOperator.Or }
            });

            Assert.That(response.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
            {
                { "q", "boop" },
                { "q.parser", "simple" },
                { "q.options", "{\"defaultOperator\":\"or\"}" }
            }));
        }

        [Test]
        [ExpectedException(typeof(SearchException), ExpectedMessage = "Failure!")]
        public async void BadRequestThrowsException()
        {
            _httpHandler.Response = ResponseSamples.BadRequest("Failure!");

            await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("123")
            });
        }

        [Test]
        public async void BadRequestIncludesInfoInException()
        {
            _httpHandler.Response = ResponseSamples.BadRequest();

            try
            {
                await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
                {
                    Query = new SimpleQuery("123")
                });
            }
            catch (SearchException ex)
            {
                Assert.That(ex.Request.Url, Is.EqualTo("search"));
                Assert.That(ex.Request.Parameters, Is.EqualTo(new Dictionary<string, string>
                {
                    { "q", "123" },
                    { "q.parser", "simple" }
                }));
            }
        }

        [Test]
        public async void BadRequestsShouldNotBeRetried()
        {
            _httpHandler.Response = ResponseSamples.BadRequest();

            try
            {
                await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
                {
                    Query = new SimpleQuery("123")
                });
            }
            catch (SearchException ex)
            {
                Assert.That(ex.ShouldRetry, Is.False);
            }
        }

        [Test]
        [ExpectedException(typeof(SearchException), ExpectedMessage = "Oh god no!")]
        public async void InternalServerErrorThrowsException()
        {
            _httpHandler.Response = ResponseSamples.InternalServerError("Oh god no!");

            await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("123")
            });
        }

        [Test]
        public async void InternalServerErrorsShouldBeRetried()
        {
            _httpHandler.Response = ResponseSamples.InternalServerError();

            try
            {
                await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
                {
                    Query = new SimpleQuery("123")
                });
            }
            catch (SearchException ex)
            {
                Assert.That(ex.ShouldRetry, Is.True);
            }
        }

        public class Result
        {
            public int Id { get; set; }
        }
    }
}
