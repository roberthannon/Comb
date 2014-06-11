using System.Net.Http;
using Comb.Searching;
using Comb.Searching.Queries;
using Comb.Tests.Support;
using NUnit.Framework;

namespace Comb.Tests.Search
{
    public class SearchAsyncTests
    {
        HttpMessageHandlerStub _httpHandler;
        HttpContentStub _httpContent;
        HttpClient _httpClient;
        CloudSearchClient _cloudSearchClient;

        [SetUp]
        public void SetUp()
        {
            _httpContent = new HttpContentStub();

            var response = new HttpResponseMessage
            {
                Content = _httpContent
            };

            _httpHandler = new HttpMessageHandlerStub(response);
            _httpClient  = new HttpClient(_httpHandler);

            _cloudSearchClient = new CloudSearchClient("");
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
