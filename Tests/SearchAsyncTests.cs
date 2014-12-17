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
            _cloudSearchClient = new CloudSearchClient(new CloudSearchSettings
            {
                Endpoint = "cloudsearch.example.com",
                HttpClientFactory = new TestHttpClientFactory(_httpHandler)
            });
        }

        [Test]
        public async void InfoIncludesDetailsOfSimpleQuery()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop")
            });

            Assert.That(response.Request.Parser, Is.EqualTo("simple"));
            Assert.That(response.Request.Query,  Is.EqualTo("boop"));
            Assert.That(response.Request.Url,    Is.EqualTo("search?q=boop&q.parser=simple"));
        }

        [Test]
        public async void InfoIncludesDetailsOfStructuredQuery()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new StructuredQuery(new FieldCondition("two", "one"))
            });

            Assert.That(response.Request.Parser, Is.EqualTo("structured"));
            Assert.That(response.Request.Query,  Is.EqualTo("one:'two'"));
            Assert.That(response.Request.Url,    Is.EqualTo("search?q=one%3a%27two%27&q.parser=structured"));
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

            Assert.That(response.Request.Size,  Is.EqualTo("123"));
            Assert.That(response.Request.Start, Is.EqualTo("456"));
        }

        [Test]
        public async void InfoIncludesFacets()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop"),
                Facets = new Facet[]
                {
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

            Assert.AreEqual(response.Request.Facets.Length, 2);
            Assert.AreEqual(response.Request.Facets[0].Key, "facet.colour");
            Assert.AreEqual(response.Request.Facets[0].Value, "{buckets:[\"red\",\"green\",\"blue\"],method:\"filter\"}");
            Assert.AreEqual(response.Request.Facets[1].Key, "facet.century");
            Assert.AreEqual(response.Request.Facets[1].Value, "{buckets:[\"[1600,1700}\",\"[1700,1800}\",\"[1800,2000}\"],method:\"interval\"}");
        }

        [Test]
        public async void InfoIncludesSortExpression()
        {
            var expression = new Expression("one", "two*three+four");
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop"),
                Sort = new List<Sort>
                {
                    new Sort(expression, SortDirection.Descending),
                    new Sort(SortFields.Id, SortDirection.Ascending)
                }
            });

            Assert.That(response.Request.Sort, Is.EqualTo("one desc,_id asc"));
            Assert.That(response.Request.Expressions, Contains.Item(new KeyValuePair<string, string>("expr.one", "two*three+four")));
            Assert.That(response.Request.Url, Contains.Substring("expr.one=two*three%2bfour"));
        }

        [Test]
        public async void InfoIncludesReturnFields()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new StructuredQuery(new FieldCondition("yellow")),
                Return = new List<Return> { new Return("this"), new Return("that"), Return.Score }
            });

            Assert.That(response.Request.Return, Is.EqualTo("this,that,_score"));
            Assert.That(response.Request.Url, Contains.Substring("&return=this%2cthat%2c_score"));
        }

        [Test]
        public async void InfoIncludesReturnExpression()
        {
            var expression = new Expression("one", "two*three+four");
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop"),
                Return = new List<Return>
                {
                    new Return(expression)
                }
            });

            Assert.That(response.Request.Return, Is.EqualTo("one"));
            Assert.That(response.Request.Expressions, Contains.Item(new KeyValuePair<string, string>("expr.one", "two*three+four")));
            Assert.That(response.Request.Url, Contains.Substring("&expr.one=two*three%2bfour"));
        }

        [Test]
        public async void InfoIncludesFilterQuery()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop"),
                Filter = new StructuredQuery(new AndCondition(new[] { new FieldCondition("thingy 1", "somefield"), new FieldCondition("thingy 2") }))
            });

            Assert.That(response.Request.Filter, Is.EqualTo("(and somefield:'thingy 1' 'thingy 2')"));
            Assert.That(response.Request.Url, Contains.Substring("&fq=(and+somefield%3a%27thingy+1%27+%27thingy+2%27)"));
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
                Assert.That(ex.Request.Parser, Is.EqualTo("simple"));
                Assert.That(ex.Request.Query,  Is.EqualTo("123"));
                Assert.That(ex.Request.Url,    Is.EqualTo("search?q=123&q.parser=simple"));
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
