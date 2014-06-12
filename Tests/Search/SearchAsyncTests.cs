using System.Net.Http;
using Comb.Tests.Support;
using NUnit.Framework;

namespace Comb.Tests.Search
{
    public class SearchAsyncTests
    {
        TestHttpMessageHandler _httpHandler;
        TestHttpContent _httpContent;
        CloudSearchClient _cloudSearchClient;

        [SetUp]
        public void SetUp()
        {
            _httpContent = new TestHttpContent("{\"status\":{\"rid\":\"/aK08egovR4K+x+p\",\"time-ms\":1},\"hits\":{\"found\":48,\"start\":0,\"hit\":[{\"id\":\"20336\",\"fields\":{\"_score\":\"17.843252\"}},{\"id\":\"22520\",\"fields\":{\"_score\":\"16.343973\"}},{\"id\":\"21168\",\"fields\":{\"_score\":\"15.340134\"}},{\"id\":\"22516\",\"fields\":{\"_score\":\"14.365174\"}},{\"id\":\"22508\",\"fields\":{\"_score\":\"14.335672\"}},{\"id\":\"36809\",\"fields\":{\"_score\":\"12.922127\"}},{\"id\":\"36805\",\"fields\":{\"_score\":\"11.652959\"}},{\"id\":\"32893\",\"fields\":{\"_score\":\"11.511021\"}},{\"id\":\"23012\",\"fields\":{\"_score\":\"11.123916\"}},{\"id\":\"31101\",\"fields\":{\"_score\":\"10.848106\"}}]}}");
            _httpHandler = new TestHttpMessageHandler(new HttpResponseMessage
            {
                Content = _httpContent
            });

            _cloudSearchClient = new CloudSearchClient(new CloudSearchSettings
            {
                Endpoint = "cloudsearch.example.com",
                HttpClientFactory = new TestHttpClientFactory(_httpHandler)
            });
        }

        [Test]
        public async void Boop()
        {
            var response = await _cloudSearchClient.SearchAsync<Result>(new SearchRequest
            {
                Query = new SimpleQuery("boop")
            });

            Assert.That(response.Hits.Found == 0);
        }

        public class Result
        {
            public int Id { get; set; }
        }
    }
}
